using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Unity.AI.Navigation.Samples
{
    /// <summary>
    /// Use physics raycast hit from mouse click to set agent destination 
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class ClickToMove : MonoBehaviour
    {
        NavMeshAgent m_Agent;
        RaycastHit m_HitInfo = new RaycastHit();
        Animator m_Animator;
        private bool checkRemainingDistance = false;

        [HideInInspector]
        public bool playersTurn = true;
        public float timeBetweenMoves = 4f;
        private float currentMoveTime;

        [SerializeField] public int speed;

        void Start()
        {
            m_Agent = GetComponent<NavMeshAgent>();
            m_Animator = GetComponent<Animator>();
            currentMoveTime = timeBetweenMoves;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftAlt) && playersTurn)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo))
                    m_Agent.destination = m_HitInfo.point;
                StartCoroutine(CheckDestination());
            }

            if (m_Agent.velocity.magnitude != 0f)
            {
                m_Animator.SetBool("Running", true);
            }
            else
            {
                m_Animator.SetBool("Running", false);
            }

            if (checkRemainingDistance)
            {
                if (m_Agent.remainingDistance <= m_Agent.stoppingDistance + 0.1f)
                {
                    playersTurn = false;
                    checkRemainingDistance = false;
                }
                else return;
            }

            if (!playersTurn)
            {
                currentMoveTime -= Time.deltaTime;
                if (currentMoveTime <= 0.0f)
                {
                    playersTurn = true;
                    currentMoveTime = timeBetweenMoves;
                }
            }
        }

        IEnumerator CheckDestination()
        {
            yield return new WaitForSeconds(0.5f);
            checkRemainingDistance = true;
        }

        private void OnAnimatorMove()
        {
            if (m_Animator.GetBool("Running"))
            {
                m_Agent.speed = (m_Animator.deltaPosition / Time.deltaTime).magnitude * speed;
            }
        }
    }
}