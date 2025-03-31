using System.Collections;
using System.ComponentModel;
using UnityEngine;

public class AttackState : AIState
{
    float attackTimer;
    bool hasAttacked;

    public AttackState(StateAgent agent) : base(agent)
    {
        CreateTransition(nameof(IdleState))
            .AddCondition(agent.health, Condition.Predicate.Greater, 0)
            .AddCondition(agent.enemySeen, false);

        CreateTransition(nameof(ChaseState))
            .AddCondition(agent.timer, Condition.Predicate.LessOrEqual, 0);
    }

    public override void OnEnter()
    {
        attackTimer = 1;
        hasAttacked = false;

        agent.timer.value = 2;
        agent.movement.Stop();
        agent.animator.SetTrigger("Attack");
    }

    public override void OnUpdate()
    {
        //look at enemy
        Vector3 direction = agent.enemy.transform.position - agent.transform.position;
        agent.transform.rotation = Quaternion.Lerp(agent.transform.rotation, Quaternion.LookRotation(direction, Vector3.up), Time.deltaTime * 5);

        attackTimer -= Time.deltaTime;
        if (!hasAttacked && attackTimer <= 0)
        {
            hasAttacked = true;
            agent.Attack();
        }
    }

    public override void OnExit()
    {
        //
    }
}