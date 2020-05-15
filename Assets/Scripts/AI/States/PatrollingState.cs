using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingState : IState
{
    private Vector2 patrolRouteStartPos;
    private bool changeDirection=true;
    private Ai_Medium_Script ai;
    private string animationName;
    private GameObject player;
    private float areaToPatrol = 20f;
    
    public PatrollingState(Ai_Medium_Script ai, string animationName)
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
        if(Vector2.Distance(player.transform.position,ai.transform.position) < 10f)
        {
            ai.FSM.ChangeState(new AttackingState(ai, "Enemy_attack"));
        }
        else
        {
            MoveAI();
            if (changeDirection==true)
            {
                GetNewDirection();
            }
            else
            {
                CheckForDistancePatrolled();
            }

        }

    }
    private void MoveAI()
    {
        ai.transform.position += ai.transform.right * ai.movementSpeed * Time.deltaTime;
    }
    private void GetNewDirection()
    {
        patrolRouteStartPos = ai.transform.position;
        Vector2 lookAt = (UnityEngine.Random.insideUnitCircle * areaToPatrol) - new Vector2(ai.transform.position.x, ai.transform.position.y);
        ai.transform.right = lookAt.normalized;
        changeDirection = false;
    }
    private void CheckForDistancePatrolled()
    {
        if(Vector2.Distance(patrolRouteStartPos,ai.transform.position) > ai.distanceToPatrol)
        {
            changeDirection = true;
        }
    }

    public void Exit()
    {
        
    }
}
