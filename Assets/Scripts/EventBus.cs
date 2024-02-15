using UnityEngine.Events;

public static class EventBus
{
    public static UnityEvent<int> SetCoins = new ();
    public static UnityEvent<int> SetHp = new ();
    public static UnityEvent<int> InitHp = new ();
    
    public static UnityEvent GameOver = new ();
    public static UnityEvent LevelСomplete = new ();
    
    public static UnityEvent GameStart = new ();
    public static UnityEvent ClearPlayerData = new ();
}
