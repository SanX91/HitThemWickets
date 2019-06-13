using UnityEngine;

namespace HitThemWickets
{
    /// <summary>
    /// Event to toggle the spinner on/off.
    /// </summary>
    public class ToggleSpinnerEvent : IEvent
    {
        private readonly bool isActive;

        public ToggleSpinnerEvent(bool isActive)
        {
            this.isActive = isActive;
        }

        public object GetData()
        {
            return isActive;
        }
    }

    /// <summary>
    /// Event to toggle the ball spot indicator on/off.
    /// </summary>
    public class ToggleIndicatorEvent : IEvent
    {
        private readonly bool isActive;

        public ToggleIndicatorEvent(bool isActive)
        {
            this.isActive = isActive;
        }

        public object GetData()
        {
            return isActive;
        }
    }

    /// <summary>
    /// Event to toggle the ball bounce selector on/off.
    /// </summary>
    public class ToggleBounceSelectorEvent : IEvent
    {
        private readonly bool isActive;

        public ToggleBounceSelectorEvent(bool isActive)
        {
            this.isActive = isActive;
        }

        public object GetData()
        {
            return isActive;
        }
    }

    /// <summary>
    /// Event to set the spin amount to the ball.
    /// </summary>
    public class SetSpinEvent : IEvent
    {
        private readonly float spin;

        public SetSpinEvent(float spin)
        {
            this.spin = spin;
        }

        public object GetData()
        {
            return spin;
        }
    }

    /// <summary>
    /// Event to set the trajectory position to the ball.
    /// </summary>
    public class SetPositionEvent : IEvent
    {
        private Vector3 position;

        public SetPositionEvent(Vector3 position)
        {
            this.position = position;
        }

        public object GetData()
        {
            return position;
        }
    }

    /// <summary>
    /// Event to set the bounce of the ball.
    /// </summary>
    public class SetBounceEvent : IEvent
    {
        private float bounce;

        public SetBounceEvent(float bounce)
        {
            this.bounce = bounce;
        }

        public object GetData()
        {
            return bounce;
        }
    }

    /// <summary>
    /// Event to update the score UI.
    /// </summary>
    public class UpdateScoreUIEvent : IEvent
    {
        private readonly int score;

        public UpdateScoreUIEvent(int score)
        {
            this.score = score;
        }

        public object GetData()
        {
            return score;
        }
    }

    /// <summary>
    /// Event signifying that it's a new ball.
    /// </summary>
    public class NewBallEvent : IEvent
    {
        public object GetData()
        {
            return true;
        }
    }

    /// <summary>
    /// Event signifying that a bowling cycle has come to an end.
    /// </summary>
    public class EndBallEvent : IEvent
    {
        private readonly bool isWicket;

        public EndBallEvent(bool isWicket = false)
        {
            this.isWicket = isWicket;
        }

        public object GetData()
        {
            return isWicket;
        }
    }
}
