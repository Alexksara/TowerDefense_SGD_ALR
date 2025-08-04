using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    [SerializeField] private Transform endPoint;
    [SerializeField] private string animatorParam_isWalking;
    [SerializeField] private int damageValue = 10;
    [SerializeField] private int healthValue = 100;
    [SerializeField] private int moneyValue = 1;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if(agent.hasPath || agent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                ReachedEnd();
            }
        }
        if(healthValue <= 0)
        {
            GameManager.Instance.AddMoney(moneyValue);
            Destroy(this.gameObject);
        }
    }

    private void ReachedEnd()
    {
        animator.SetBool(animatorParam_isWalking, false);
        GameManager.Instance.playerHealth.TakeDamage(damageValue);
        Destroy(gameObject);
    }

    public void Initialize(Transform end)
    {
        endPoint = end;
        agent.SetDestination(endPoint.position);
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

}
