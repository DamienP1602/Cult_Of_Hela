using UnityEngine;

public class SpellState : State
{
    SpellBookComponent spellBook;
    AnimationComponent animationComp;

    bool endState;
    bool canUseSpell;

    public SpellState(string _stateName) : base(_stateName) { }

    public override void Start(EnemyEntity _owner)
    {
        spellBook = _owner.SpellBookComponent;
        animationComp = _owner.AnimationComponent;

        endState = false;
        canUseSpell = true;

        animationComp.OnEndSpellAnimation += SetEndState;

        isStarted = true;
    }

    public override void Update(EnemyEntity _owner)
    {
        if (canUseSpell)
        {
            int _index = spellBook.GetRandomSpellIndex();
            spellBook.LaunchAbility(_index);
            canUseSpell = false;
        }
    }

    public override void Stop(EnemyEntity _owner)
    {
        isStarted = false;
        animationComp.OnEndSpellAnimation -= SetEndState;
    }

    public bool ShouldEndState() => endState;

    void SetEndState() => endState = true;
}
