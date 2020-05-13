using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : IState
{
    private GameObject player;
    private Ai_Medium_Script ai;
    private string animationName;
    private Rigidbody2D rb;

    public AttackingState(Ai_Medium_Script ai, string animationName)
    {
        this.ai = ai;
        this.animationName = animationName;
    }
    public void Enter()
    {
        rb = ai.gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Tick()
    {
        if (Vector2.Distance(player.transform.position, rb.transform.position) >= 10f)
        {
            ai.FSM.ChangeState(new PatrollingState(ai, "Enemy_attack"));
        }
        else
        {
            ai.emission.enabled = true;
            Vector2 lookAt = player.transform.position - rb.transform.position;
            rb.transform.right = lookAt.normalized;
        }
        
    }

    public void Exit()
    {
        ai.emission.enabled = false;
    }
}
