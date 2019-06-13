using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    public BowlingSetup bowlingSetup;
    public PitchSetup pitchSetup;

    // Start is called before the first frame update
    void Start()
    {
        IController controller = new GameController();
        bowlingSetup.Initialize(controller);
        pitchSetup.Initialize(controller);
    }
}
