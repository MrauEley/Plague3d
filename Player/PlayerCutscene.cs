using UnityEngine;

public class PlayerCutscene : MonoBehaviour
{
    private GameManager _gameManager;
    [SerializeField] private Animator _animator;
    private void Awake()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
        _gameManager.OnWin += Cutscene;
        _gameManager.OnRestart += Restart;
    }
    private void Cutscene()
    {
        _animator.ResetTrigger("ToIdle");
        _animator.SetTrigger("ToJump");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Door")
        {
            //_animator.SetTrigger("ToIdle");
        }
    }

    private void OnDestroy()
    {
        _gameManager.OnWin -= Cutscene;
    }

    private void Restart()
    {
        _animator.SetTrigger("ToIdle");
    }
}
