using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    public Vector3 GetPosition(IIndicator indicator)
    {
        throw new System.NotImplementedException();
    }

    public float GetSpin(ISpinner spinner)
    {
        throw new System.NotImplementedException();
    }

    public bool IsReady(IController controller)
    {
        return controller.IsReady();
    }
}
