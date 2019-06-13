using UnityEngine;

public class CricketBall : MonoBehaviour, IBall
{
    public new Rigidbody rigidbody;
    public float speed = 50;
    public float spinFactor = 0.5f;
    Vector3 targetPosition;
    float spin;

    Vector3 lastPos;
    Vector3 impulse;
    float gravity;

    bool isTrajectory;

    void Start()
    {
        rigidbody.isKinematic = true;
    }

    public void Bowl()
    {
        if (BallisticPhysics.SolveBallisticArcLateral(transform.position, speed, targetPosition, Vector3.zero, 5, out Vector3 fireVel, out float gravity, out Vector3 impactPos))
        {
            lastPos = transform.position;
            this.gravity = gravity;
            impulse += fireVel;
            isTrajectory = true;
        }
    }

    public void SetPosition(Vector3 position)
    {
        targetPosition = position + Vector3.up;
    }

    public void SetSpin(float spin)
    {
        this.spin = spin;
    }

    void FixedUpdate()
    {
        if(!isTrajectory)
        {
            return;
        }

        float dt = Time.fixedDeltaTime;
        Vector3 accel = -gravity * Vector3.up;

        Vector3 curPos = transform.position;
        Vector3 newPos = curPos + (curPos - lastPos) + impulse * dt + accel * dt * dt;
        lastPos = curPos;

        float yCap = Mathf.Clamp(newPos.y, 0, 10);
        rigidbody.MovePosition(new Vector3(newPos.x,yCap,newPos.z));

        impulse = Vector3.zero;
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.layer != LayerMask.NameToLayer("Ground"))
        {
            return;
        }

        Debug.Log("Hit");
        if(isTrajectory)
        {
            isTrajectory = false;
            rigidbody.isKinematic = false;

            AddSpinAndBounce();
        }
    }

    void AddSpinAndBounce()
    {
        Vector3 spinForce = Vector3.right * spin * spinFactor * -1;
        Vector3 bounceForce = Vector3.up * 30;
        rigidbody.AddForce(spinForce + bounceForce, ForceMode.Impulse);
    }
}