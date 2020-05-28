using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : IState {

    private PlayerController player;
    private string animationName;
    private Vector2 lastInput;

    public RunningState (PlayerController player, string animationName) {
        this.player = player;
        this.animationName = animationName;
    }
    public void Enter () {
        player.CurrentStateText.text = "Running";
        player.anim.Play(animationName);
        //player.anim.SetBool("Run", true);
    }

    public void Tick () {
        if (player.xInput == 0 && player.yInput == 0) {
            player.rb.velocity = Vector2.zero;

            //Alô alô, algo estranho acontece aqui.
            Debug.Log(lastInput);
            player.anim.SetFloat("xInput", lastInput.x);
            player.anim.SetFloat("yInput", lastInput.y);

            player.FSM.ChangeState (new IdleState (player, "player_idle"));
        }

        //Old
        //player.rb.velocity = new Vector2 (player.xInput * player.Speed, player.yInput * player.Speed);

        //Movimento Norte/Sul
        player.rb.velocity = Vector3.up * player.Speed * player.yInput + Vector3.right * player.Speed * player.xInput;

        //NÂO FUNCIONAL
        //player.particle.gameObject.transform.rotation = Quaternion.LookRotation(player.rb.velocity); //Tentando rotacionar a partícula

        //Aqui podemos adicionar o controle da animação
        player.anim.SetFloat("xInput", player.xInput);
        player.anim.SetFloat("yInput", player.yInput);

        //Salvando ultimo input não zero.
        if(player.xInput != 0 || player.yInput != 0)
        {
            lastInput.x = player.xInput;
            lastInput.y = player.yInput;
            //Debug.Log(lastInput);
        }

    }

    public void Exit()
    {

    }
}