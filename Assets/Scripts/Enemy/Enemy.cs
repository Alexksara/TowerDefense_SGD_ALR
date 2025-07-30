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
    
}
