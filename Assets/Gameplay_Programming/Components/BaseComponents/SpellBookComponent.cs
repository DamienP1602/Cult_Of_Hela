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
        // Don't have the selected spell
        if (_index >= bindedSpells.Count) return;
        Spell _spell = bindedSpells[_index];

        StatsComponent _ownerStats = GetComponent<StatsComponent>();
        // can't use the spell : not enough ressources
        if (_spell.ressourceCost > _ownerStats.ressource.Value) return;

        BaseEntity _owner = GetComponent<BaseEntity>();
        if (_spell.LaunchSpell(_owner))
        {
            // Consume ressource
            _ownerStats.ressource.RemoveValue(_spell.ressourceCost);
        }
    }
}
