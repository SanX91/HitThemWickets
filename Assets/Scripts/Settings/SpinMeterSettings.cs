using UnityEngine;

namespace HitThemWickets
{
    /// <summary>
    /// Spin meter settings.
    /// </summary>
    [CreateAssetMenu(fileName = "SpinMeterSettings", menuName = "Hit Them Wickets/Spin meter settings")]
    public class SpinMeterSettings : ScriptableObject
    {
        public float speed;
        public float maxAngle;
    }
}
