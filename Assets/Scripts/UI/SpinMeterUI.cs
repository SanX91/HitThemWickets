using System.Collections;
using UnityEngine;

namespace HitThemWickets
{
    /// <summary>
    /// The Spin Meter UI class.
    /// </summary>
    public class SpinMeterUI : MonoBehaviour
    {
        public SpinMeterSettings settings;
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
                arrow.Rotate(Vector3.forward, settings.speed * Time.deltaTime * dir);
                if (Mathf.Abs(angle) >= settings.maxAngle
                    && Mathf.Sign(angle) == dir)
                {
                    arrow.rotation = Quaternion.Euler(0, 0, settings.maxAngle * dir);
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
