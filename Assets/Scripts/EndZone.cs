using UnityEngine;

namespace HitThemWickets
{
    /// <summary>
    /// If the ball enters this region, in a bowling cycle, it triggers the end of that bowling cycle.
    /// </summary>
    public class EndZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            EventManager.Instance.TriggerEvent(new EndBallEvent());
            Debug.Log($"Collider: {other.name}");
        }
    }
}
