using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(AIBrain))]
public class AIBrainEditor : Editor
{
    AIBrain enemy;
    List<Vector3> patrolPositions;
    
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
                DisplayIdleTime();
                DisplayLoopToogle();
                EditorGUILayout.Space();
                DisplayPatrolRoute();
                break;
            case AIBrain.Behaviour.agressive:
                //agressive properties
                break;
            case AIBrain.Behaviour.vigilance:
                //vigilance properties
                break;
        }
        
    }
    private void DisplayPatrolRoute()
    {
        SerializedProperty patrolRoutesProperty = serializedObject.FindProperty("patrolRoutes");
        EditorGUILayout.PropertyField(patrolRoutesProperty);
    }
    private void DisplayLoopToogle()
    {
        enemy.patrolLoop = EditorGUILayout.Toggle("Patrol Loop", enemy.patrolLoop);
    }
    private void DisplayIdleTime()
    {
        enemy.timeToWait = EditorGUILayout.DelayedFloatField("Idle Waiting Time", enemy.timeToWait);
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
        try
        {
            if (enemy.aiBehaviour == AIBrain.Behaviour.directionalPatrol)
            {
                enemy = (AIBrain)target;
                patrolPositions = enemy.patrolRoutes.SelectedRoute.PatrolRoute;
                Vector3 positionStart, positionFinish;
                for (int i = 0; i < patrolPositions.Count - 1; i++)
                {
                    DrawPatrolPath(out positionStart, out positionFinish, i);
                    CreateTransformHandles(ref positionStart, ref positionFinish, i);
                }
                DrawLoop();
            }
        }
        catch { }
    }
    private void DrawPatrolPath(out Vector3 positionStart, out Vector3 positionFinish, int i)
    {
        positionStart = patrolPositions[i];
        positionFinish = patrolPositions[i + 1];
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
            patrolPositions[i] = positionStart;
        }

        EditorGUI.BeginChangeCheck();
        positionFinish = Handles.DoPositionHandle(positionFinish, Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(enemy);
            patrolPositions[i + 1] = positionFinish;
        }
    }
    private void DrawLoop()
    {
        if (enemy.patrolLoop)
        {
            Handles.color = Color.cyan;
            Handles.DrawLine(patrolPositions[0], patrolPositions[patrolPositions.Count - 1]);
        }
    }

}