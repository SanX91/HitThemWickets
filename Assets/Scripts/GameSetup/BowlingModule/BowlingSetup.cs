using UnityEngine;

namespace HitThemWickets
{
    /// <summary>
    /// Initializes a IBowling interface and starts it's bowling cycle coroutine.
    /// </summary>
    public class BowlingSetup : MonoBehaviour
    {
        public CricketBall ball;

        public void Initialize(IController controller)
        {
            IBowling bowling = new Bowling(controller, ball);
            StartCoroutine(bowling.BowlingCycle());
        }
    }
}
