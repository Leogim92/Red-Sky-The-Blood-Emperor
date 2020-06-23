using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePointToShoot : MonoBehaviour
{
    public GameObject particle;
    public Vector3 upPosition;
    public Vector3 upRightPosition;
    public Vector3 upLeftPosition;
    public Vector3 rightPosition;
    public Vector3 leftPosition;
    public Vector3 downPosition;
    public Vector3 downRightPosition;
    public Vector3 downLeftPosition;

    public void upShoot()
    {
        particle.transform.localPosition = upPosition;
        particle.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
    public void upRightShoot()
    {
        particle.transform.localPosition = upRightPosition;
        particle.transform.localRotation = Quaternion.Euler(0, 0, -45f);
    }
    public void upLeftShoot()
    {
        particle.transform.localPosition = upLeftPosition;
        particle.transform.localRotation = Quaternion.Euler(0, 0, 45f);
    }
    public void rightShoot()
    {
        particle.transform.localPosition = rightPosition;
        particle.transform.localRotation = Quaternion.Euler(0, 0, -90f);
    }
    public void leftShoot()
    {
        particle.transform.localPosition = leftPosition;
        particle.transform.localRotation = Quaternion.Euler(0, 0, 90f);
    }
    public void downShoot()
    {
        particle.transform.localPosition = downPosition;
        particle.transform.localRotation = Quaternion.Euler(0, 0, 180);
    }
    public void downRightShoot()
    {
        particle.transform.localPosition = downRightPosition;
        particle.transform.localRotation = Quaternion.Euler(0, 0, -135f);
    }
    public void downLeftShoot()
    {
        particle.transform.localPosition = downLeftPosition;
        particle.transform.localRotation = Quaternion.Euler(0, 0, 135f);
    }

}
