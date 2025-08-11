using UnityEngine;

public class Rock : Projectile
{

    protected override void CollisionEffect(Collider other)
    {
        Destroy(other.gameObject);
        Destroy(this.gameObject);
    }
}
