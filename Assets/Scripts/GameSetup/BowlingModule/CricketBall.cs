using UnityEngine;

namespace HitThemWickets
{
    /// <summary>
    /// The Cricket Ball class which implements the IBall interface.
    /// Receives the amount of spin and bounce, and also the position to bounce from other sources.
    /// Uses ballistic trajectory to move to the target position.
    /// Adds spin and bounce to the ball after it hits the ground the first time.
    /// </summary>
    public class CricketBall : MonoBehaviour, IBall
    {
        public new Rigidbody rigidbody;
        public CricketBallSettings settings;
        private Vector3 targetPosition;
        private float spin;
        private float bounce;
        private Vector3 lastPos;
        private Vector3 impulse;
        private float gravity;
        private bool isTrajectory;
        private Vector3 initPosition;

        private void Awake()
        {
            initPosition = transform.position;
        }

        private void Start()
        {
            Initialize();
        }

        /// <summary>
        /// Uses ballistic trajectory physics to determine the gravity and the impulse required to move the ball to the target position.
        /// </summary>
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

        public void SetBounce(float amount)
        {
            bounce = amount;
        }

        /// <summary>
        /// Based on the impulse and gravity received in the Bowl() method, and fixedDeltaTime, 
        /// determines the position for the rigidbody(this ball) to move to each fixed update step.
        /// </summary>
        private void FixedUpdate()
        {
            if (!isTrajectory)
            {
                return;
            }

            float dt = Time.fixedDeltaTime;
            Vector3 accel = -gravity * Vector3.up;

            Vector3 curPos = transform.position;
            Vector3 newPos = curPos + (curPos - lastPos) + impulse * dt + accel * dt * dt;
            lastPos = curPos;

            float yCap = Mathf.Clamp(newPos.y, settings.minHeight, settings.maxHeight);
            rigidbody.MovePosition(new Vector3(newPos.x, yCap, newPos.z));

            impulse = Vector3.zero;
        }

        /// <summary>
        /// Checks if the ball hits the ground.
        /// If it's the first time, adds a certain amount of spin and bounce to the ball.
        /// </summary>
        /// <param name="collider"></param>
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.layer != LayerMask.NameToLayer(GameConstants.GroundLayer))
            {
                return;
            }

            if (isTrajectory)
            {
                isTrajectory = false;
                rigidbody.isKinematic = false;

                AddSpinAndBounce();
            }
        }

        private void AddSpinAndBounce()
        {
            rigidbody.velocity = Vector3.zero;

            Vector3 spinForce = Vector3.right * spin * settings.spinFactor * -1;
            Vector3 bounceForce = Vector3.up * (settings.minBounce + bounce * settings.bounceFactor);
            Debug.Log($"Bounce Force: {bounceForce}");
            Vector3 forwardForce = Vector3.forward * settings.speed;
            rigidbody.AddForce(spinForce + bounceForce + forwardForce, ForceMode.Impulse);
        }

        public void Initialize()
        {
            transform.position = initPosition;
            rigidbody.isKinematic = true;
        }
    }
}