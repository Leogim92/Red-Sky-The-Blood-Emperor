using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Ai_Medium_Script : MonoBehaviour
{
    internal StateMachine FSM = new StateMachine();
    internal ParticleSystem.EmissionModule emission;

    public float movementSpeed = 5f;
    public float distanceToPatrol = 5f;
    public enum Behaviour { patrol, vigilance, agressive};
    public Behaviour aiBehaviour;
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
            case Behaviour.vigilance:
                //this.FSM.ChangeState(new VigilanceState(this, "enemy_vigilance"));
                break;
            case Behaviour.agressive:
                //this.FSM.ChangeState(new AgressiveState(this, "enemy_agressive"));
                break;
        }
        
    }
}
