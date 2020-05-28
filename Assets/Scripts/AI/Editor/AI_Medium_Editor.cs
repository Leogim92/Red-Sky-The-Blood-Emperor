using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Ai_Medium_Script))]
public class AI_Medium_Editor : Editor
{
    Ai_Medium_Script enemy;

    public override void OnInspectorGUI()
    {
        enemy = (Ai_Medium_Script)target;
        base.OnInspectorGUI();

        CheckAIBehaviour();
        serializedObject.ApplyModifiedProperties();
    }

    private void CheckAIBehaviour()
    {
        switch (enemy.aiBehaviour)
        {
            case Ai_Medium_Script.Behaviour.patrol:
                break;
            case Ai_Medium_Script.Behaviour.directionalPatrol:
                DisplayPatrolPositions();
                break;
            case Ai_Medium_Script.Behaviour.alternativeDPatrol:
                DisplayPatrolPositions();
                break;
            case Ai_Medium_Script.Behaviour.agressive:
                //agressive properties
                break;
            case Ai_Medium_Script.Behaviour.vigilance:
                //vigilance properties
                break;

        }
        
    }

    private void DisplayPatrolPositions()
    {
        SerializedProperty patrolPositions = serializedObject.FindProperty("positionsToBe");
        patrolPositions.isExpanded = EditorGUILayout.Foldout(patrolPositions.isExpanded, "Patrol Positions");
        if (patrolPositions.isExpanded)
        {
            patrolPositions.arraySize = EditorGUILayout.DelayedIntField("Size", patrolPositions.arraySize);
            for (int i = 0; i < patrolPositions.arraySize; ++i)
            {
                SerializedProperty transform = patrolPositions.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(transform, new GUIContent("Position " + i));
            }
        }
    }
}