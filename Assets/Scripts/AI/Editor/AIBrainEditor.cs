using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(AIBrain))]
public class AIBrainEditor : Editor
{
    AIBrain enemy;
    

    //custom inspectorHUI
    public override void OnInspectorGUI()
    {
        enemy = (AIBrain)target;
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
            case AIBrain.Behaviour.patrol:
                DisplayMovementSpeed();
                DisplayDistanceToPatrol();
                DisplayDistanceToAttack();
                break;
            case AIBrain.Behaviour.directionalPatrol:
                DisplayMovementSpeed();
                DisplayDistanceToAttack();
                DisplayLoopToogle();
                DisplayPatrolPositions();
                break;
            case AIBrain.Behaviour.agressive:
                //agressive properties
                break;
            case AIBrain.Behaviour.vigilance:
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
    private void DisplayLoopToogle()
    {
        enemy.patrolLoop = EditorGUILayout.Toggle("Patrol Loop", enemy.patrolLoop);
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


    //custom sceneGUI
    private void OnSceneGUI()
    {
        enemy = (AIBrain)target;
        Vector3 positionStart, positionFinish;
        for (int i = 0; i < enemy.patrolPositions.Count - 1; i++)
        {
            DrawPatrolPath(out positionStart, out positionFinish, i);
            CreateTransformHandles(ref positionStart, ref positionFinish, i);
        }
        DrawLoop();
    }
    private void DrawPatrolPath(out Vector3 positionStart, out Vector3 positionFinish, int i)
    {
        positionStart = enemy.patrolPositions[i].position;
        positionFinish = enemy.patrolPositions[i + 1].position;
        Handles.color = Color.cyan;
        Handles.DrawLine(positionStart, positionFinish);
    }
    private void CreateTransformHandles(ref Vector3 positionStart, ref Vector3 positionFinish, int i)
    {
        EditorGUI.BeginChangeCheck();
        positionStart = Handles.DoPositionHandle(positionStart, Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(enemy);
            enemy.patrolPositions[i].position = positionStart;
        }

        EditorGUI.BeginChangeCheck();
        positionFinish = Handles.DoPositionHandle(positionFinish, Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(enemy);
            enemy.patrolPositions[i + 1].position = positionFinish;
        }
    }
    private void DrawLoop()
    {
        if (enemy.patrolLoop)
        {
            Handles.color = Color.cyan;
            Handles.DrawLine(enemy.patrolPositions[0].position, enemy.patrolPositions[enemy.patrolPositions.Count - 1].position);
        }
    }

}