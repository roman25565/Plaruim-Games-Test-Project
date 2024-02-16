using System.Collections;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

public class LevelProgressBar : UIFather
{
    public Slider Slider;
    [SerializeField] private Transform Player;  
    [SerializeField] private Transform EndLevel;
    
    [SerializeField] private float delay = 1.0f;

    private float _levelLength;
    private Coroutine _coroutine;
    protected override void Awake()
    {
        base.Awake();
        _levelLength = Vector3.Distance(Player.position, EndLevel.position);
        
        _coroutine = StartCoroutine(UpdateSliderCalculate());
        
        EventBus.GameOver.AddListener(StopCoroutine);
        EventBus.LevelСomplete.AddListener(StopCoroutine);
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
        var newLevelLength = Vector3.Distance(Player.position, EndLevel.position);
        var percentProgress = (_levelLength - newLevelLength) * 100 / _levelLength; 
        Slider.value = percentProgress / 100;
    }

    private void StopCoroutine()
    {
        StopCoroutine(_coroutine);
    }
}