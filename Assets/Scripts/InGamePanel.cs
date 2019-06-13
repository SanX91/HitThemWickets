using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class InGamePanel : MonoBehaviour
{
    public GameObject messageText;
    public TextMeshProUGUI scoreText;
    public SpinMeterUI spinMeterUI;

    private void OnEnable()
    {
        EventManager.Instance.AddListener<UpdateScoreUIEvent>(OnUpdateScoreUIEvent);
        EventManager.Instance.AddListener<ToggleSpinnerEvent>(OnToggleSpinnerEvent);
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveListener<UpdateScoreUIEvent>(OnUpdateScoreUIEvent);
        EventManager.Instance.RemoveListener<ToggleSpinnerEvent>(OnToggleSpinnerEvent);
    }

    private void OnUpdateScoreUIEvent(UpdateScoreUIEvent evt)
    {
        scoreText.SetText($"Wickets: {evt.GetData()}");
    }

    private void OnToggleSpinnerEvent(ToggleSpinnerEvent evt)
    {
        spinMeterUI.ToggleMeter((bool)evt.GetData());
    }
}
