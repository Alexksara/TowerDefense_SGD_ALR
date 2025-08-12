using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private Animator animator;
    [SerializeField] private Transform endPoint;
    [SerializeField] private string animatorParam_isWalking;
    [SerializeField] protected int damageValue = 10;
    [SerializeField] protected int healthValue = 100;
    [SerializeField] protected int moneyValue = 1;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            if(navAgent.hasPath || navAgent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                ReachedEnd();
            }
        }
        if(healthValue <= 0)
        {
            Die();
        }
    }

    protected virtual void ReachedEnd()
    {
        animator.SetBool(animatorParam_isWalking, false);
        GameManager.Instance.playerHealth.TakeDamage(damageValue);
        Destroy(gameObject);
    }

    public void Initialize(Transform end)
    {
        endPoint = end;
        navAgent.SetDestination(endPoint.position);
        animator.SetBool(animatorParam_isWalking, true);
    }

    public void TakeDamage(int dmg)
    {
        healthValue -= dmg;
        Debug.Log($"Took {dmg} damage health is now {healthValue}");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("End Point"))
        {
            ReachedEnd();
        }
    }

    public void ChangeSpeed(float addSpeed)
    {
        navAgent.speed += addSpeed;
    }

    public virtual void Die()
    {
        GameManager.Instance.AddMoney(moneyValue);
        GameManager.Instance.IncrimentEnemiesKilled();
        Destroy(this.gameObject);
    }
}
