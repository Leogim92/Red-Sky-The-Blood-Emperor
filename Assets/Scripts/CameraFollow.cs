using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;
    public float cameraDistance;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player == null) //Se não existir mirar no começo da tela
        {
            player = new GameObject(); 
        }

    }

    void Update()
    {
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + cameraDistance);

    }
}
