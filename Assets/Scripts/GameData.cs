using UnityEngine;

public class GameData : MonoBehaviour
{
    private int wickets;

    public void UpdateWicket()
    {
        wickets++;
        EventManager.Instance.TriggerEvent(new UpdateScoreUIEvent(wickets));
    }
}
