using UnityEngine;

public class SpellState : State
{
    SpellBookComponent spellBook;

    public SpellState(string _stateName) : base(_stateName) { }

    public override void Start(EnemyEntity _owner)
    {
        spellBook = _owner.SpellBookComponent;

        isStarted = true;
    }

    public override void Update(EnemyEntity _owner)
    {

    }

    public override void Stop(EnemyEntity _owner)
    {
        isStarted = false;
    }
}
