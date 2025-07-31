using UnityEngine;

public class CannonBall : Projectile
{
    [SerializeField] private int M_damage = 10;
    [SerializeField] private GameObject M_explosionPrefab;

    protected override void CollisionEffect(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(M_damage);
            //Destroy(other.gameObject);
        }
        Instantiate(M_explosionPrefab, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
} 
