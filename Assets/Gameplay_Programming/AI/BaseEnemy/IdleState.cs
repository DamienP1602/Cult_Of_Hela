using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(string _stateName) : base(_stateName) { }

    public override void Start(EnemyEntity _owner)
    {
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
