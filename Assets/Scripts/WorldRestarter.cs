using UnityEngine;

public class WorldRestarter : MonoBehaviour
{
    private void Awake()
    {
        EventBus.GameStart.AddListener(GameStart);
    }

    private void GameStart()
    {
        EnableAllChild(transform);
    }

    private void EnableAllChild(Transform father)
    {
        foreach (Transform child in father)
        {
            child.gameObject.SetActive(true);
            EnableAllChild(child.transform);
        }
    }
}