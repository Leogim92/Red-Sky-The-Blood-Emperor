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
    public List<GameObject> positionsToBe = new List<GameObject>(); //Por que gameobjects? Porque todos eles tem transform
    public bool shouldReturnToFirstPosition = false; //Booleano para dizer se o personagem deve retornar ao começo.
    //Interessante seria colocar outro bool para definir se o personagem retorna ao começo depois de passar pelo ultimo
    //objeto da lista ou se retorna pelos objetos da lista.

    public float movementSpeed = 5f;
    public float distanceToPatrol = 5f;
    void Start()
    {
        emission = GetComponentInChildren<ParticleSystem>().emission;
        GetComponentInChildren<ParticleSystem>().Play();
        emission.enabled = false;

        initialPosition = this.transform.position;

        //this.FSM.ChangeState(new PatrollingState(this, "enemy_walk"));
        this.FSM.ChangeState(new DirectionalPatrolState(this, "enemy_walk"));

    }

    void Update()
    {
        FSM.Tick();
    }

}
