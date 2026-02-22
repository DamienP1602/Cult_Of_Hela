using System.Collections.Generic;
using UnityEngine;

public class SpellBookComponent : MonoBehaviour
{
    [SerializeField] List<Ability> allLearnedAbilities;
    [SerializeField] List<Spell> bindedSpells;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void LaunchAbility(int _index)
    {
        if (_index >= bindedSpells.Count) return;

        Spell _spell = bindedSpells[_index];
        Debug.Log(_spell.AbilityName);
    }
}
