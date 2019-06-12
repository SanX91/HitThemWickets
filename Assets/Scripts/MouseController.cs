using UnityEngine;

public class MouseController : IController
{
    public float HorizontalAxis()
    {
        return Input.GetAxisRaw("Mouse X");
    }

    public bool IsReady()
    {
        return Input.GetKeyDown(KeyCode.Mouse0);
    }

    public float VerticalAxis()
    {
        return Input.GetAxisRaw("Mouse Y");
    }
}