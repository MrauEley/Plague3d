using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{
    private Slider _chargeSlider;

    private FireProjectile _fireProjectile;


    private void Awake()
    {
        _fireProjectile = FindAnyObjectByType<FireProjectile>();
        _fireProjectile.OnChargeChange += HandleCharge;
    }
    void Start()
    {
        _chargeSlider = gameObject.GetComponent<Slider>();
        _chargeSlider.value = 0;
    }


    private void HandleCharge(float charge)
    {
        _chargeSlider.value = charge;
    }




}
