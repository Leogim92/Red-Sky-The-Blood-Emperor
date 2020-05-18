﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternativeDirectionalPatrolState : IState 
{
    private Ai_Medium_Script ai;
    private string animationName;
    private GameObject player;

    private List<Transform> patrolPositions = new List<Transform>();
    private int currentGoal=0;
    bool arePositionsAdded = false;

    public AlternativeDirectionalPatrolState(Ai_Medium_Script ai, string animationName)
    {
        this.ai = ai;
        this.animationName = animationName;
    }

    public void Enter()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (!arePositionsAdded)
        {
            AddPositionsToTheList();
            arePositionsAdded = true;
        }
    }

    private void AddPositionsToTheList()
    {
        foreach (Transform position in ai.positionsToBe)
        {
            patrolPositions.Add(position);
        }
    }

    public void Tick()
    {
        if (Vector2.Distance(player.transform.position, ai.transform.position) < 10f)
        {
            ai.FSM.ChangeState(new AttackingState(ai, "Enemy_attack"));
        }
        else 
        {
            if (Vector2.Distance(ai.transform.position, patrolPositions[currentGoal].position) > 0.6f)
            {
                LookAtPatrolPosition(patrolPositions[currentGoal].position);
                ai.transform.position = Vector2.MoveTowards(ai.transform.position, patrolPositions[currentGoal].position, ai.movementSpeed * Time.deltaTime);
            }
            else if (currentGoal < patrolPositions.Count - 1)
            {
                currentGoal++;
            }
            else if (ai.shouldReturnToFirstPosition)
            {
                patrolPositions.Reverse();
                currentGoal = 0;
            }
            else
                Debug.Log("End of Patrol"); //go to idle state
        }

    }
    private void LookAtPatrolPosition(Vector2 patrolPosition)
    {
        Vector2 lookAt = (patrolPosition - new Vector2(ai.transform.position.x, ai.transform.position.y));
        ai.transform.right = lookAt.normalized;
    }

    public void Exit()
    {

    }

  
}
