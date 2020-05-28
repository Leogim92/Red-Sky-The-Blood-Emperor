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
        EditorGUILayout.Space();
        GUILayout.Label("Behaviour Properties", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        switch (enemy.aiBehaviour)
        {
            case Ai_Medium_Script.Behaviour.patrol:
                DisplayMovementSpeed();
                DisplayDistanceToPatrol();
                DisplayDistanceToAttack();
                break;
            case Ai_Medium_Script.Behaviour.directionalPatrol:
                DisplayMovementSpeed();
                DisplayDistanceToAttack();
                DisplayPatrolReturnToogle();
                DisplayPatrolPositions();
                break;
            case Ai_Medium_Script.Behaviour.alternativeDPatrol:
                DisplayMovementSpeed();
                DisplayDistanceToAttack();
                DisplayPatrolReturnToogle();
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
        SerializedProperty patrolPositions = serializedObject.FindProperty("patrolPositions");
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
    private void DisplayPatrolReturnToogle()
    {
        enemy.shouldReturnToFirstPosition = EditorGUILayout.Toggle("Patrol Return", enemy.shouldReturnToFirstPosition);
    }
    private void DisplayMovementSpeed()
    {
        enemy.movementSpeed = EditorGUILayout.DelayedFloatField("Movement Speed", enemy.movementSpeed);
    }
    private void DisplayDistanceToPatrol()
    {
        enemy.distanceToPatrol = EditorGUILayout.DelayedFloatField("Distance To Patrol", enemy.distanceToPatrol);
    }
    private void DisplayDistanceToAttack()
    {
        enemy.distanceToAttack = EditorGUILayout.DelayedFloatField("Distance To Attack", enemy.distanceToAttack);
    }
}