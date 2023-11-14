using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FireProjectile : MonoBehaviour, IEventSystemHandler
{
    [SerializeField] private GameObject projectilePrefab;
    private GameObject _tempProjectile;
    [SerializeField] private Transform projectileBin;

    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileLife = 10;
    [SerializeField]private float shootToHPCoef; //depends on how many trees have spawned on road  (0.1 * treesCount) / 0.8
    [SerializeField] private float chargeSpeed;
    private float _spawnDistance = 0.4f; //0-1 from original player scale
    private float _tempProjectileSize = 0;
    [SerializeField] private float startSize = 0.1f;
    [SerializeField] private float maxSize = 0.5f;

    private bool _isCharging = false;

    public delegate void HPchange(float value);
    public event HPchange OnHPchange;
    public event HPchange OnChargeChange;

    private RoadCheck _roadCheck;
    private GameManager _gameManager;


    private void Start()
    {
        _roadCheck = FindAnyObjectByType<RoadCheck>();
        _roadCheck.OnTreesSpawn += CountHealthCoef;
        _gameManager = FindAnyObjectByType<GameManager>();
        _gameManager.OnEnd += Restart;

    }
    public void Update()
    {

        if(_isCharging)
        {
            ScaleProjectile();
        }
    }

    private void SpawnProjectile()
    {
        Vector3 position = transform.position + transform.forward*_spawnDistance;
        _tempProjectile = Instantiate(projectilePrefab, position, transform.rotation);
        _tempProjectile.transform.localScale = new Vector3(startSize, startSize, startSize);

        OnHPchange?.Invoke(startSize / shootToHPCoef);


        _tempProjectile.transform.SetParent(transform.parent.transform);
    }

    private void ScaleProjectile()
    {
        if (_tempProjectileSize < (maxSize-startSize))
        {
            _tempProjectileSize += Time.deltaTime * chargeSpeed;

            OnChargeChange?.Invoke(_tempProjectileSize); 
            
            float tempScale = startSize + _tempProjectileSize; 
            _tempProjectile.transform.localScale = new Vector3(tempScale, tempScale, tempScale);

            OnHPchange?.Invoke(Time.deltaTime * chargeSpeed / shootToHPCoef);
        }
    }


    public void PointerDown()
    {
        _isCharging = true;
        SpawnProjectile();
    }

    public void PointerUp()
    {
        _isCharging =false;
        _tempProjectileSize = 0;

        OnChargeChange?.Invoke(_tempProjectileSize);

        _tempProjectile.GetComponent<Projectile>().ProjectileActivation(projectileSpeed, projectileLife); //Activating projectile
        _tempProjectile.transform.SetParent(projectileBin);
    }

    private void CountHealthCoef(int value)
    {
        shootToHPCoef = 0.1f * value / 0.8f;
    }

    private void OnDestroy()
    {
        _roadCheck.OnTreesSpawn -= CountHealthCoef;
        _gameManager.OnEnd -= Restart;
    }

    private void Restart()
    {
        PointerUp();
        for (int i = 0; i < projectileBin.childCount; i++)
        {
            Transform sibling = projectileBin.GetChild(i);
            Destroy(sibling.gameObject);
        }

    }

}
