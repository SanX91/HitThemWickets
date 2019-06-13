using System;
using UnityEngine;

public class GameData : MonoBehaviour
{
    private int wickets;

    private void OnEnable()
    {
        EventManager.Instance.AddListener<EndBallEvent>(OnEndBallEvent);
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveListener<EndBallEvent>(OnEndBallEvent);
    }

    private void Start()
    {
        EventManager.Instance.TriggerEvent(new UpdateScoreUIEvent(wickets));
    }

    private void OnEndBallEvent(EndBallEvent evt)
    {
        if(!(bool)evt.GetData())
        {
            return;
        }

        wickets++;
        EventManager.Instance.TriggerEvent(new UpdateScoreUIEvent(wickets));
    }
}
