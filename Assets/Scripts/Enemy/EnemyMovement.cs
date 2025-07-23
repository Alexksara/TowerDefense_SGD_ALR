using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    [SerializeField] private Transform endPoint;
    [SerializeField] private string animatorParam_isWalking;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent.SetDestination(endPoint.position);
        animator.SetBool(animatorParam_isWalking, true);

    }

    // Update is called once per frame
    void Update()
    {
        if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if(agent.hasPath || agent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                animator.SetBool(animatorParam_isWalking, false);
            }
            
        }
       
    }

    
}
