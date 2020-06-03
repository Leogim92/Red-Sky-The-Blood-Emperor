using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalPatrolState : IState 
{
    private AIBrain ai;
    private string animationName;
    private GameObject player;

    private List<Transform> patrolPositions = new List<Transform>();
    private int currentGoal=0;

    public DirectionalPatrolState(AIBrain ai, string animationName)
    {
        this.ai = ai;
        this.animationName = animationName;
        AddPositionsToTheList();
    }
    

    public void Enter()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void AddPositionsToTheList()
    {
        foreach (Transform position in ai.patrolPositions)
        {
            patrolPositions.Add(position);
        }
    }

    public void Tick()
    {
        if (Vector2.Distance(player.transform.position, ai.transform.position) < ai.distanceToAttack)
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
            else if (currentGoal < patrolPositions.Count-1)
            {
                currentGoal++;
                ai.FSM.ChangeState(new IdleAIState(ai, "Enemy_idle", this));
            }
            else if (ai.patrolLoop)
            {
                currentGoal = 0;
                ai.FSM.ChangeState(new IdleAIState(ai, "Enemy_idle", this));
            }
            else
            {
                patrolPositions.Reverse();
                currentGoal = 0;
                ai.FSM.ChangeState(new IdleAIState(ai, "Enemy_idle", this));
            }
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
