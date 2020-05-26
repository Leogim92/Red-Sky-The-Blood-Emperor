using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : IState {

    private PlayerController player;
    private string animationName;

    public RunningState (PlayerController player, string animationName) {
        this.player = player;
        this.animationName = animationName;
    }
    public void Enter () {
        player.CurrentStateText.text = "Running";
        //player.anim.Play(animationName);
        player.anim.SetBool("Run", true);
    }

    public void Tick () {
        if (player.xInput == 0 && player.yInput == 0) {
            player.rb.velocity = Vector2.zero;
            player.FSM.ChangeState (new IdleState (player, "player_idle"));
        }

        //Old
        //player.rb.velocity = new Vector2 (player.xInput * player.Speed, player.yInput * player.Speed);

        //Movimento TANK
        /*
        if (player.yInput > 0.1f)//Por que? Porque recebemos o input como analógico e isso faz com que o vetor movimento seja normalizado. Mas eu quero que ele se mova como se fosse um botão.
        {
            player.rb.velocity = player.transform.right * player.Speed * (1 + Time.deltaTime);
            //Por que eu adiciono 1 ao delta time? Porque sem isso o move speed fica devagar, já que o delta time é normalmente um número decimal
        }
        else if (player.yInput < -0.1f)
        {
            player.rb.velocity = -player.transform.right * player.Speed * (1 + Time.deltaTime);
        }
        player.transform.Rotate(0, 0, -player.xInput * (player.Speed * Time.deltaTime * 30)); //Aqui a gente só roda em relação a z.
        //Por que eu multipliquei esse rotate speed por 30? Porque rodando pelo input fica bem devagar.
        */

        //Movimento Norte/Sul
        player.rb.velocity = Vector3.up * player.Speed * player.yInput + Vector3.right * player.Speed * player.xInput;

        //Aqui podemos adicionar o controle da animação
        player.anim.SetFloat("xInput", player.xInput);
        player.anim.SetFloat("yInput", player.yInput);
    }

    public void Exit()
    {
        player.anim.SetBool("Run", false);
    }
}