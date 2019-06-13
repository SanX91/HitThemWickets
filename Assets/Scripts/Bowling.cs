﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowling : IBowling
{
    enum States
    {
        NewBall,
        Spinner,
        Indicator,
        EndBall
    }

    IController controller;
    IBall ball;
    States state;

    public Bowling(IController controller, IBall ball)
    {
        this.controller = controller;
        this.ball = ball;

        EventManager.Instance.AddListener<SetSpinEvent>(OnSetSpinEvent);
        EventManager.Instance.AddListener<SetPositionEvent>(OnSetPositionEvent);
        EventManager.Instance.AddListener<EndBallEvent>(OnEndBallEvent);
    }

    private void OnEndBallEvent(EndBallEvent evt)
    {
        if(state == States.EndBall)
        {
            state = States.NewBall;
        }
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
            yield return new WaitForSeconds(2);

            //New Ball Event
            ball.Initialize();
            EventManager.Instance.TriggerEvent(new NewBallEvent());

            //Start new ball
            yield return new WaitUntil(() => controller.IsReady());
            state = States.Spinner;

            //Start spinner
            EventManager.Instance.TriggerEvent(new ToggleSpinnerEvent(true));
            Debug.Log("Start spinner");
            yield return null;

            //Wait for player's input
            yield return new WaitUntil(() => controller.IsReady());
            state = States.Indicator;

            //Stop spinner, start indicator
            EventManager.Instance.TriggerEvent(new ToggleSpinnerEvent(false));
            EventManager.Instance.TriggerEvent(new ToggleIndicatorEvent(true));
            Debug.Log("Start indicator");
            yield return null;

            //Wait for player's input
            yield return new WaitUntil(() => controller.IsReady());
            state = States.EndBall;

            //Stop indicator
            EventManager.Instance.TriggerEvent(new ToggleIndicatorEvent(false));
            yield return null;

            //Bowl
            ball.Bowl();

            //Wait for player's input to start next bowling cycle
            yield return new WaitUntil(() => state == States.NewBall);
            Debug.Log("The End");
        }
    }
}
