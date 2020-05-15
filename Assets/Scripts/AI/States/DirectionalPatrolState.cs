using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalPatrolState : IState
{
    private Ai_Medium_Script ai;
    private string animationName;
    private GameObject player;
    private List<Vector2> positions = new List<Vector2>();
    private int lastIteration = 0;
    private bool retorno = false;

    public DirectionalPatrolState(Ai_Medium_Script ai, string animationName)
    {
        this.ai = ai;
        this.animationName = animationName;
    }

    public void Enter()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (ai.positionsToBe.Count == 0)
        {
            Debug.LogError("DirectionalPatrolState: You need to set the positions to be on the public gameobject list");
        }

        //Aqui estamos trocando de Gameobject para Vector2
        foreach (GameObject p in ai.positionsToBe)
        {
            positions.Add(p.transform.position);
        }
        
        //Para que isso? Para verificar se ele está mais próximo de algum ponto, talvez o patrol tenha reiniciado
        for (int i = 0; i < positions.Count; i++)
        {
            if(Vector2.Distance(new Vector2(ai.transform.position.x, ai.transform.position.y), positions[i]) < 0.6f)
            {
                lastIteration = i;
            }
        }

        LookAtPosition(positions[lastIteration]); //Começar olhando para a posição certa
    }
        
    public void Tick()
    {

        if (Vector2.Distance(player.transform.position, ai.transform.position) < 10f)
        {
            //ai.FSM.ChangeState(new AttackingState(ai, "Enemy_attack"));
            Debug.Log("I am back to previous state ");
            ai.FSM.SwitchToPreviousState();
        }
        else //Ok isso é hardcore, faz uma força
        {
            MoveToPosition();
            if (CheckForDistancePatrolled(positions[lastIteration])) //Se ele chega no ponto *
            {
                if (retorno)//**
                {
                    lastIteration--;
                    LookAtPosition(positions[lastIteration]);

                    if (lastIteration <= 0)
                    {
                        retorno = false;
                        lastIteration = 0;
                    }
                }
                else
                {
                    lastIteration++; //* Adiciona um, para andar para o próximo ponto
                    LookAtPosition(positions[lastIteration]);

                    if (lastIteration >= (positions.Count - 1)) // Se esse próximo ponto for o valor máximo
                    {
                        if (ai.shouldReturnToFirstPosition) //Confere se o bool de retornar é true, para que ele retorne ao ponto inicial
                        {//Utilizando o **
                            retorno = true;
                            lastIteration = (positions.Count - 1);// Isso serve para evitar valores maiores do que existe no vetor
                        }
                        else
                        {
                            Debug.Log("I am back to previous state ");
                            ai.FSM.SwitchToPreviousState(); //Colocar idle aqui.
                        }

                    }
                }


            }
        }
    }
    public void Exit()
    {

    }

    private void MoveToPosition()
    {

        ai.transform.position += ai.transform.right * ai.movementSpeed * Time.deltaTime;

    }
    private void LookAtPosition(Vector2 goTo)
    {
        Vector2 lookAt = (goTo - new Vector2(ai.transform.position.x, ai.transform.position.y));
        ai.transform.right = lookAt.normalized;
    }
    private bool CheckForDistancePatrolled(Vector2 goTo)
    {
        if (Vector2.Distance(new Vector2(ai.transform.position.x, ai.transform.position.y), goTo) < 0.6f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
