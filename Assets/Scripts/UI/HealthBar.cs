using UnityEngine;
using UnityEngine.UI;

public class HealthBar : UIFather
{
    private Image[] _heartFills;

    [SerializeField] private GameObject heartPrefab;
    
    [SerializeField] private Transform parent;

    protected override void Awake()
    {
        base.Awake();
        EventBus.SetHp.AddListener(UiSetHp);
        EventBus.InitHp.AddListener(Init);
    }

    private void UiSetHp(int health)
    {
        for (var i = 0; i < _heartFills.Length; i++)
        {
            _heartFills[i].fillAmount = i < health ? 1 : 0;
        }
    }

    private void Init(int health)
    {
        _heartFills = new Image[health];
        for (var i = 0; i < health; i++)
        { 
            var newImage = Instantiate(heartPrefab, parent).GetComponent<Image>();
            _heartFills[i] = newImage.transform.GetChild(0).GetComponent<Image>();
        }
    }
}
