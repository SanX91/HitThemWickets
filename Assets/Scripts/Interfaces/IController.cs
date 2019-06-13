namespace HitThemWickets
{
    /// <summary>
    /// An interface for setting up various controllers.
    /// </summary>
    public interface IController
    {
        bool IsReady();
        float HorizontalAxis();
        float VerticalAxis();
    }
}