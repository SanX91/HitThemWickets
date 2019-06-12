using UnityEngine;

public class CricketBall : MonoBehaviour, IBall
{
    public void Bowl()
    {
        Debug.Log("Bowling");
    }

    public void SetPosition(Vector3 position)
    {
        Debug.Log($"Ball position: {position}");
    }

    public void SetSpin(float spin)
    {
        Debug.Log($"Ball spin: {spin}");
    }
}