using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Cloud {
    public Transform transform;
    public float speed;
}
public class CloudManager : MonoBehaviour {

    [MinMax (0, 1, ShowEditRange = true)]
    public Vector2 SpeedRange = new Vector2 (0, 1);
    public Vector3 boundaries;

    [Header("Layer Color")]
    public float GreyScaleMultiplier;
    public float Alpha = 1;
    List<Cloud> clouds = new List<Cloud> ();
    private int direction;
    void Start () {
        direction = Random.Range (0, 2) * 2 - 1; //-1 or 1;
        
        foreach (Transform child in transform) {
            SpriteRenderer sr = child.gameObject.GetComponent<SpriteRenderer>();
            sr.sortingOrder = Random.Range (-5, 0);
            sr.color = new Color(1 + sr.sortingOrder * GreyScaleMultiplier, 1 + sr.sortingOrder * GreyScaleMultiplier, 1 + sr.sortingOrder * GreyScaleMultiplier, Alpha);
            
            Cloud temp = new Cloud ();
            temp.transform = child;
            temp.speed = Random.Range (SpeedRange.x, SpeedRange.y);
            clouds.Add (temp);
        }
    }

    void Update () {
        foreach (Cloud cloud in clouds) {
            cloud.transform.position += new Vector3 (direction * cloud.speed * Time.deltaTime, 0, 0);
            
            if (cloud.transform.position.x >= boundaries.x) {
                cloud.transform.position = new Vector3 (transform.position.x - boundaries.x, cloud.transform.position.y, cloud.transform.position.z);
                cloud.transform.GetComponent<SpriteRenderer>().flipX = !cloud.transform.GetComponent<SpriteRenderer>().flipX;
            } else if (cloud.transform.position.x <= -boundaries.x) {
                cloud.transform.position = new Vector3 (transform.position.x + boundaries.x, cloud.transform.position.y, cloud.transform.position.z);
                cloud.transform.GetComponent<SpriteRenderer>().flipX = !cloud.transform.GetComponent<SpriteRenderer>().flipX;
            }
        }
    }

    private void OnDrawGizmos () {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube (transform.position + boundaries, new Vector3 (1, 10, 0));
        Gizmos.DrawWireCube (transform.position - boundaries, new Vector3 (1, 10, 0));
    }
}