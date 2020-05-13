using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_test : MonoBehaviour
{
    public ParticleSystem particle;
    public bool shouldPatrol;//Quero fazer uma patrulha em uma área definida.
    public float areaToPatrol; //Área da patrulha
    private GameObject player;
    private ParticleSystem.EmissionModule emission;
    private Vector2 initialPosition; //Para que ele patrulhe dentro desta posição inicial
    private bool patrol = false; 


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //Pegando o player para encontrar sua posição
        particle.Play(); //Necessário para controlar as partículas apenas ativando e desativando a emissão
        emission = particle.emission; //Pegando a emissão para ativar/desativar

        if (shouldPatrol)
        {
            patrol = true;
        }
        initialPosition = transform.position;

        InvokeRepeating("Patrol", 0.1f, 2f); //A função de patrulhar será chamada a cada 2 segundos.
    }

    void Update()
    {
        if (Vector2.Distance(player.transform.position, transform.position) < 10f) //Se o player estiver a uma distância menor do que 10f
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            if (shouldPatrol) //Deve parar de patrulhar quando começar a atirar no player.
            {
                patrol = false;
            }

            emission.enabled = true; //Começar a atirar

            //All hail Legoin, The Code savior.
            Vector2 lookAt = player.transform.position - transform.position;
            transform.right = lookAt.normalized;

            /*OLD
            Copiei e colei, só sei que funciona https://answers.unity.com/questions/654222/make-sprite-look-at-vector2-in-unity-2d-1.html
            var dir = player.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            */

        }
        else //Se o player sair do sight desativar a emissão** e voltar a patrulhar
        {
            if (emission.enabled)
            {
                emission.enabled = false; //Parar de atirar

                if (shouldPatrol) //Voltar a patrulhar
                {
                    patrol = true;
                }
            }

        }
        
        
    }

    private void Patrol()
    {
        if (patrol)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero; //Sempre parar a cada chamada da função

            Vector2 lookAt = (initialPosition + Random.insideUnitCircle * areaToPatrol) - new Vector2(transform.position.x, transform.position.y); //Randomizando para onde ele vai olhar
            transform.right = lookAt.normalized;

            GetComponent<Rigidbody2D>().AddForce(transform.right * 100); //Para então voltar a andar
            
        }
    }
}
// Por que é legal usar a emissão? Porque desativando o gameobject da partícula nós desativamos por completo as partículas até já instanciadas.
// E eu quero que as balas continuem seu caminho natural mesmo após o player ter saído da area de procura do inimigo.
