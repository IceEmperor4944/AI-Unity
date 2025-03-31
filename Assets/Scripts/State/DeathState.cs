using System.Collections;
using System.ComponentModel;
using UnityEngine;

public class DeathState : AIState
{
    float deathTimer;
    bool hasAttacked;

    public DeathState(StateAgent agent) : base(agent)
    {
        //
    }

    public override void OnEnter()
    {
        deathTimer = 1.5f;

        agent.timer.value = 2.5f;
        agent.movement.Stop();
        agent.animator.SetTrigger("Death");
    }

    public override void OnUpdate()
    {
        //look at enemy
        Vector3 direction = agent.enemy.transform.position - agent.transform.position;
        agent.transform.rotation = Quaternion.Lerp(agent.transform.rotation, Quaternion.LookRotation(direction, Vector3.up), Time.deltaTime * 5);

        deathTimer -= Time.deltaTime;
        if (deathTimer <= 0)
        {
            GameObject.Destroy(agent.gameObject);
        }
    }

    public override void OnExit()
    {
        //
    }
}