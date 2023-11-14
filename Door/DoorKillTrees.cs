using UnityEngine;

public class DoorKillTrees: MonoBehaviour
{
    private bool _canKill = false;
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _gameManager.OnWin += HandleWin;
        _gameManager.OnRestart += HandleRestart;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tree" && _canKill)
        {
            Destroy(other.gameObject);
        }
    }

    private void HandleWin()
    {
        _canKill = true;
    }
    private void HandleRestart()
    {
        _canKill = false;
    }

    private void OnDestroy()
    {
        _gameManager.OnWin -= HandleWin;
        _gameManager.OnRestart -= HandleRestart;
    }

}
