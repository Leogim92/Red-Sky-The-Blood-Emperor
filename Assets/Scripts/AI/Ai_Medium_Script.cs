using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[System.Serializable]
public class Ai_Medium_Script : MonoBehaviour
{
    internal StateMachine FSM = new StateMachine();
    internal ParticleSystem.EmissionModule emission;
    [HideInInspector] public List<Transform> positionsToBe;
    public bool shouldReturnToFirstPosition = false; //Booleano para dizer se o personagem deve retornar ao começo.
    //Interessante seria colocar outro bool para definir se o personagem retorna ao começo depois de passar pelo ultimo
    //objeto da lista ou se retorna pelos objetos da lista.

    public float movementSpeed = 5f;
    public float distanceToPatrol = 5f;
    public enum Behaviour { patrol, directionalPatrol, alternativeDPatrol, vigilance, agressive};
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
            case Behaviour.directionalPatrol:
                this.FSM.ChangeState(new DirectionalPatrolState(this, "enemy_walk"));
                break;
            case Behaviour.alternativeDPatrol:
                this.FSM.ChangeState(new AlternativeDirectionalPatrolState(this, "enemy_walk"));
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
