using UnityEngine;

public class PlayerScale: MonoBehaviour
{
    private FireProjectile _fireProjectile;
    private GameManager _gameManager;

    

    private void Awake()
    {
        _fireProjectile = FindAnyObjectByType<FireProjectile>();
        _fireProjectile.OnHPchange += HandleHPlose;
        _gameManager = FindAnyObjectByType<GameManager>();
        _gameManager.OnRestart += Restart;
    }



    private void HandleHPlose(float value)
    {
        transform.localScale -= new Vector3(value, value, value);
    }


    private void Restart()
    {
        transform.localScale = new Vector3(1, 1, 1);
        transform.rotation = new Quaternion(0, 0, 0,0);
    }

    private void OnDestroy()
    {
        _fireProjectile.OnHPchange -= HandleHPlose;
        _gameManager.OnRestart -= Restart;
    }

    
}
