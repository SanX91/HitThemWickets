using UnityEngine;

namespace HitThemWickets
{
    /// <summary>
    /// Bounce meter settings.
    /// </summary>
    [CreateAssetMenu(fileName = "BounceMeterSettings", menuName = "Hit Them Wickets/Bounce meter settings")]
    public class BounceMeterSettings : ScriptableObject
    {
        public float speed;
    }
}
