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
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveListener<UpdateScoreUIEvent>(OnUpdateScoreUIEvent);
    }

    private void OnUpdateScoreUIEvent(UpdateScoreUIEvent evt)
    {
        scoreText.SetText($"Wickets: {evt.GetData()}");
    }
}
