using UnityEngine;

namespace HitThemWickets
{
    /// <summary>
    /// The Game Data class.
    /// Currently storing the number of wickets taken by the user.
    /// Listens to the EndBallEvent to stay updated on the wicket count.
    /// Dispatches an event to update the score UI on wicket count increment.
    /// </summary>
    public class GameData : MonoBehaviour
    {
        private int wickets;

        private void OnEnable()
        {
            EventManager.Instance.AddListener<EndBallEvent>(OnEndBallEvent);
        }

        private void OnDisable()
        {
            EventManager.Instance.RemoveListener<EndBallEvent>(OnEndBallEvent);
        }

        private void Start()
        {
            EventManager.Instance.TriggerEvent(new UpdateScoreUIEvent(wickets));
        }

        private void OnEndBallEvent(EndBallEvent evt)
        {
            if (!(bool)evt.GetData())
            {
                return;
            }

            wickets++;
            EventManager.Instance.TriggerEvent(new UpdateScoreUIEvent(wickets));
        }
    }
}

