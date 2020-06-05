using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Patrol Routes", menuName = "New Patrol Routes Container")]
[System.Serializable]
public class PatrolRoutes : ScriptableObject
{
    [HideInInspector] public List<Vector3> PatrolRoute;
    [HideInInspector] public int SelectedOption;
    public List<Route> Routes;
    [HideInInspector] public Route SelectedRoute;
}
