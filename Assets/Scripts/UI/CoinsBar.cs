using TMPro;
using UnityEngine;

public class CoinsBar : UIFather
{
    [SerializeField] private TextMeshProUGUI text;

    private void Awake()
    {
        MainUIObject = text.gameObject;
        EventBus.SetCoins.AddListener(SetCoins);
    }

    private void SetCoins(int coins)
    {
        text.text = coins.ToString();
    }
}
