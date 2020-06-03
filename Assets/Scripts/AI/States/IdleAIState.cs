using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAIState : IState
{
    private AIBrain ai;
    private string animationName;
    private GameObject player;
    IState patrolState;
    float timeToWait;
    public IdleAIState(AIBrain ai, string animationName ,IState state)
    {
        this.ai = ai;
        this.animationName = animationName;
        patrolState = state;
    }
    public void Enter()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timeToWait = Time.time + ai.timeToWait;
        
    }
    public void Tick()
    {
        if (Vector2.Distance(player.transform.position, ai.transform.position) < ai.distanceToAttack)
        {
            ai.FSM.ChangeState(new AttackingState(ai, "Enemy_attack"));
        }
        else if (Time.time >= timeToWait) 
        {
            ai.FSM.ChangeState(patrolState);
        }
    }
    public void Exit()
    {
        
    }
}
