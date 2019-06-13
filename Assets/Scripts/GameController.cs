using UnityEngine;

public class GameController : IController
{
    public float HorizontalAxis()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public bool IsReady()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    public float VerticalAxis()
    {
        return Input.GetAxisRaw("Vertical");
    }
}