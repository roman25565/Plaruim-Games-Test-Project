using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private enum MenuType
    {
        GameOver,
        LevelComplete
    }
    
    [SerializeField] private GameObject GameOverWindow;
    [SerializeField] private GameObject LevelCompleteWindow;

    private MenuType _menuType;
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
        StartGame();
        EventBus.ClearPlayerData?.Invoke();
    }

    private void StartGame()
    {
        EventBus.GameStart?.Invoke();
        GameOverWindow.SetActive(false);
        LevelCompleteWindow.SetActive(false);
    }
}
