using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine {

    private IState currentState;
    private IState previousState;
    public void ChangeState (IState newState) {
        if (currentState != null) {
            currentState.Exit ();
        }
        previousState = currentState;
        currentState = newState;
        currentState.Enter ();
    }

    public void Tick () {
        if (currentState != null) {
            currentState.Tick ();
        }
    }

    public void SwitchToPreviousState(){
        currentState.Exit();
        currentState = previousState;
        currentState.Enter();
    }
}