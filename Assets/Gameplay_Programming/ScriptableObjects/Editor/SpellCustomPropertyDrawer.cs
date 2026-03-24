using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.VFX;

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

        HorizontalGUI(() =>
        {
            GUILayout.Label("Ability Sprite", GUILayout.Width(120.0f));
            _spell.abilitySprite = (Sprite)EditorGUILayout.ObjectField(_spell.abilitySprite, typeof(Sprite), true);
        });

        HorizontalGUI(() =>
        {
            GUILayout.Label("Ability Sprite Color", GUILayout.Width(120.0f));
            _spell.abilitySpriteColor = EditorGUILayout.ColorField(_spell.abilitySpriteColor);
        });

        HorizontalGUI(() =>
        {
            GUILayout.Label("Description", GUILayout.Width(120.0f));
            _spell.AbilityDescription = EditorGUILayout.TextField(_spell.AbilityDescription);
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
            GUILayout.Label("Visual Effect", GUILayout.Width(120.0f));
            _spell.visualEffect = (VisualEffectAsset)EditorGUILayout.ObjectField(_spell.visualEffect, typeof(VisualEffectAsset), true);
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

        DrawTitle("Spell Requirement");
        HorizontalGUI(() =>
        {
            GUILayout.Label("Has Requirement", GUILayout.Width(120.0f));
            _spell.hasRequirement = EditorGUILayout.Toggle(_spell.hasRequirement, GUILayout.Width(15.0f));

            if (_spell.hasRequirement)
            {
                GUILayout.Label("Need to Have", GUILayout.Width(90.0f));
                _spell.EquipmentRequirement = (EquipmentSlotType)EditorGUILayout.EnumPopup(_spell.EquipmentRequirement);
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

        if (_spell.spellAction == SpellActionType.MeleeAttack)
        {
            HorizontalGUI(() =>
            {
                GUILayout.Label("Strength Percent", GUILayout.Width(160.0f));
                _spell.strengthPercent = EditorGUILayout.FloatField(_spell.strengthPercent);
            });

            HorizontalGUI(() =>
            {
                GUILayout.Label("Attack Bonus Percent", GUILayout.Width(160.0f));
                _spell.bonusAttackPercent = EditorGUILayout.FloatField(_spell.bonusAttackPercent);
            });

            HorizontalGUI(() =>
            {
                GUILayout.Label("Spell Bonus Percent", GUILayout.Width(160.0f));
                _spell.bonusSpellPercent = EditorGUILayout.FloatField(_spell.bonusSpellPercent);
            });

            HorizontalGUI(() =>
            {
                GUILayout.Label("Mono Target", GUILayout.Width(120.0f));
                _spell.monoTarget = EditorGUILayout.Toggle(_spell.monoTarget, GUILayout.Width(15.0f));

                if (!_spell.monoTarget)
                {
                    GUILayout.Label("Area of Effect (m)", GUILayout.Width(120.0f));
                    _spell.areaOfEffect = EditorGUILayout.FloatField(_spell.areaOfEffect);
                }
            });

            HorizontalGUI(() =>
            {
                GUILayout.Label("In Front Only", GUILayout.Width(120.0f));
                _spell.inFront = EditorGUILayout.Toggle(_spell.inFront, GUILayout.Width(15.0f));
            });
        }

        if (_spell.spellAction == SpellActionType.StatsBonus)
        {
            HorizontalGUI(() =>
            {
                GUILayout.Label("Bonus Effect", GUILayout.Width(120.0f));
                _spell.effect = (CustomEffect)EditorGUILayout.ObjectField(_spell.effect, typeof(CustomEffect), true);
            });
        }

        if (_spell.spellAction == SpellActionType.SpecialAttack)
        {
            HorizontalGUI(() =>
            {
                GUILayout.Label("Damages", GUILayout.Width(120.0f));
                _spell.effect = (CustomEffect)EditorGUILayout.ObjectField(_spell.effect, typeof(CustomEffect), true);
            });

            HorizontalGUI(() =>
            {
                GUILayout.Label("Special Object", GUILayout.Width(120.0f));
                _spell.specialObjectToSpawn = (GameObject)EditorGUILayout.ObjectField(_spell.specialObjectToSpawn, typeof(GameObject), true);
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
