using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PatrolRoutes))]
public class PatrolRoutesEditor : Editor
{
    PatrolRoutes patrolRoute;
    int selected;
    string[] options =null;
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        patrolRoute = (PatrolRoutes)target;
        CreateOptions();
        EditorGUILayout.Popup("Select Route", selected, options);
        patrolRoute.SelectedRoute = patrolRoute.Routes[selected];
        EditorUtility.SetDirty(target);
        serializedObject.ApplyModifiedProperties();
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
