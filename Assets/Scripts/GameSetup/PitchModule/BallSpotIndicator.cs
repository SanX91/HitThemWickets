using System.Collections;
using UnityEngine;

namespace HitThemWickets
{
    /// <summary>
    /// The ball spot indicator class.
    /// As the name suggests, the final position of this gameobject, in a bowling cycle, is the target position of the ball's trajectory.
    /// Sets it's own position with the help of user input.
    /// </summary>
    public class BallSpotIndicator : MonoBehaviour
    {
        public BoxCollider bowlingRegion;
        public IndicatorSettings settings;
        private Coroutine positionIndicator;
        private IController controller;

        public void Initialize(IController controller)
        {
            this.controller = controller;
        }

        private IEnumerator PositionIndicator()
        {
            while (gameObject.activeSelf)
            {
                float xPos = transform.position.x - controller.HorizontalAxis() * settings.speed * Time.deltaTime;
                float yPos = transform.position.y;
                float zPos = transform.position.z - controller.VerticalAxis() * settings.speed * Time.deltaTime;

                xPos = Mathf.Clamp(xPos, bowlingRegion.bounds.min.x, bowlingRegion.bounds.max.x);
                zPos = Mathf.Clamp(zPos, bowlingRegion.bounds.min.z, bowlingRegion.bounds.max.z);

                transform.position = new Vector3(xPos, yPos, zPos);
                yield return null;
            }
        }

        public void ToggleIndicator(bool isActive)
        {
            gameObject.SetActive(isActive);

            if (isActive)
            {
                positionIndicator = StartCoroutine(PositionIndicator());
                return;
            }

            StopCoroutine(positionIndicator);
            EventManager.Instance.TriggerEvent(new SetPositionEvent(transform.position));
        }
    }
}
