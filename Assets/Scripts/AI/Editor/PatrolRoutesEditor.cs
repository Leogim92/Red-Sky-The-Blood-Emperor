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
        SelectRoute();
        EditorGUILayout.Space();
        DisplaySelectedRoute();
        EditorGUILayout.Space();
        base.OnInspectorGUI();
        serializedObject.Update();
    }

    private void DisplaySelectedRoute()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("SelectedRoute"));
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
