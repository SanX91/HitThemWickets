using UnityEngine;

namespace HitThemWickets
{
    /// <summary>
    /// The ball spot indicator settings.
    /// </summary>
    [CreateAssetMenu(fileName = "IndicatorSettings", menuName = "Hit Them Wickets/Ball Indicator Settings")]
    public class IndicatorSettings : ScriptableObject
    {
        public float speed = 20;
    }
}
