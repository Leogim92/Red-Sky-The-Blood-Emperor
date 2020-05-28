using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : IState
{
    private GameObject player;
    private Ai_Medium_Script ai;
    private string animationName;

    public AttackingState(Ai_Medium_Script ai, string animationName)
    {
        this.ai = ai;
        this.animationName = animationName;
    }
    public void Enter()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Tick()
    {
        if (Vector2.Distance(player.transform.position, ai.transform.position) >= 10f)
        {
            ai.FSM.SwitchToPreviousState();
        }
        else
        {
            ai.emission.enabled = true;
            Vector2 lookAt = player.transform.position - ai.transform.position;
            ai.transform.right = lookAt.normalized;
        }
        
    }
    public void Exit()
    {
        ai.emission.enabled = false;
    }
}
