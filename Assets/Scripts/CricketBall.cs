using UnityEngine;

public class CricketBall : MonoBehaviour, IBall
{
    public new Rigidbody rigidbody;
    public float speed = 50;
    public float releaseAngle = 45;
    Vector3 targetPosition;
    float spin;

    Vector3 lastPos;
    Vector3 impulse;
    float gravity;

    bool canBowl;

    void Start()
    {
        rigidbody.isKinematic = true;
    }

    public void Bowl()
    {
        Vector3 fireVel, impactPos;
        float gravity;

        if (solve_ballistic_arc_lateral(transform.position, speed, targetPosition, Vector3.zero, 5, out fireVel, out gravity, out impactPos))
        {
            //transform.forward = diffGround;
            Initialize(transform.position, gravity);
            AddImpulse(fireVel);

            canBowl = true;
        }
    }

    public void SetPosition(Vector3 position)
    {
        targetPosition = position;
    }

    public void SetSpin(float spin)
    {
        this.spin = spin;
    }

    void FixedUpdate()
    {
        if(!canBowl)
        {
            return;
        }

        float dt = Time.fixedDeltaTime;
        Vector3 accel = -gravity * Vector3.up;

        Vector3 curPos = transform.position;
        Vector3 newPos = curPos + (curPos - lastPos) + impulse * dt + accel * dt * dt;
        lastPos = curPos;
        rigidbody.MovePosition(newPos);
        //transform.forward = newPos - lastPos;

        impulse = Vector3.zero;

        if (Vector3.Distance(transform.position,targetPosition)<1)
        {
            canBowl = false;
            rigidbody.isKinematic = false;
        }
    }

    public bool solve_ballistic_arc_lateral(Vector3 proj_pos, float lateral_speed, Vector3 target, Vector3 target_velocity, float max_height_offset, out Vector3 fire_velocity, out float gravity, out Vector3 impact_point)
    {

        // Handling these cases is up to your project's coding standards
        Debug.Assert(proj_pos != target && lateral_speed > 0, "fts.solve_ballistic_arc_lateral called with invalid data");

        // Initialize output variables
        fire_velocity = Vector3.zero;
        gravity = 0f;
        impact_point = Vector3.zero;

        // Ground plane terms
        Vector3 targetVelXZ = new Vector3(target_velocity.x, 0f, target_velocity.z);
        Vector3 diffXZ = target - proj_pos;
        diffXZ.y = 0;

        // Derivation
        //   (1) Base formula: |P + V*t| = S*t
        //   (2) Substitute variables: |diffXZ + targetVelXZ*t| = S*t
        //   (3) Square both sides: Dot(diffXZ,diffXZ) + 2*Dot(diffXZ, targetVelXZ)*t + Dot(targetVelXZ, targetVelXZ)*t^2 = S^2 * t^2
        //   (4) Quadratic: (Dot(targetVelXZ,targetVelXZ) - S^2)t^2 + (2*Dot(diffXZ, targetVelXZ))*t + Dot(diffXZ, diffXZ) = 0
        float c0 = Vector3.Dot(targetVelXZ, targetVelXZ) - lateral_speed * lateral_speed;
        float c1 = 2f * Vector3.Dot(diffXZ, targetVelXZ);
        float c2 = Vector3.Dot(diffXZ, diffXZ);
        double t0, t1;
        int n = SolveQuadric(c0, c1, c2, out t0, out t1);

        // pick smallest, positive time
        bool valid0 = n > 0 && t0 > 0;
        bool valid1 = n > 1 && t1 > 0;

        float t;
        if (!valid0 && !valid1)
            return false;
        else if (valid0 && valid1)
            t = Mathf.Min((float)t0, (float)t1);
        else
            t = valid0 ? (float)t0 : (float)t1;

        // Calculate impact point
        impact_point = target + (target_velocity * t);

        // Calculate fire velocity along XZ plane
        Vector3 dir = impact_point - proj_pos;
        fire_velocity = new Vector3(dir.x, 0f, dir.z).normalized * lateral_speed;

        // Solve system of equations. Hit max_height at t=.5*time. Hit target at t=time.
        //
        // peak = y0 + vertical_speed*halfTime + .5*gravity*halfTime^2
        // end = y0 + vertical_speed*time + .5*gravity*time^s
        // Wolfram Alpha: solve b = a + .5*v*t + .5*g*(.5*t)^2, c = a + vt + .5*g*t^2 for g, v
        float a = proj_pos.y;       // initial
        float b = Mathf.Max(proj_pos.y, impact_point.y) + max_height_offset;  // peak
        float c = impact_point.y;   // final

        gravity = -4 * (a - 2 * b + c) / (t * t);
        fire_velocity.y = -(3 * a - 4 * b + c) / t;

        return true;
    }

    int SolveQuadric(double c0, double c1, double c2, out double s0, out double s1)
    {
        s0 = double.NaN;
        s1 = double.NaN;

        double p, q, D;

        /* normal form: x^2 + px + q = 0 */
        p = c1 / (2 * c0);
        q = c2 / c0;

        D = p * p - q;

        if (IsZero(D))
        {
            s0 = -p;
            return 1;
        }
        else if (D < 0)
        {
            return 0;
        }
        else /* if (D > 0) */
        {
            double sqrt_D = System.Math.Sqrt(D);

            s0 = sqrt_D - p;
            s1 = -sqrt_D - p;
            return 2;
        }
    }

    bool IsZero(double d)
    {
        const double eps = 1e-9;
        return d > -eps && d < eps;
    }

    public void Initialize(Vector3 pos, float gravity)
    {
        transform.position = pos;
        lastPos = transform.position;
        this.gravity = gravity;
    }

    public void AddImpulse(Vector3 impulse)
    {
        this.impulse += impulse;
    }
}