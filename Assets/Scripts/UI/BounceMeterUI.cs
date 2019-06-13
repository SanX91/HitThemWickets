using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace HitThemWickets
{
    public class BounceMeterUI : MonoBehaviour
    {
        public BounceMeterSettings settings;
        public Image slider;
        private Coroutine meterCycle;

        private IEnumerator MeterCycle()
        {
            float dir = 1;

            while (gameObject.activeSelf)
            {
                slider.fillAmount += settings.speed * dir * Time.deltaTime;

                if(slider.fillAmount>=1.0f||slider.fillAmount<=0.0f)
                {
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
                EventManager.Instance.TriggerEvent(new SetBounceEvent(slider.fillAmount));
            }
        }
    }
}
