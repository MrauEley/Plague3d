using UnityEngine;

public class CutsceneMovement: MonoBehaviour
{
    private GameManager _gameManager;

    [SerializeField] private GameObject player;
    [SerializeField] private Transform cutsceneFinalPosition;
    [SerializeField] private float cutsceneBallSpeed;
    private bool _isCutscene = false;

    private void Awake()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
        _gameManager.OnWin += CutsceneOn;
        _gameManager.OnRestart += Restart;
    }
    private void Update()
    {
        if (_isCutscene)
        {
            transform.position = Vector3.MoveTowards(transform.position, cutsceneFinalPosition.position, cutsceneBallSpeed * Time.deltaTime);
        }
    }

    private void CutsceneOn()
    {
        cutsceneFinalPosition.position = new Vector3(1.339f, player.transform.localScale.y / 2, 1.125f);
        _isCutscene=true;   
    }

    private void OnDestroy()
    {
        _gameManager.OnWin -= CutsceneOn;
        _gameManager.OnRestart -= Restart;
    }

    private void Restart()
    {
        transform.localPosition = new Vector3(-0.22f, 0.28f, -8.04f);
        _isCutscene = false;
    }


}
