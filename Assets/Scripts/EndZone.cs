using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EventManager.Instance.TriggerEvent(new EndBallEvent());
        Debug.Log($"Collider: {other.name}");
    }
}
