using UnityEngine;

public class RoadScale: MonoBehaviour
{
    private FireProjectile _fireProjectile;
    private GameManager _gameWin;

    private void Awake()
    {
        _fireProjectile = FindAnyObjectByType<FireProjectile>();
        _fireProjectile.OnHPchange += HandleHPchange;
        _gameWin = FindAnyObjectByType<GameManager>();
        _gameWin.OnRestart += Restart;
    }


    private void HandleHPchange(float value)
    {
        transform.localScale -= new Vector3(value, 0, 0);
    }

    private void Restart()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    private void OnDestroy()
    {
        _fireProjectile.OnHPchange -= HandleHPchange;
        _gameWin.OnRestart -= Restart;
    }
}
