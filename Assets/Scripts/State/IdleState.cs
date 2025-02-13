using UnityEngine;

public class IdleState : AIState
{
    float timer;

    public IdleState(StateAgent agent) : base(agent)
    {
        //
    }

    public override void OnEnter()
    {
        timer = Random.Range(1, 3);
    }

    public override void OnUpdate()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            agent.stateMachine.SetState(nameof(PatrolState));
        }
    }

    public override void OnExit()
    {
        //
    }
}