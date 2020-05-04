using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

[RequireComponent (typeof (UnityEngine.Experimental.Rendering.Universal.Light2D))]
public class SunManager : MonoBehaviour {
    public Transform Center;
    public Vector3 Axis = Vector3.forward;

    [MinMax (0, 1, ShowEditRange = true)]
    public Vector2 IntensityRange;

    [MinMax (0, 255, ShowEditRange = true)]
    public Vector2 GreenRange;
    public float Radius = 20.0f;
    public float RadiusSpeed = 0f;
    public float RotationSpeed = 80.0f;
    private Vector3 desiredPosition;
    private UnityEngine.Experimental.Rendering.Universal.Light2D sunLight;

    void Start () {
        transform.position = (transform.position - Center.position).normalized * Radius + Center.position;
        sunLight = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D> ();
    }

    void Update () {
        transform.RotateAround (Center.position, Axis, RotationSpeed * Time.deltaTime);
        desiredPosition = (transform.position - Center.position).normalized * Radius + Center.position;
        transform.position = Vector3.MoveTowards (transform.position, desiredPosition, Time.deltaTime * RadiusSpeed);

        //usar a variavel sunLight para fazer alterações à intensidade, cor, etc..
    }
}