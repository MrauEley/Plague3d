using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class HealthBar : MonoBehaviour, IEventSystemHandler
{
    [SerializeField] private GameObject hpBar;
    [SerializeField] private GameObject hpBarWhite;
    private Slider _hpSlider;
    private Slider _hpSliderWhite;
    private float currentHP;

    private FireProjectile _fireProjectile;
    private GameManager _gameWin;

    public event Action OnLoose;


    private void Awake()
    {
        _fireProjectile = FindAnyObjectByType<FireProjectile>();
        _fireProjectile.OnHPchange += HandleHPchange;
        _gameWin = FindAnyObjectByType<GameManager>();
        _gameWin.OnRestart += Restart;
    }
    void Start()
    {
        _hpSlider = hpBar.GetComponent<Slider>();
        _hpSliderWhite = hpBarWhite.GetComponent<Slider>();
        Restart();

    }

    private void HandleHPchange(float value)
    {
        _hpSlider.value = currentHP - value;
        currentHP = _hpSlider.value;

        CheckLoose();
    }

    private void CheckLoose()
    {
        if(currentHP <= 0.2f)
        {
            OnLoose?.Invoke();
        }
    }

    private void Restart()
    {
        currentHP = 1;
        _hpSlider.value = currentHP;
        _hpSliderWhite.value = currentHP;
    }

    private void OnDestroy()
    {
        _fireProjectile.OnHPchange -= HandleHPchange;
        _gameWin.OnRestart -= Restart;
    }


}
