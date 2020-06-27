using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Ok tentei de tudo com o animator, de blend a sub-state machine. Vai ser do jeito feio. HARDCODED
//Talvez fazer a troca de string to hash seja uma forma melhor de comparar essas strings
//Atenção! A deadzone para joystick foi aumentada para evitar pequenos erros no direcional.
//Nas direções temos duas formas de receber a direção da animação. Tanto faz uma ou outra neste momento.

public class RunningState : IState {

    private PlayerController player;
    private string animationName;
    private string nextAnimation = "idle_down"; //Caso de bug

    public RunningState (PlayerController player, string animationName) {
        this.player = player;
        this.animationName = animationName;
    }
    public void Enter () {
        player.CurrentStateText.text = "Running";
    }

    public void Tick () {
        if (player.xInput == 0 && player.yInput == 0) {
            player.rb.velocity = Vector2.zero;

            player.FSM.ChangeState(new IdleState(player, nextAnimation));
        }

        //Direcionais, esquerda, direita....
        else if(player.xInput < 0 && Mathf.Round(player.yInput) == 0)
        //else if (player.rb.velocity.x < 0 && Mathf.Round(player.rb.velocity.y) == 0)
        {
            player.anim.Play("walk_left");
            nextAnimation = "idle_left";

        }
        else if(player.xInput > 0 && Mathf.Round(player.yInput) == 0)
        //else if (player.rb.velocity.x > 0 && Mathf.Round(player.rb.velocity.y) == 0)
        {
            player.anim.Play("walk_right");
            nextAnimation = "idle_right";
        }
        else if(Mathf.Round(player.xInput) == 0 && player.yInput > 0)
        //else if (Mathf.Round(player.rb.velocity.x) == 0 && player.rb.velocity.y > 0)
        {
            player.anim.Play("walk_up");
            nextAnimation = "idle_up";
        }
        else if(Mathf.Round(player.xInput) == 0 && player.yInput < 0)
        //else if (Mathf.Round(player.rb.velocity.x) == 0 && player.rb.velocity.y < 0)
        {
            player.anim.Play("walk_down");
            nextAnimation = "idle_down";
        }
        //Misturados
        //Para cima
        else if(player.xInput > 0 && player.yInput > 0)
        //else if (player.rb.velocity.x > 0 && player.rb.velocity.y > 0)
        {
            player.anim.Play("walk_up_right");
            nextAnimation = "idle_up_right";
        }
        else if(player.xInput < 0 && player.yInput > 0)
        //else if (player.rb.velocity.x < 0 && player.rb.velocity.y > 0)
        {
            player.anim.Play("walk_up_left");
            nextAnimation = "idle_up_left";
        }
        //Para baixo
        else if(player.xInput > 0 && player.yInput < 0)
        //else if (player.rb.velocity.x > 0 && player.rb.velocity.y < 0)
        {
            player.anim.Play("walk_down_right");
            nextAnimation = "idle_down_right";
        }
        else if(player.xInput < 0 && player.yInput < 0)
        //else if (player.rb.velocity.x < 0 && player.rb.velocity.y < 0)
        {
            player.anim.Play("walk_down_left");
            nextAnimation = "idle_down_left";
        }


        player.rb.velocity = new Vector2 (player.xInput * player.Speed, player.yInput * player.Speed);

    }

    public void Exit()
    {

    }
}