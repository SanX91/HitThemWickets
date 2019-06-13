using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingSetup : MonoBehaviour
{
    public CricketBall ball;

    public void Initialize(IController controller)
    {
        IBowling bowling = new Bowling(controller, ball);
        StartCoroutine(bowling.BowlingCycle());
    }
}
