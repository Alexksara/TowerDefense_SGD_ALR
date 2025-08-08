using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    private ParticleSystem M_particleSystem;
    [SerializeField] private float destroyIn = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this.gameObject, destroyIn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
