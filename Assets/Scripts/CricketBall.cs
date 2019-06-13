using UnityEngine;

public class CricketBall : MonoBehaviour, IBall
{
    public new Rigidbody rigidbody;
    public CricketBallSettings settings;

    Vector3 targetPosition;
    float spin;

    Vector3 lastPos;
    Vector3 impulse;
    float gravity;

    bool isTrajectory;
    Vector3 initPosition;

    void Awake()
    {
        initPosition = transform.position;
    }

    void Start()
    {
        Initialize();
    }

    public void Bowl()
    {
        if (BallisticPhysics.SolveBallisticArcLateral(transform.position, settings.speed, targetPosition, 
            Vector3.zero, settings.launchArc, out Vector3 fireVel, out float gravity, 
            out Vector3 impactPos))
        {
            lastPos = transform.position;
            this.gravity = gravity;
            impulse = fireVel;
            isTrajectory = true;
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
        if(!isTrajectory)
        {
            return;
        }

        float dt = Time.fixedDeltaTime;
        Vector3 accel = -gravity * Vector3.up;

        Vector3 curPos = transform.position;
        Vector3 newPos = curPos + (curPos - lastPos) + impulse * dt + accel * dt * dt;
        lastPos = curPos;

        float yCap = Mathf.Clamp(newPos.y, settings.minHeight, settings.maxHeight);
        rigidbody.MovePosition(new Vector3(newPos.x,yCap,newPos.z));

        impulse = Vector3.zero;
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.layer != LayerMask.NameToLayer(GameConstants.GroundLayer))
        {
            return;
        }

        if(isTrajectory)
        {
            isTrajectory = false;
            rigidbody.isKinematic = false;

            AddSpinAndBounce();
        }
    }

    void AddSpinAndBounce()
    {
        rigidbody.velocity = Vector3.zero;

        Vector3 spinForce = Vector3.right * spin * settings.spinFactor * -1;
        Vector3 bounceForce = Vector3.up * 10;
        Vector3 forwardForce = Vector3.forward * settings.speed;
        rigidbody.AddForce(spinForce + bounceForce + forwardForce, ForceMode.Impulse);
    }

    public void Initialize()
    {
        transform.position = initPosition;
        rigidbody.isKinematic = true;
    }
}