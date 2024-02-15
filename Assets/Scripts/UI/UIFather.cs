using UnityEngine;

public class UIFather: MonoBehaviour
{
    protected GameObject MainUIObject;


    private void Awake()
    {
        EventBus.GameOver.AddListener(StopUI);
        EventBus.LevelСomplete.AddListener(StopUI);
        
        EventBus.GameStart.AddListener(StartUI);
    }

    private void StopUI()
    {
        MainUIObject.SetActive(false);
    }

    private void StartUI()
    {
        MainUIObject.SetActive(true);
    }
}