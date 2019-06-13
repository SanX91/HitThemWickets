using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchSetup : MonoBehaviour
{
    public BallSpotIndicator spotIndicator;

    private void OnEnable()
    {
        EventManager.Instance.AddListener<ToggleIndicatorEvent>(OnToggleIndicatorEvent);
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveListener<ToggleIndicatorEvent>(OnToggleIndicatorEvent);
    }

    private void OnToggleIndicatorEvent(ToggleIndicatorEvent evt)
    {
        spotIndicator.ToggleIndicator((bool)evt.GetData());
    }
}
