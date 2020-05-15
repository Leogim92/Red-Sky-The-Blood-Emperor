using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Ai_Medium_Script : MonoBehaviour
{
    internal StateMachine FSM = new StateMachine();
    internal Vector2 initialPosition;
    internal ParticleSystem.EmissionModule emission;

    public float movementSpeed = 5f;
    public float distanceToPatrol = 5f;
    void Start()
    {
        emission = GetComponentInChildren<ParticleSystem>().emission;
        GetComponentInChildren<ParticleSystem>().Play();
        emission.enabled = false;

        initialPosition = this.transform.position;

        this.FSM.ChangeState(new PatrollingState(this, "enemy_walk"));

    }

    void Update()
    {
        FSM.Tick();
    }

}
