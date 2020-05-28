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
    bool arePositionsAdded = false;
    public DirectionalPatrolState(Ai_Medium_Script ai, string animationName)
    {
        this.ai = ai;
        this.animationName = animationName;
    }

    public void Enter()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (ai.patrolPositions.Count == 0)
        {
            Debug.LogError("DirectionalPatrolState: You need to set the positions to be on the public gameobject list");
        }

        if (!arePositionsAdded)
        {
            foreach (Transform p in ai.patrolPositions)
            {
                positions.Add(p.position);
            }
            arePositionsAdded = true;
        }
        //Para que isso? Para verificar se ele está mais próximo de algum ponto, talvez o patrol tenha reiniciado
        for (int i = 0; i < positions.Count; i++)
        {
            if (Vector2.Distance(new Vector2(ai.transform.position.x, ai.transform.position.y), positions[i]) < 0.6f)
            {
                lastIteration = i;
            }
        }

        LookAtPosition(positions[lastIteration]); //Começar olhando para a posição certa
    }

    public void Tick()
    {

        if (Vector2.Distance(player.transform.position, ai.transform.position) < ai.distanceToAttack) //Se estiver próximo ao player não fazer esta patrulha.
        {
            ai.FSM.ChangeState(new AttackingState(ai, "Enemy_attack"));
        }

        else //Ok isso é hardcore, faz uma força
        {
            MoveToPosition(); //Primeiro, movemos.

            if (CheckForDistancePatrolled(positions[lastIteration])) // Se alcança a posição
            {
                if (retorno == false) // Não está retornando , ou seja, está no caminho de ida.
                {
                    if (ai.shouldReturnToFirstPosition)// Confere se é para retornar, se for, nós vamos ao próximo ponto da lista antes de conferir se é o ultimo
                    {
                        lastIteration++;
                        LookAtPosition(positions[lastIteration]);//Olha para a posição nova

                        if (lastIteration >= (positions.Count - 1)) //Se o ponto for o ultimo
                        {
                            retorno = true; //Retorne, então ele começa andar no sentido negativo da lista.
                            lastIteration = (positions.Count - 1); //Evitar bugs
                        }

                    }
                    else // Se não é para retornar
                    {
                        if (lastIteration >= (positions.Count - 1)) //Confere antes de aumentar se esse é o ultimo ponto da lista
                        {
                            Debug.Log("Should stop here / Awaiting idle state implementation");
                            return;
                        }

                        lastIteration++; //Se não for, continuar.
                        LookAtPosition(positions[lastIteration]); //Olha para a posição nova
                    }

                }
                else //Se ele está retornando
                {
                    lastIteration--;  //Sempre que usamos o retorno temos de andar para o ponto anterior/próximo da lista antes de conferir
                    LookAtPosition(positions[lastIteration]);

                    if (lastIteration <= 0) //Conferimos se é a ultima posição, se for trocar o retorno para false e continuar interando, agora no sentido positivo.
                    {
                        retorno = false;
                        lastIteration = 0;//Evitar bugs
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
    private void LookAtPosition(Vector2 goTo) //Aqui ele deve olhar sempre para o ponto de patrulha
    {
        Vector2 lookAt = (goTo - new Vector2(ai.transform.position.x, ai.transform.position.y));
        ai.transform.right = lookAt.normalized;
    }
    private bool CheckForDistancePatrolled(Vector2 goTo)
    {
        if (Vector2.Distance(new Vector2(ai.transform.position.x, ai.transform.position.y), goTo) < 0.6f) //Se estiver próximo ao ponto, retorna true para que vá ao próximo ponto de patrulha
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
