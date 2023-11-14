using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnTrees : MonoBehaviour, IEventSystemHandler
{
    [SerializeField] private GameObject treePrefab;
    [SerializeField] int maxTrees;
    private List<GameObject> _trees = new List<GameObject>();
    [SerializeField]private Transform treesBin;

    private RoadCheck _roadCheck;

    private void Start()
    {
        _roadCheck = FindObjectOfType<RoadCheck>(); 
    }

    private void SpawnTree()
    {
        float x = Random.Range(-2f, 2f);
        float z = Random.Range(-7f, 0f);
        Vector3 randomPos = new Vector3(x, 0, z);
        float randomRotation = Random.Range(0, 360);

        GameObject tree = Instantiate(treePrefab, randomPos, transform.rotation);
        tree.transform.eulerAngles = new Vector3(0, randomRotation, 0);
        tree.transform.SetParent(treesBin);
        _trees.Add(tree);
    }

    public void RespawnAllTrees()
    {
        for (int i = 0; i < _trees.Count; i += 0)
        {
            Destroy(_trees[i]);
            _trees.RemoveAt(i);
        }

        _roadCheck.Restart();

        for (int i = 0; i < maxTrees; i++)
        {
            SpawnTree();
        }

    }

    public void Trees50()
    {
        maxTrees = 50;
    }
    public void Trees100()
    {
        maxTrees = 100;
    }
    public void Trees150()
    {
        maxTrees = 150;
    }


}
