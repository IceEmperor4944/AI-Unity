using System.Collections;
using System.ComponentModel;
using UnityEngine;

public class HitState : AIState
{
    float hitTimer;

    public HitState(StateAgent agent) : base(agent)
    {
        CreateTransition(nameof(ChaseState))
            .AddCondition(agent.timer, Condition.Predicate.LessOrEqual, 0);
    }

    public override void OnEnter()
    {
        hitTimer = 1;
        
        agent.timer.value = 2;
        agent.movement.Stop();
        agent.animator.SetTrigger("Hit");
    }

    public override void OnUpdate()
    {
        //look at enemy
        Vector3 direction = agent.enemy.transform.position - agent.transform.position;
        agent.transform.rotation = Quaternion.Lerp(agent.transform.rotation, Quaternion.LookRotation(direction, Vector3.up), Time.deltaTime * 5);

        hitTimer -= Time.deltaTime;
    }

    public override void OnExit()
    {
        //
    }
}