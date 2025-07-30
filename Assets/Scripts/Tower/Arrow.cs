using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Arrow : Projectile
{
    [SerializeField] private int damage = 10;
    protected override void MoveTowards()
    {
        Vector3 direction = (target.position - this.transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        transform.forward = direction;
    }

    protected override void CollisionEffect(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
        Destroy(this.gameObject);
    }
}
