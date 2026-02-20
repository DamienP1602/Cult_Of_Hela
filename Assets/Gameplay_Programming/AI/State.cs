using System;
using System.Collections.Generic;

[Serializable]
public abstract class State
{
    public string StateName { get; set; }
    public bool isStarted;
    public List<Transition> allTransitions = new List<Transition>();

    public State(string _stateName)
    {
        StateName = _stateName;
    }

    public abstract void Start(EnemyEntity _owner);

    public abstract void Update(EnemyEntity _owner);

    public abstract void Stop(EnemyEntity _owner);
}
