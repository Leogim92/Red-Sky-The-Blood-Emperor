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
    public ParticleSystem particle;
    void Start()
    {
        
        initialPosition = this.transform.position;
        GetComponent<ParticleSystem>().Play();
        emission = particle.emission;
        emission.enabled = false;

        this.FSM.ChangeState(new PatrollingState(this, "enemy_walk"));

    }

    // Update is called once per frame
    void Update()
    {
        FSM.Tick();
    }

}
