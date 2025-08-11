using UnityEngine;

public class Rock : Projectile
{

    protected override void CollisionEffect(Collider other)
    {
        other.GetComponent<Enemy>().Die();
        Destroy(this.gameObject);
    }
}
