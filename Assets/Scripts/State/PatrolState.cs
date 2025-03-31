using System.ComponentModel;
using UnityEngine;

public class PatrolState : AIState
{
    public PatrolState(StateAgent agent) : base(agent)
    {
        CreateTransition(nameof(IdleState))
            .AddCondition(agent.destinationDistance, Condition.Predicate.Less, 0.5f)
            .AddCondition(agent.enemySeen, false);

        CreateTransition(nameof(ChaseState))
            .AddCondition(agent.enemySeen, true);
    }

    public override void OnEnter()
    {
        agent.movement.Destination = NavNode.GetRandomNavNode().transform.position;
        agent.movement.Resume();
    }

    public override void OnUpdate()
    {
        // rotate towards movement direction
        if (agent.movement.Direction != Vector3.zero)
        {
            agent.transform.rotation = Quaternion.Lerp(agent.transform.rotation, 
                Quaternion.LookRotation(agent.movement.Direction, Vector3.up), Time.deltaTime * 5);
        }
    }

    public override void OnExit()
    {
        //
    }
}