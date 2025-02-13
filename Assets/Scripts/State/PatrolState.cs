using System.ComponentModel;
using UnityEngine;

public class PatrolState : AIState
{
    Vector3 destination = Vector3.zero;

    public PatrolState(StateAgent agent) : base(agent)
    {
        //
    }

    public override void OnEnter()
    {
        destination = NavNode.GetRandomNavNode().transform.position;
        agent.movement.Destination = destination;
        agent.movement.Resume();
    }

    public override void OnExit()
    {
        Vector3 direction = agent.transform.position - destination;
        direction.y = 0;
        float distance = direction.magnitude;
        if (distance <= 0.25f)
        {
            agent.stateMachine.SetState(nameof(IdleState));
        }
    }

    public override void OnUpdate()
    {
        //
    }
}