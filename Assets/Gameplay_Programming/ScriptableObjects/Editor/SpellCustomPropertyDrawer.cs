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

        HorizontalGUI(() =>
        {
            GUILayout.Label("Has Animation", GUILayout.Width(120.0f));
            _spell.hasAnimation = EditorGUILayout.Toggle(_spell.hasAnimation, GUILayout.Width(15.0f));

            if (_spell.hasAnimation)
            {
                GUILayout.Label("Animation Name", GUILayout.Width(100.0f));
                _spell.animationName = EditorGUILayout.TextField(_spell.animationName);
            }

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
                GUILayout.Label("Attack Bonus", GUILayout.Width(120.0f));
                _spell.effect = (CustomEffect)EditorGUILayout.ObjectField(_spell.effect, typeof(CustomEffect),true);
            });
            
        }

        if (_spell.spellAction == SpellActionType.ThrowProjectile)
        {
            HorizontalGUI(() =>
            {
                GUILayout.Label("Projectile", GUILayout.Width(120.0f));
                _spell.objectReference = (GameObject)EditorGUILayout.ObjectField(_spell.objectReference, typeof(GameObject), true);
            });

            HorizontalGUI(() =>
            {
                GUILayout.Label("Projectile Damages", GUILayout.Width(120.0f));
                _spell.spellValue = EditorGUILayout.IntField(_spell.spellValue);
            });

            HorizontalGUI(() =>
            {
                GUILayout.Label("Projectile Effect on Hit", GUILayout.Width(150.0f));
                _spell.effect = (CustomEffect)EditorGUILayout.ObjectField(_spell.effect, typeof(CustomEffect), true);
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
