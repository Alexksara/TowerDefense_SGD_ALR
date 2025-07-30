using UnityEngine;

public class CannonBall : Projectile
{
    [SerializeField] private GameObject explosionPrefab;

    protected override void CollisionEffect(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            //Destroy(other.gameObject);
        }
        Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
    protected override void MoveTowards()
    {
        Vector3 direction = (target.position - this.transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        transform.forward = direction;
    }

} 
