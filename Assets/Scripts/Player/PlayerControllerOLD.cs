using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerControllerOLD : MonoBehaviour
{
    public string Name;
    public float Speed;
    public TextMeshProUGUI CurrentStateText;

    internal StateMachine FSM = new StateMachine();
    internal Animator animator;
    internal Rigidbody2D rb;
    internal float xInput { get; set; }
    internal float yInput { get; set; }

    private void Start()
    {
        Name = "Carlos";
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //this.FSM.ChangeState(new IdleState(this, "player_idle"));

    }

    private void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        FSM.Tick();
        Flip();
    }

    private void Flip()
    {
        if (xInput != 0)
        {
            GetComponent<SpriteRenderer>().flipX = xInput == -1 ? true : false;
        }
        if (yInput != 0)
        {
            GetComponent<SpriteRenderer>().flipY = yInput == -1 ? true : false;
        }
    }

    /// <summary> Verifica se a Animação atual já acabou de correr. </summary>
    internal bool CurrentAnimationDone()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1;
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Colidi com " + other.name);
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