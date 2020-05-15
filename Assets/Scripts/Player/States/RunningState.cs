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
        //player.animator.Play (animationName);
    }

    public void Tick () {
        if (player.xInput == 0 && player.yInput == 0) {
            player.FSM.ChangeState (new IdleState (player, "player_idle"));
        }

        player.rb.velocity = new Vector2 (player.xInput * player.Speed, player.yInput * player.Speed);
    }

    public void Exit()
    {

    }
}