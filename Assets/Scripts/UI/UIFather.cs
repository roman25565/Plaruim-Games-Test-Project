using UnityEngine;

public class UIFather: MonoBehaviour
{
    private GameObject MainUIObject;
    
    protected virtual void Awake()
    {
        MainUIObject = transform.GetChild(0).gameObject;
        
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