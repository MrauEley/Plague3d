using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Projectile : MonoBehaviour
{
    private Rigidbody _rb;
    private float _lifetime=4; //sets in ProjectileActivation
    private bool isActive = false;

    [SerializeField] private float scaleOnImpact = 2;
    private bool _isFirstImpact = true;
    [SerializeField] private float lifetimeAfterImpact = 0.02f;

    [SerializeField] private ParticleSystem _particlesOnImpact;
    ShapeModule _shapeModule;

    
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _shapeModule = _particlesOnImpact.shape;  
    }

    private void Update()
    {
        if (isActive)
            LifeTimer();
    }

    private void LifeTimer()
    {
        _lifetime -= Time.deltaTime;

        if (_lifetime < 0)
        {
            Destroy(gameObject);
        }
    }

    public void ProjectileActivation(float projSpeed, float projLife)
    {
        _rb.velocity = transform.forward * projSpeed;
        isActive = true;

        _shapeModule.radius = transform.localScale.x;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Tree")
        {
            OnFirstImpact();
            RoadCheck.instance.TreeRemove(other.gameObject);
            other.gameObject.GetComponent<TreeScript>().TreeActivation();
        }
    }

    private void OnFirstImpact()
    {
        if(_isFirstImpact)
        {
            transform.localScale *= scaleOnImpact;
            _isFirstImpact = false;
            _lifetime = lifetimeAfterImpact;

            gameObject.GetComponent<MeshRenderer>().enabled = false;

            ParticleSystem tempParticle = Instantiate(_particlesOnImpact, transform.position, Quaternion.identity);
            tempParticle.transform.SetParent(transform.parent);
        }
    }



}
