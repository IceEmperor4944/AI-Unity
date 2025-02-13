using UnityEngine;

public abstract class AIState
{
    protected StateAgent agent;
    StateMachine stateMachine = new StateMachine();

    public AIState(StateAgent agent)
    {
        this.agent = agent;
    }

    public string Name => GetType().Name;

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();
}