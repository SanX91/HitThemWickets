using UnityEngine;

/// <summary>
/// Ballistic Physics Helper methods. Source - https://unity3d.college/2017/06/30/unity3d-cannon-projectile-ballistics/
/// </summary>
public static class BallisticPhysics
{
    public static bool SolveBallisticArcLateral(Vector3 projPos, float lateralSpeed, Vector3 target, Vector3 targetVelocity, float maxHeightOffset, out Vector3 fireVelocity, out float gravity, out Vector3 impactPoint)
    {
        // Handling these cases is up to your project's coding standards
        Debug.Assert(projPos != target && lateralSpeed > 0, "fts.solve_ballistic_arc_lateral called with invalid data");

        // Initialize output variables
        fireVelocity = Vector3.zero;
        gravity = 0f;
        impactPoint = Vector3.zero;

        // Ground plane terms
        Vector3 targetVelXZ = new Vector3(targetVelocity.x, 0f, targetVelocity.z);
        Vector3 diffXZ = target - projPos;
        diffXZ.y = 0;

        // Derivation
        //   (1) Base formula: |P + V*t| = S*t
        //   (2) Substitute variables: |diffXZ + targetVelXZ*t| = S*t
        //   (3) Square both sides: Dot(diffXZ,diffXZ) + 2*Dot(diffXZ, targetVelXZ)*t + Dot(targetVelXZ, targetVelXZ)*t^2 = S^2 * t^2
        //   (4) Quadratic: (Dot(targetVelXZ,targetVelXZ) - S^2)t^2 + (2*Dot(diffXZ, targetVelXZ))*t + Dot(diffXZ, diffXZ) = 0
        float c0 = Vector3.Dot(targetVelXZ, targetVelXZ) - lateralSpeed * lateralSpeed;
        float c1 = 2f * Vector3.Dot(diffXZ, targetVelXZ);
        float c2 = Vector3.Dot(diffXZ, diffXZ);
        int n = SolveQuadric(c0, c1, c2, out double t0, out double t1);

        // pick smallest, positive time
        bool valid0 = n > 0 && t0 > 0;
        bool valid1 = n > 1 && t1 > 0;

        float t;
        if (!valid0 && !valid1)
        {
            return false;
        }
        else if (valid0 && valid1)
        {
            t = Mathf.Min((float)t0, (float)t1);
        }
        else
        {
            t = valid0 ? (float)t0 : (float)t1;
        }

        // Calculate impact point
        impactPoint = target + (targetVelocity * t);

        // Calculate fire velocity along XZ plane
        Vector3 dir = impactPoint - projPos;
        fireVelocity = new Vector3(dir.x, 0f, dir.z).normalized * lateralSpeed;

        // Solve system of equations. Hit max_height at t=.5*time. Hit target at t=time.
        //
        // peak = y0 + vertical_speed*halfTime + .5*gravity*halfTime^2
        // end = y0 + vertical_speed*time + .5*gravity*time^s
        // Wolfram Alpha: solve b = a + .5*v*t + .5*g*(.5*t)^2, c = a + vt + .5*g*t^2 for g, v
        float a = projPos.y;       // initial
        float b = Mathf.Max(projPos.y, impactPoint.y) + maxHeightOffset;  // peak
        float c = impactPoint.y;   // final

        gravity = -4 * (a - 2 * b + c) / (t * t);
        fireVelocity.y = -(3 * a - 4 * b + c) / t;

        return true;
    }

    private static int SolveQuadric(double c0, double c1, double c2, out double s0, out double s1)
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

    private static bool IsZero(double d)
    {
        const double eps = 1e-9;
        return d > -eps && d < eps;
    }
}
