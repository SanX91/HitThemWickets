using UnityEngine;

namespace HitThemWickets
{
    /// <summary>
    /// The game setup class.
    /// Initializes various modules which have similar dependencies.
    /// Currently initializing the BowlingSetup and PitchSetup classes. 
    /// </summary>
    public class GameSetup : MonoBehaviour
    {
        public BowlingSetup bowlingSetup;
        public PitchSetup pitchSetup;

        // Start is called before the first frame update
        private void Start()
        {
            IController controller = new GameController();
            bowlingSetup.Initialize(controller);
            pitchSetup.Initialize(controller);
        }
    }
}
