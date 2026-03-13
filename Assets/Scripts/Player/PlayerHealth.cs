using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    [SerializeField] private Slider _healthSlider;

    protected override void Awake()
    {
        base.Awake();
        _healthSlider.maxValue = startHealth;
        _healthSlider.minValue = 0;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _healthSlider.value = _currentHealth;
        OnNewHealth += ChangeSliderHealth;
    }

    private void OnDisable()
    {
        OnNewHealth -= ChangeSliderHealth;
    }

    private void ChangeSliderHealth(int newHealth)
    {
        if (newHealth > 0)
            _healthSlider.value = newHealth;
        else if (newHealth <= 0)
            _healthSlider.value = 0;
    }
}