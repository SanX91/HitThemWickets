using TMPro;
using UnityEngine;

namespace HitThemWickets
{
    /// <summary>
    /// The In Game Panel class.
    /// Responsible for initializing, displaying and updating the UI elements within the game.
    /// </summary>
    public class InGamePanel : MonoBehaviour
    {
        public GameObject messageText;
        public TextMeshProUGUI scoreText;
        public SpinMeterUI spinMeterUI;

        private void Start()
        {
            scoreText.gameObject.SetActive(true);
            spinMeterUI.ToggleMeter(false);
            messageText.SetActive(false);
        }

        private void OnEnable()
        {
            EventManager.Instance.AddListener<NewBallEvent>(OnNewBallEvent);
            EventManager.Instance.AddListener<UpdateScoreUIEvent>(OnUpdateScoreUIEvent);
            EventManager.Instance.AddListener<ToggleSpinnerEvent>(OnToggleSpinnerEvent);
        }

        private void OnDisable()
        {
            EventManager.Instance.RemoveListener<NewBallEvent>(OnNewBallEvent);
            EventManager.Instance.RemoveListener<UpdateScoreUIEvent>(OnUpdateScoreUIEvent);
            EventManager.Instance.RemoveListener<ToggleSpinnerEvent>(OnToggleSpinnerEvent);
        }

        private void OnNewBallEvent(NewBallEvent evt)
        {
            messageText.SetActive(true);
        }

        private void OnUpdateScoreUIEvent(UpdateScoreUIEvent evt)
        {
            scoreText.SetText($"Wickets: {evt.GetData()}");
        }

        private void OnToggleSpinnerEvent(ToggleSpinnerEvent evt)
        {
            if (messageText.activeSelf)
            {
                messageText.SetActive(false);
            }

            spinMeterUI.ToggleMeter((bool)evt.GetData());
        }
    }
}
