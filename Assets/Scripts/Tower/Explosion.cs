using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField ]private int damage;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
        Destroy(this.gameObject);
    }
}
