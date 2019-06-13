using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowling : IBowling
{
    IController controller;
    IBall ball;

    public Bowling(IController controller, IBall ball)
    {
        this.controller = controller;
        this.ball = ball;

        EventManager.Instance.AddListener<SetSpinEvent>(OnSetSpinEvent);
        EventManager.Instance.AddListener<SetPositionEvent>(OnSetPositionEvent);
    }

    private void OnSetPositionEvent(SetPositionEvent evt)
    {
        ball.SetPosition((Vector3)evt.GetData());
    }

    private void OnSetSpinEvent(SetSpinEvent evt)
    {
        ball.SetSpin((float)evt.GetData());
    }

    public IEnumerator BowlingCycle()
    {
        //Bowling cycle
        while(true)
        {
            Debug.Log("Waiting for input");

            //Start new ball
            yield return new WaitUntil(() => controller.IsReady());
            Debug.Log("Start new ball");

            //Start spinner
            EventManager.Instance.TriggerEvent(new ToggleSpinnerEvent(true));
            Debug.Log("Start spinner");
            yield return null;

            //Wait for player's input
            yield return new WaitUntil(() => controller.IsReady());

            //Stop spinner, start indicator
            EventManager.Instance.TriggerEvent(new ToggleSpinnerEvent(false));
            EventManager.Instance.TriggerEvent(new ToggleIndicatorEvent(true));
            Debug.Log("Start indicator");
            yield return null;

            //Wait for player's input
            yield return new WaitUntil(() => controller.IsReady());

            //Stop indicator
            EventManager.Instance.TriggerEvent(new ToggleIndicatorEvent(false));
            yield return null;

            //Bowl
            ball.Bowl();

            //Wait for player's input to start next bowling cycle
            yield return new WaitUntil(() => controller.IsReady());
        }
    }
}
