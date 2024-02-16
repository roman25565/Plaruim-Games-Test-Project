using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BosHpBar : MonoBehaviour
{
    public Slider Slider;
    [SerializeField] private Boss _boss;
    
    [SerializeField] private float delay = 1.0f;

    private float _levelLength;
    private Coroutine _coroutine;
    private void Awake()
    {
        _coroutine = StartCoroutine(UpdateSliderCalculate());
        
        EventBus.GameOver.AddListener(StopCoroutine);
        EventBus.LevelСomplete.AddListener(StopCoroutine);
        
        EventBus.GameStart.AddListener(GameStart);
    }
    
    private IEnumerator UpdateSliderCalculate()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            UpdateProgressSlider();
        }
    }

    private void UpdateProgressSlider()
    {
        var percentProgress = (_boss.MaxHealth - _boss.Health) * 100 / _boss.MaxHealth; 
        Slider.value = 1 - percentProgress / 100;
    }

    private void StopCoroutine()
    {
        StopCoroutine(_coroutine);
    }

    private void GameStart()
    {
        _coroutine = StartCoroutine(UpdateSliderCalculate());
    }
}
