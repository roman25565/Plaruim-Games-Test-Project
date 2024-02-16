using UnityEngine;

public class MenuManager : MonoBehaviour
{
    
    [SerializeField] private GameObject GameOverWindow;
    [SerializeField] private GameObject LevelCompleteWindow;
    
    private void Awake()
    {
        EventBus.LevelСomplete.AddListener(() => LevelCompleteWindow.SetActive(true));
        EventBus.GameOver.AddListener(() => GameOverWindow.SetActive(true));
    }

    public void NextGame()
    {
        StartGame();
    }

    public void RestartGame()
    {
        EventBus.ClearPlayerData?.Invoke();
        StartGame();
    }

    private void StartGame()
    {
        GameOverWindow.SetActive(false);
        LevelCompleteWindow.SetActive(false);
        EventBus.GameStart?.Invoke();
    }
}
