using System;
using TMPro;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    [SerializeField] private Ship _ship;
    [SerializeField] private TextMeshProUGUI levelText;

    [SerializeField] private int costFirstLeveling = 3;
    [SerializeField] private int LevelUpCostMultiplier = 3;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        RefreshButton();
        RefreshText();
    }

    public void LevelUpButton()
    {
        _ship.ShipLevelUp();
        var costLeveling = costFirstLeveling * LevelUpCostMultiplier * _ship._shipLevel;

        _ship.SetMeshLvl(_ship._shipLevel);
        RefreshText();
        _ship.SpendCoins(costLeveling);

        if (_ship._shipLevel == 5)
        {
            _canvasGroup.interactable = false;
        }

        RefreshButton();
    }

    private void RefreshText()
    {
        levelText.text = _ship._shipLevel + "/5";
    }

    private void RefreshButton()
    {
        var costLeveling = costFirstLeveling * LevelUpCostMultiplier * (_ship._shipLevel + 1);
        if (costLeveling > _ship._coins)
        {
            _canvasGroup.interactable = false;
        }
        _canvasGroup.interactable = costLeveling < _ship._coins;
    }
}