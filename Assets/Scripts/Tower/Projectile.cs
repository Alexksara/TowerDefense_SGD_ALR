using UnityEngine;

[RequireComponent (typeof(Collider))]
public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected int damage = 10;
    [SerializeField] protected float speed = 20f;
    [SerializeField] protected float lifetime = 3f;
    protected Transform target;

    private void Start()
    {
        Destroy(this.gameObject,lifetime);
    }

    private void Update()
    {
        if (target != null)
        {
            MoveTowards();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void SetTarget(Transform inputTarget)
    {
        target = inputTarget;
    }

    protected abstract void MoveTowards();
    protected abstract void CollisionEffect(Collider other);
    

    private void OnTriggerEnter(Collider other)
    {
        if ((other.transform == target))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                CollisionEffect(other);
            }
        }
        Destroy(this.gameObject);
    }
}
