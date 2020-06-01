using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[System.Serializable]
public class AIBrain : MonoBehaviour
{
    internal StateMachine FSM = new StateMachine();
    internal ParticleSystem.EmissionModule emission;

    public enum Behaviour { patrol, directionalPatrol, vigilance, agressive };
    public Behaviour aiBehaviour;

    //custom inspector properties
    [HideInInspector] public List<Transform> patrolPositions;
    [HideInInspector] public bool shouldReturnToFirstPosition = false;
    [HideInInspector] public float movementSpeed = 5f;
    [HideInInspector] public float distanceToPatrol = 5f;
    [HideInInspector] public float distanceToAttack = 10f;

    void Start()
    {
        emission = GetComponentInChildren<ParticleSystem>().emission;
        emission.enabled = false;
        AIInitialBehaviour();
    }
    void Update()
    {
        FSM.Tick();
    }
    private void AIInitialBehaviour()
    {
        switch (aiBehaviour)
        {
            case Behaviour.patrol:
                this.FSM.ChangeState(new PatrollingState(this, "enemy_walk"));
                break;
            case Behaviour.directionalPatrol:
                this.FSM.ChangeState(new DirectionalPatrolState(this, "enemy_walk"));
                break;
            case Behaviour.vigilance:
                //this.FSM.ChangeState(new VigilanceState(this, "enemy_vigilance"));
                break;
            case Behaviour.agressive:
                //this.FSM.ChangeState(new AgressiveState(this, "enemy_agressive"));
                break;
        }
        
    }
}
