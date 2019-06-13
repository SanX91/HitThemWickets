using UnityEngine;

namespace HitThemWickets
{
    /// <summary>
    /// A Game controller class which implements IController.
    /// Uses keyboard controls mainly
    /// </summary>
    public class GameController : IController
    {
        public float HorizontalAxis()
        {
            return Input.GetAxisRaw(GameConstants.HorizontalAxis);
        }

        public bool IsReady()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }

        public float VerticalAxis()
        {
            return Input.GetAxisRaw(GameConstants.VerticalAxis);
        }
    }
}
