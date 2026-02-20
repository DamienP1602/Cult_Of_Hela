using System;
using UnityEngine;
using UnityEngine.Events;

public class Transition
{
    public State nextState;
    public Func<bool> requirement;

    public Transition(State _nextState, Func<bool> _requirement)
    {
        nextState = _nextState;
        requirement = _requirement;
    }
}
