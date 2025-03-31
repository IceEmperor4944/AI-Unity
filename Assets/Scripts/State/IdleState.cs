using UnityEngine;

public class IdleState : AIState
{
    ValueCondition<float> timerCheck;
    public IdleState(StateAgent agent) : base(agent)
    {
        CreateTransition(nameof(PatrolState))
            .AddCondition(agent.timer, Condition.Predicate.LessOrEqual, 0)
            .AddCondition(agent.enemySeen, false);

        CreateTransition(nameof(ChaseState))
            .AddCondition(agent.enemySeen, true);
    }

    public override void OnEnter()
    {
        agent.timer.value = Random.Range(1, 3);
        agent.movement.Stop();
    }

    public override void OnUpdate()
    {
        //
    }

    public override void OnExit()
    {
        //
    }
}