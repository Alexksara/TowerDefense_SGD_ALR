using UnityEngine;

[RequireComponent (typeof(Collider))]
public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float M_speed = 8f;
    [SerializeField] protected float M_lifetime = 3f;
    protected Transform M_target;

    private void Start()
    {
        Destroy(this.gameObject,M_lifetime);
    }

    private void Update()
    {
        if (M_target != null)
        {
            MoveTowards();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    //Summary
    // sets the target enemy to the argument passed through
    public void SetTarget(Transform inputTarget)
    {
        M_target = inputTarget;
    }

    //Summary
    //default move towards functionality, moves in a straight line towards target, can be overriden
    protected virtual void MoveTowards()
    {
        Vector3 direction = (M_target.position - this.transform.position).normalized;
        transform.position += direction * M_speed * Time.deltaTime;
        transform.forward = direction;
    }
    //Summary
    //each projectile has unique functionality when they collide with the enemy
    protected abstract void CollisionEffect(Collider other);

    //Summary
    //when hitting the enemy we make sure its the target, and that its an enemy, then call its collision then destroy the projectile
    private void OnTriggerEnter(Collider other)
    {
        if ((other.transform == M_target))
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
