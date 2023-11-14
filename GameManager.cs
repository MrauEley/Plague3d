using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager: MonoBehaviour, IEventSystemHandler
{
    private RoadCheck _roadCheck;
    private SpawnTrees _spawnTrees;
    [SerializeField] private GameObject textWin;
    [SerializeField] private GameObject textLoose;
    [SerializeField] private GameObject restartHud;

    public event Action OnRestart;
    public event Action OnWin;
    public event Action OnEnd;

    private HealthBar _healthBar;


    private void Awake()
    {
        _healthBar = FindObjectOfType<HealthBar>();
        _healthBar.OnLoose += HandleLoose;

        _roadCheck = FindAnyObjectByType<RoadCheck>();
        _roadCheck.OnWin += HandleWin;

        _spawnTrees = FindAnyObjectByType<SpawnTrees>();
    }

    private void HandleWin()
    {
        textWin.SetActive(true);
        //BallAnimation 
        {
            restartHud.SetActive(true);
        }

        OnWin?.Invoke();
        OnEnd?.Invoke();


    }

    private void HandleLoose()
    {
        textLoose.SetActive(true);

        restartHud.SetActive(true);
        OnEnd?.Invoke();
    }

    public void RestartGame()
    {
        _spawnTrees.RespawnAllTrees();
        restartHud.SetActive(false);
        textLoose.SetActive(false); 
        textWin.SetActive(false);

        OnRestart?.Invoke();
    }

    private void OnDestroy()
    {
        _roadCheck.OnWin -= HandleWin;
        _healthBar.OnLoose -= HandleLoose;
    }
}
