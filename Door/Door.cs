using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField] private Animator animator;
    private GameManager _gameManager;
    private bool _canOpen = true;

    private void Awake()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
        _gameManager.OnRestart += Restart;
        _gameManager.OnWin += HandleWin;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && _canOpen)
        {
            _canOpen = false;
            animator.ResetTrigger("ToIdle");
            animator.SetTrigger("ToOpen");
        }
    }

    private void Restart()
    {
        _canOpen = true;
        animator.SetTrigger("ToIdle");
        transform.localEulerAngles = new Vector3(0, 9.87f, 0);
    }

    private void OnDestroy()
    {
        _gameManager.OnRestart -= Restart;
        _gameManager.OnWin -= HandleWin;
    }

    private void HandleWin()
    {
        _canOpen = true;
    }
}
