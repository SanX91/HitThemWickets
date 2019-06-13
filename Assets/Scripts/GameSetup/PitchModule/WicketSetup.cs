using System.Collections.Generic;
using UnityEngine;

namespace HitThemWickets
{
    /// <summary>
    /// The Wicket setup class.
    /// Intializes the wickets and it's bails.
    /// Also responsible for checking if the ball enter it's trigger.
    /// If the ball enters the trigger, a wicket is taken by the user.
    /// </summary>
    public class WicketSetup : MonoBehaviour
    {
        public new BoxCollider collider;
        public List<Rigidbody> wickets, bails;
        private List<Vector3> initWktPositions, initBailPositions;

        /// <summary>
        /// Stores the initial wicket and bail positions, to help reset them later.
        /// </summary>
        private void Awake()
        {
            initBailPositions = new List<Vector3>();
            initWktPositions = new List<Vector3>();

            foreach (Rigidbody bail in bails)
            {
                initBailPositions.Add(bail.transform.position);
            }

            foreach (Rigidbody wicket in wickets)
            {
                initWktPositions.Add(wicket.transform.position);
            }
        }

        private void Start()
        {
            Initialize();
        }

        /// <summary>
        /// Can only be triggered by the ball as Physics settings of this gameobject's layer has been set that way.
        /// Triggers an EndBallEvent, with a true parameter, which signifies that a wicket is taken by the user.
        /// The collider on this gameobject is switched off to prevent further trigger entries.
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            foreach (Rigidbody bail in bails)
            {
                bail.isKinematic = false;
            }

            collider.enabled = false;
            EventManager.Instance.TriggerEvent(new EndBallEvent(true));
        }

        /// <summary>
        /// Initializes or resets the wickets and the bails to their initial positions.
        /// The bails are kinematic, to prevent their force impact on the wickets.
        /// </summary>
        public void Initialize()
        {
            for (int i = 0; i < bails.Count; i++)
            {
                bails[i].isKinematic = true;
                bails[i].transform.position = initBailPositions[i];
                bails[i].transform.rotation = Quaternion.identity;
            }

            for (int i = 0; i < wickets.Count; i++)
            {
                wickets[i].velocity = wickets[i].angularVelocity = Vector3.zero;
                wickets[i].transform.position = initWktPositions[i];
                wickets[i].transform.rotation = Quaternion.identity;
            }

            collider.enabled = true;
        }
    }
}
