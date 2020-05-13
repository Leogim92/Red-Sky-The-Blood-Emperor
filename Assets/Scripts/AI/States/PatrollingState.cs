using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingState : IState
{
    private Ai_Medium_Script ai;
    private string animationName;
    private Rigidbody2D rb;
    private GameObject player;
    public float areaToPatrol = 20f;

    public PatrollingState(Ai_Medium_Script ai, string animationName)
    {
        this.ai = ai;
        this.animationName = animationName;
    }
    public void Enter()
    {
        rb = ai.gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        player = GameObject.FindGameObjectWithTag("Player");

    }

    public void Tick()
    {
        if(Vector2.Distance(player.transform.position,rb.transform.position) < 10f)
        {
            ai.FSM.ChangeState(new AttackingState(ai, "Enemy_attack"));
        }
        else
        {// Esse código tem de ser feito de x em x períodos de tempo.

            Vector2 lookAt = (ai.initialPosition + Random.insideUnitCircle * areaToPatrol) - new Vector2(rb.transform.position.x, rb.transform.position.y); //Randomizando para onde ele vai olhar
            rb.transform.right = lookAt.normalized;

            rb.AddForce(rb.transform.right * 100);
        }

    }

    public void Exit()
    {
        rb.velocity = Vector2.zero;
    }
}
