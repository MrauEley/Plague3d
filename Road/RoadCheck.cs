using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoadCheck : MonoBehaviour, IEventSystemHandler
{
    public static RoadCheck instance;
    public event Action OnWin;

    public delegate void TreeCount(int value);
    public event TreeCount OnTreesSpawn;
    private List<GameObject> _treesOnRoad;

    private void Awake()
    {
        if(instance == null)
            instance = this;

        Restart();
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tree")
        {
            _treesOnRoad.Add(other.gameObject);
            Debug.Log($"Trigger entered. Total count: {_treesOnRoad.Count}");
            OnTreesSpawn?.Invoke(_treesOnRoad.Count);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Tree")
        {
            _treesOnRoad.Remove(other.gameObject);
            Debug.Log($"Trigger exited. Total count: {_treesOnRoad.Count}");
            CheckWin();
        }
    }

    public void TreeRemove(GameObject tree)
    {
        _treesOnRoad.Remove(tree);
        Debug.Log($"Trigger exited. Total count: {_treesOnRoad.Count}");
        CheckWin();
    }

    private void CheckWin()
    {
        if(_treesOnRoad.Count == 0)
        {
            OnWin?.Invoke();
        }
    }

    public void Restart()
    {
        _treesOnRoad = new List<GameObject>();
    }


}
