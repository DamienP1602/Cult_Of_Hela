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

        BaseEntity _owner = GetComponent<BaseEntity>();
        _spell.LaunchSpell(_owner);
    }
}
