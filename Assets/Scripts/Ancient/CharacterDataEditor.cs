using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(BaseCharacter))]
public class CharacterDataEditor : Editor {

    private SerializedObject CharacterData;  //序列化的对象
    private SerializedProperty type, gender, hpLevel, epLevel, freeExp;

    void OnEnable()  //重新注册各项数值
    {
        CharacterData = new SerializedObject(target);
        type = CharacterData.FindProperty("type");
        gender = CharacterData.FindProperty("gender");
        hpLevel = CharacterData.FindProperty("hpLevel");
        epLevel = CharacterData.FindProperty("epLevel");
        freeExp = CharacterData.FindProperty("freeExp");
    }

    public override void OnInspectorGUI()  //重新注册UI控件
    {
        CharacterData.Update();
        EditorGUILayout.PropertyField(type);
        if(type.enumValueIndex == 0) {
            serializedObject.ApplyModifiedProperties();
        }
    }

}
