using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Spell))]
public class SpellCustomPropertyDrawer : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();   
        Spell _spell = (Spell)target;
        
        DrawTitle("Base Ability Parameters");
        HorizontalGUI(() =>
        {
            GUILayout.Label("AbilityName", GUILayout.Width(120.0f));
            _spell.AbilityName = EditorGUILayout.TextField(_spell.AbilityName);

        });
        DrawTitle("Spell Parameters");

        HorizontalGUI(() =>
        {
            GUILayout.Label("Ressource Cost",GUILayout.Width(120.0f));
            _spell.ressourceCost = EditorGUILayout.IntField(_spell.ressourceCost);
        });
        HorizontalGUI(() =>
        {
            GUILayout.Label("Cooldown", GUILayout.Width(120.0f));
            _spell.cooldown = EditorGUILayout.IntField(_spell.cooldown);
        });

        HorizontalGUI(() =>
        {
            GUILayout.Label("Spell Action", GUILayout.Width(120.0f));
            _spell.spellAction = (SpellActionType)EditorGUILayout.EnumPopup(_spell.spellAction);
        });

        if (_spell.spellAction == SpellActionType.ThrowProjectile)
        {
            DrawTitle("Projectile Parameters");
            HorizontalGUI(() =>
            {
                SerializedProperty _object = serializedObject.FindProperty("objectReference");
                EditorGUILayout.ObjectField(_object);
            });
        }












        serializedObject.ApplyModifiedProperties();
    }

    void DrawTitle(string _label)
    {
        GUILayout.Space(5.0f);
        GUILayout.BeginHorizontal();
        GUILayout.Label(_label, EditorStyles.boldLabel);
        GUILayout.EndHorizontal();
    }

    void HorizontalGUI(Action _fields)
    {
        GUILayout.BeginHorizontal();
        _fields.Invoke();
        GUILayout.EndHorizontal();
    }
}
