using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePointToShoot : MonoBehaviour
{
    public float[] rotation;
    public Vector3[] position;

    public void upShoot()
    {
        Debug.Log("Up");
    }
    public void upRightShoot()
    {
        Debug.Log("upRight");
    }
    public void upLeftShoot()
    {
        Debug.Log("upLeft");
    }
    public void rightShoot()
    {
        Debug.Log("Right");
    }
    public void leftShoot()
    {
        Debug.Log("left");
    }
    public void downShoot()
    {
        Debug.Log("Down");
    }
    public void downRightShoot()
    {
        Debug.Log("downRight");
    }
    public void downLeftShoot()
    {
        Debug.Log("downLeft");
    }

}
