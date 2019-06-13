using System.Collections;
using UnityEngine;

namespace HitThemWickets
{
    /// <summary>
    /// The Spin Meter UI class.
    /// </summary>
    public class SpinMeterUI : MonoBehaviour
    {
        public float speed = 50;
        public float maxAngle = 30;
        public RectTransform arrow;
        private float angle;
        private Coroutine meterCycle;

        /// <summary>
        /// The meter cycle from left to right and vice versa.
        /// </summary>
        /// <returns></returns>
        private IEnumerator MeterCycle()
        {
            float dir = Random.Range(0, 1) == 0 ? -1 : 1;
            while (gameObject.activeSelf)
            {
                angle = Vector3.SignedAngle(Vector3.up, arrow.up, Vector3.forward);
                arrow.Rotate(Vector3.forward, speed * Time.deltaTime * dir);
                if (Mathf.Abs(angle) >= maxAngle
                    && Mathf.Sign(angle) == dir)
                {
                    arrow.rotation = Quaternion.Euler(0, 0, maxAngle * dir);
                    dir *= -1;
                }

                yield return null;
            }
        }

        public void ToggleMeter(bool isActive)
        {
            gameObject.SetActive(isActive);

            if (isActive)
            {
                meterCycle = StartCoroutine(MeterCycle());
                return;
            }

            if (meterCycle != null)
            {
                StopCoroutine(meterCycle);
                EventManager.Instance.TriggerEvent(new SetSpinEvent(angle));
            }
        }
    }
}
