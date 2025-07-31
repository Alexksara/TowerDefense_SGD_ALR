using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField ]private int M_damage;

    //Summary
    //all enemies within the explosion radius take damage equal to the damage value and the explosion is destroyed
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(M_damage);
        }
        Destroy(this.gameObject);
    }
}
