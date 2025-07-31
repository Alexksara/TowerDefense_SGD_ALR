using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Arrow : Projectile
{
    [SerializeField] private int M_damage = 10;

    //Summary
    //
    protected override void CollisionEffect(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(M_damage);
        }
        Destroy(this.gameObject);
    }
}
