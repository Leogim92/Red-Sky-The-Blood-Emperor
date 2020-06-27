using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(PatrolRoutes))]
public class PatrolRoutesEditor : Editor
{
    PatrolRoutes patrolRoute;
    string[] options =null;
    public override void OnInspectorGUI()
    {
        patrolRoute = (PatrolRoutes)target;
        CreateOptions();
        EditorGUILayout.Space();
        SelectRoute();
        DisplaySelectedRoute();
        EditorGUILayout.Space();
        base.OnInspectorGUI();
    }

    private void DisplaySelectedRoute()
    {
        SetRouteSize();
        DisplayRoutePositions();
    }

    private void SetRouteSize()
    {
        int size = EditorGUILayout.DelayedIntField("Size", patrolRoute.SelectedRoute.PatrolPositions.Count);
        int difference = size - patrolRoute.SelectedRoute.PatrolPositions.Count;
        if (size > patrolRoute.SelectedRoute.PatrolPositions.Count)
        {
            Vector3 newPos = patrolRoute.SelectedRoute.PatrolPositions[patrolRoute.SelectedRoute.PatrolPositions.Count - 1];
            for (int i = 0; i< difference; i++)
            {
                patrolRoute.SelectedRoute.PatrolPositions.Add(newPos);
            }
        }
        else if (size < patrolRoute.SelectedRoute.PatrolPositions.Count)
        {
            int listElements = patrolRoute.SelectedRoute.PatrolPositions.Count - 1;
            int goalElements = patrolRoute.SelectedRoute.PatrolPositions.Count + difference;
            for (int i = listElements; i >= goalElements; i--)
            {
                patrolRoute.SelectedRoute.PatrolPositions.RemoveAt(i);
            }
        }
    }

    private void DisplayRoutePositions()
    {
        for (int i = 0; i < patrolRoute.SelectedRoute.PatrolPositions.Count; i++)
        {
            patrolRoute.SelectedRoute.PatrolPositions[i] = EditorGUILayout.Vector3Field("Position: " + i, patrolRoute.SelectedRoute.PatrolPositions[i]);
        }
    }

    private void SelectRoute()
    {
        patrolRoute.SelectedOption = EditorGUILayout.Popup("Choose a Patrol Route", patrolRoute.SelectedOption, options);
        patrolRoute.SelectedRoute = patrolRoute.Routes[patrolRoute.SelectedOption];
    }

    void CreateOptions()
    {
        options = new string[patrolRoute.Routes.Count];
        for( int i =0; i < patrolRoute.Routes.Count; i++)
        {
            options[i] = "Route:" + (i+1);
        }
    }
}
