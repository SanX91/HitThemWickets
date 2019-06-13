using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingSetup : MonoBehaviour
{
    public CricketBall ball;

    // Start is called before the first frame update
    void Start()
    {
        IController controller = new GameController();
        IBowling bowling = new Bowling(controller, ball);
        StartCoroutine(bowling.BowlingCycle());
    }
}
