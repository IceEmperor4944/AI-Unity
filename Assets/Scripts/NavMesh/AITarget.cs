using Unity.AI.Navigation.Samples;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AITarget : MonoBehaviour
{
    public Transform Target;
    public float AttackDistance;

    private NavMeshAgent agent;
    private Animator animator;
    private float distance;
    private Vector3 startingPoint;
    private bool pathCalculate = true;

    [SerializeField] public int speed;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        startingPoint = transform.position;
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        animator.speed = Random.Range(0.6f, 1.6f);
        agent.avoidancePriority = Random.Range(10, 65);
        float radiusValue = Random.Range(0.5f, 0.0f);
        agent.radius = Mathf.Round(radiusValue * 10) * 0.1f;
    }

    void Update()
    {
        if (!Target.GetComponent<ClickToMove>().playersTurn)
        {
            agent.isStopped = false;
            animator.SetBool("Walking", true);
            distance = Vector3.Distance(agent.transform.position, Target.position);
            if (distance < AttackDistance)
            {
                agent.isStopped = true;
                animator.SetBool("Attack", true);
            }
            else
            {
                agent.isStopped = false;
                if (agent.hasPath && pathCalculate)
                {
                    agent.destination = startingPoint;
                    pathCalculate = false;
                }
                else
                {
                    animator.SetBool("Attack", false);
                    animator.SetBool("Walking", true);
                    agent.destination = Target.position;
                    pathCalculate = true;
                }
            }
        }
        else if(Target.GetComponent<ClickToMove>().playersTurn)
        {
            agent.isStopped = true;
            animator.SetBool("Walking", false);
        }
    }

    private void OnAnimatorMove()
    {
        if (animator.GetBool("Attack") == false)
        {
            agent.speed = (animator.deltaPosition / Time.deltaTime).magnitude * speed;
        }
    }
}