using UnityEngine;

public class WorldRestarter : MonoBehaviour
{
    [SerializeField] private Transform CoinsFather;
    [SerializeField] private Transform ObstacleFather;
    [SerializeField] private Transform WorldConvasFather;

    private void Awake()
    {
        EventBus.GameStart.AddListener(GameStart);
    }

    private void GameStart()
    {
        EnableAllChild(CoinsFather);
        EnableAllChild(ObstacleFather);
        EnableAllChild(WorldConvasFather);
        EnableAllChild(transform);
    }

    private void EnableAllChild(Transform father)
    {
        foreach (Transform child in father)
            child.gameObject.SetActive(true);
    }
}