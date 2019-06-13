using UnityEngine;

namespace HitThemWickets
{
    /// <summary>
    /// The pitch setup class.
    /// Initializes and triggers the functionality of it's submodules - BallSpotIndicator and WicketSetup.
    /// </summary>
    public class PitchSetup : MonoBehaviour
    {
        public BallSpotIndicator spotIndicator;
        public WicketSetup wicketSetup;

        private void OnEnable()
        {
            EventManager.Instance.AddListener<ToggleIndicatorEvent>(OnToggleIndicatorEvent);
            EventManager.Instance.AddListener<NewBallEvent>(OnNewBallEvent);
        }

        private void OnDisable()
        {
            EventManager.Instance.RemoveListener<ToggleIndicatorEvent>(OnToggleIndicatorEvent);
            EventManager.Instance.RemoveListener<NewBallEvent>(OnNewBallEvent);
        }

        private void Start()
        {
            spotIndicator.gameObject.SetActive(false);
        }

        public void Initialize(IController controller)
        {
            spotIndicator.Initialize(controller);
        }

        private void OnNewBallEvent(NewBallEvent evt)
        {
            wicketSetup.Initialize();
        }

        private void OnToggleIndicatorEvent(ToggleIndicatorEvent evt)
        {
            spotIndicator.ToggleIndicator((bool)evt.GetData());
        }
    }
}
