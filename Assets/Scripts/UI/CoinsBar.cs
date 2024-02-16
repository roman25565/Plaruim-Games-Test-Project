using TMPro;
using UnityEngine;

public class CoinsBar : UIFather
{
    [SerializeField] private TextMeshProUGUI text;

    protected override void Awake()
    {
        EventBus.SetCoins.AddListener(SetCoins);
    }

    private void SetCoins(int coins)
    {
        text.text = coins.ToString();
    }
}
