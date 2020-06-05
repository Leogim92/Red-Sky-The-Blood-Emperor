using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public string Name;
    public float Speed = 3f; //Usado para definir a velocidade do player

    public TextMeshProUGUI CurrentStateText; //Usado para apresentar a forma de movimentação do player

    public static PlayerInputActions controls; //Aqui recebemos o input pelo novo sistema de input
    internal StateMachine FSM = new StateMachine(); //Fazendo o FSM
    internal Animator anim; //Controlando o Animator
    internal Rigidbody2D rb;
    internal ParticleSystem particle;
    internal HPCount hp;

    private Vector2 moveInput;
    internal float xInput { get; set; } //Isto é definido em Update como uma parte do vetor recebido do input
    internal float yInput { get; set; }

    private Vector2 aimInput;


    private void Start()
    {
        Name = "Carlos";
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        this.FSM.ChangeState(new IdleState(this, "idle_down"));

        particle = GetComponentInChildren<ParticleSystem>();
        particle.Stop();

        hp = GetComponent<HPCount>();
    }

    private void Awake()
    {
        controls = new PlayerInputActions();

        //PELO NEW INPUT SYSTEM RECEBEMOS AQUI ALGUMAS COISAS
        controls.PlayerMovement.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>(); //Recebendo para onde mover
        controls.PlayerMovement.Move.canceled += ctx => moveInput = Vector2.zero; //Parar de mover se soltar

        controls.PlayerMovement.Aim.performed += ctx => aimInput = ctx.ReadValue<Vector2>(); //Recebendo para onde mirar

        controls.PlayerMovement.Attack.performed += ctx => particle.Play(); //Enquanto segurar o ataque atacar
        controls.PlayerMovement.Attack.canceled += ctx => particle.Stop(); //Quando soltar, parar.

    }

    private void OnEnable() => controls.Enable(); //Não esquecer disso, sem isso não funcionam os controles.

    private void OnDisable() => controls.Disable();

    private void Update()
    {
        xInput = moveInput.x;
        yInput = moveInput.y;
        //Debug.Log(moveInput);

        FSM.Tick();
    }


    /// <summary> Verifica se a Animação atual já acabou de correr. </summary>
    internal bool CurrentAnimationDone()
    {
        return anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1;
    }

    public void DamageTaken()
    {
        //Debug.Log("Tomei dano");
    }

    public void Death()
    {

    }

    private void OnParticleCollision(GameObject other)
    {
        //Debug.Log("Colidi com " + other.name);
        if(other.name == "PistolParticle")
        {
            hp.Damaged(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Colidi com " + other.name);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Estou a colidir com " + other.name);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Sai da colisão com " + other.name);
    }
}