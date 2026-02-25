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
            GUILayout.Label("Ability Name", GUILayout.Width(120.0f));
            _spell.AbilityName = EditorGUILayout.TextField(_spell.AbilityName);
        });

        HorizontalGUI(() =>
        {
            GUILayout.Label("Ability ID", GUILayout.Width(120.0f));
            _spell.AbilityID = EditorGUILayout.TextField(_spell.AbilityID);

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

        DrawTitle("Spell Action");
        HorizontalGUI(() =>
        {
            _spell.spellAction = (SpellActionType)EditorGUILayout.EnumPopup(_spell.spellAction);
        });

        DrawTitle("Parameters");
        if (_spell.spellAction == SpellActionType.AttackBonus)
        {
            HorizontalGUI(() =>
            {
                GUILayout.Label("Damage Bonus", GUILayout.Width(120.0f));
                _spell.bonusValue = EditorGUILayout.IntField(_spell.bonusValue);
            });


            HorizontalGUI(() =>
            {
                GUILayout.Label("Has Duration", GUILayout.Width(120.0f));
                _spell.hasDuration = EditorGUILayout.Toggle(_spell.hasDuration, GUILayout.Width(15.0f));

                if (_spell.hasDuration)
                {
                    GUILayout.Label("Duration", GUILayout.Width(75.0f));
                    _spell.bonusDuration = EditorGUILayout.IntField(_spell.bonusDuration);
                }
            });

            HorizontalGUI(() =>
            {
                GUILayout.Label("One Time Use", GUILayout.Width(120.0f));
                _spell.oneTimeUse = EditorGUILayout.Toggle(_spell.oneTimeUse, GUILayout.Width(15.0f));
            });

            HorizontalGUI(() =>
            {
                GUILayout.Label("Unique Effect", GUILayout.Width(120.0f));
                _spell.uniqueEffect = EditorGUILayout.Toggle(_spell.uniqueEffect, GUILayout.Width(15.0f));
            });
        }

        if (_spell.spellAction == SpellActionType.ThrowProjectile)
        {
            HorizontalGUI(() =>
            {
                SerializedProperty _object = serializedObject.FindProperty("objectReference");
                EditorGUILayout.ObjectField(_object);
            });
        }

        EditorUtility.SetDirty(target);
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
