using UnityEngine;
using System.Collections;

public class TreeScript : MonoBehaviour
{
    private bool _isActive = false;
    [SerializeField] private float lifetimeAfterActivation = 1f;

    [SerializeField] Material materialAfterActivation;

    private void Update()
    {
        LifeTimer();

        if (lifetimeAfterActivation <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void TreeActivation()
    {
        _isActive = true;

       transform.GetChild(0).gameObject.GetComponent<Renderer>().material = materialAfterActivation;
    }

    private void LifeTimer()
    {
        if (_isActive) 
        {
            lifetimeAfterActivation -= Time.deltaTime; 
        }
    }
}
