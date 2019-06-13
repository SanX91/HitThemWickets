using UnityEngine;

public class ToggleSpinnerEvent : IEvent
{
    bool isActive;

    public ToggleSpinnerEvent(bool isActive)
    {
        this.isActive = isActive;
    }

    public object GetData()
    {
        return isActive;
    }
}

public class ToggleIndicatorEvent : IEvent
{
    bool isActive;

    public ToggleIndicatorEvent(bool isActive)
    {
        this.isActive = isActive;
    }

    public object GetData()
    {
        return isActive;
    }
}

public class SetSpinEvent : IEvent
{
    float spin;

    public SetSpinEvent(float spin)
    {
        this.spin = spin;
    }

    public object GetData()
    {
        return spin;
    }
}

public class SetPositionEvent : IEvent
{
    Vector3 position;

    public SetPositionEvent(Vector3 position)
    {
        this.position = position;
    }

    public object GetData()
    {
        return position;
    }
}

public class UpdateScoreUIEvent : IEvent
{
    int score;

    public UpdateScoreUIEvent(int score)
    {
        this.score = score;
    }

    public object GetData()
    {
        return score;
    }
}