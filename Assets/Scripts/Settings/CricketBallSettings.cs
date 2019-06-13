using UnityEngine;

namespace HitThemWickets
{
    /// <summary>
    /// Cricket ball settings
    /// </summary>
    [CreateAssetMenu(fileName = "CricketBallSettings", menuName = "Hit Them Wickets/Cricket Ball Settings")]
    public class CricketBallSettings : ScriptableObject
    {
        public float speed = 50;
        public float spinFactor = 0.5f;
        [Range(1, 10)]
        public float launchArc = 5;
        public float minHeight = 0, maxHeight = 100;
    }
}
