using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Arrow : Projectile
{
    protected override void MoveTowards()
    {
        {
            Vector3 direction = (target.position - this.transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
            transform.forward = direction;
        }
    }

    protected override void CollisionEffect(Collider other)
    {
        Debug.Log("Destroyed Enemy");
        Destroy(other.gameObject);
    }
}
