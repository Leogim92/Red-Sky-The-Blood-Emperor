using System;
using UnityEngine;

public class IdleState : IState {
    PlayerController player;
    private string animationName;
    public IdleState (PlayerController player, string animationName) {
        this.player = player;
        this.animationName = animationName;
    }
    public void Enter () {
        player.CurrentStateText.text = "Idle";
        //player.animator.Play(animationName);
    }

    public void Tick () {
        if (player.xInput != 0 || player.yInput != 0) {
            player.FSM.ChangeState (new RunningState (player, "player_run"));
        }
    }

    public void Exit() { }
}