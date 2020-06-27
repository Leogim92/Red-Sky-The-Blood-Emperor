using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Route", menuName = "New Patrol Route")]
[System.Serializable]
public class Route : ScriptableObject
{
    public List<Vector3> PatrolPositions;
}
