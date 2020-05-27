using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof (Ai_Medium_Script))]
public class AI_Medium_Editor : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        var container = new VisualElement();

        var movementSpeed = new PropertyField(property.FindPropertyRelative("movementSpeed"));

        return container;
        //return base.CreatePropertyGUI(property);
    }
}

//[CustomEditor(typeof(Ai_Medium_Script))]
//[CanEditMultipleObjects]
//public class AI_Medium_Editor : Editor
//{
//    private Ai_Medium_Script ai;
//    private string behave = "Passive";
//    private string movement = "Idle";
//    private string talking = "NoTalking";
//    private byte spots = 0; //Um integer entre 0 e 255;

//    private void OnEnable()
//    {
//        ai = (Ai_Medium_Script)target;
//    }

//    public override void OnInspectorGUI()
//    {
//        Event evt = Event.current;
//        Rect dropDownRect;
//        base.OnInspectorGUI();

//        EditorGUILayout.LabelField("AI Settings", EditorStyles.boldLabel);
//        EditorGUILayout.Space(5f);

//        ai.movementSpeed = EditorGUILayout.FloatField("Ai Movement Speed", ai.movementSpeed);

//        EditorGUILayout.BeginHorizontal();
//        EditorGUILayout.PrefixLabel("Set Behaviour");
//        EditorGUILayout.DropdownButton(new GUIContent("Behaviour"), FocusType.Passive);
//        dropDownRect = GUILayoutUtility.GetLastRect();
//        EditorGUILayout.EndHorizontal();

//        //Escolhendo o comportamento
//        if (evt.type == EventType.MouseDown)
//        { //Tirei daqui https://docs.unity3d.com/ScriptReference/GenericMenu.AddItem.html
//            if (dropDownRect.Contains(evt.mousePosition))
//            {
//                GenericMenu m = new GenericMenu();

//                m.AddItem(new GUIContent("Agressive"), false, Callback, "Agressive");
//                m.AddItem(new GUIContent("Passive"), false, Callback, "Passive");
//                m.DropDown(dropDownRect);
//            }
//        }

//        //Se for agressivo
//        if(behave == "Agressive")
//        {
//            //Definir o tipo de movimento
//            EditorGUILayout.BeginHorizontal();
//            EditorGUILayout.PrefixLabel("Set Movement Type");
//            EditorGUILayout.DropdownButton(new GUIContent("Idle"), FocusType.Passive);
//            dropDownRect = GUILayoutUtility.GetLastRect();
//            EditorGUILayout.EndHorizontal();
//            if(evt.type == EventType.MouseDown)
//            {
//                if (dropDownRect.Contains(evt.mousePosition))
//                {
//                    GenericMenu a = new GenericMenu();

//                    a.AddItem(new GUIContent("Idle"), false, CallBack1, "Idle");
//                    a.AddItem(new GUIContent("Patrol"), false, CallBack1, "Patrol");
//                    a.AddItem(new GUIContent("Directional Patrol"), false, CallBack1, "Directional Patrol");
//                    a.DropDown(dropDownRect);
//                }
//            }
//        }

//        //Se for passivo
//        if(behave == "Passive")
//        {
//            //Definir o tipo de movimento
//            EditorGUILayout.BeginHorizontal();
//            EditorGUILayout.PrefixLabel("Set Movement Type");
//            EditorGUILayout.DropdownButton(new GUIContent("Idle"), FocusType.Passive);
//            dropDownRect = GUILayoutUtility.GetLastRect();
//            EditorGUILayout.EndHorizontal();
//            if (evt.type == EventType.MouseDown)
//            {
//                if (dropDownRect.Contains(evt.mousePosition))
//                {
//                    GenericMenu a = new GenericMenu();

//                    a.AddItem(new GUIContent("Idle"), false, CallBack1, "Idle");
//                    a.AddItem(new GUIContent("Patrol"), false, CallBack1, "Patrol");
//                    a.AddItem(new GUIContent("Directional Patrol"), false, CallBack1, "Directional Patrol");
//                    a.DropDown(dropDownRect);
//                }
//            }

//            //Definir se fala
//            EditorGUILayout.BeginHorizontal();
//            EditorGUILayout.PrefixLabel("Set Talk Type");
//            EditorGUILayout.DropdownButton(new GUIContent("NoTalking"), FocusType.Passive);
//            dropDownRect = GUILayoutUtility.GetLastRect();
//            EditorGUILayout.EndHorizontal();
//            if (evt.type == EventType.MouseDown)
//            {
//                if (dropDownRect.Contains(evt.mousePosition))
//                {
//                    GenericMenu a = new GenericMenu();

//                    a.AddItem(new GUIContent("Talking"), false, CallBack2, "Talking");
//                    a.AddItem(new GUIContent("NoTalking"), false, CallBack2, "NoTalking");
//                    a.DropDown(dropDownRect);
//                }
//            }
            
//        }

//        //Definindo o menu para a patrulha
//        if(movement == "Patrol")
//        {
//            ai.distanceToPatrol = EditorGUILayout.FloatField("Distance To Patrol", ai.distanceToPatrol);
//        }
//        else if(movement == "Directional Patrol")
//        {
//            EditorGUILayout.IntField("How many different spots I need to be?", spots);
//            ObjectField field = new ObjectField();
//            field.allowSceneObjects = true;
//            for (byte i = 0; i < spots; i++)
//            {
//                //field.hierarchy.Insert(i, new GUIContent()); how?
//            }
//        }
        
//    }

//    public void Callback(object obj)
//    {
//        Debug.Log("Meu comportamento é " + obj);
//        behave = obj.ToString();
//    }
//    public void CallBack1(object obj)
//    {
//        Debug.Log("Meu movimento é " + obj);
//        movement = obj.ToString();
//    }
//    public void CallBack2(object obj)
//    {
//        Debug.Log("Eu devo falar? " + obj);
//        talking = obj.ToString();
//    }
//}