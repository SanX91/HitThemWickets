using UnityEngine;

public interface IPlayer
{
    bool IsReady(IController controller);
    float GetSpin(ISpinner spinner);
    Vector3 GetPosition(IIndicator indicator);
}