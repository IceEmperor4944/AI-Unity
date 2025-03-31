using System.ComponentModel;
using UnityEngine;

public class ChaseState : AIState
{
    public ChaseState(StateAgent agent) : base(agent)
    {
        CreateTransition(nameof(IdleState))
            .AddCondition(agent.enemySeen, false);

        CreateTransition(nameof(AttackState))
            .AddCondition(agent.enemyDistance, Condition.Predicate.LessOrEqual, 1.5f);
    }

    public override void OnEnter()
    {
        agent.movement.MaxSpeed *= 2;
        agent.movement.Resume();
    }

    public override void OnUpdate()
    {
        // set destination to enemy position
        agent.movement.Destination = agent.enemy.transform.position;
        // rotate towards movement direction
        if (agent.movement.Direction != Vector3.zero)
        {
            agent.transform.rotation = Quaternion.Lerp(agent.transform.rotation, 
                Quaternion.LookRotation(agent.movement.Direction, Vector3.up), Time.deltaTime * 5);
        }
    }

    public override void OnExit()
    {
        agent.movement.MaxSpeed /= 2;
    }
}