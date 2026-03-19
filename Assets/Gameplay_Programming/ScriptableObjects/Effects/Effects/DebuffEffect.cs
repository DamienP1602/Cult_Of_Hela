using UnityEngine;

[CreateAssetMenu(fileName = "Debuff Effect", menuName = "Scriptable Objects/CustomEffect/Debuff")]
public class DebuffEffect : CustomEffect
{

    public DebuffEffect(string _effectID,float _duration,bool _uniqueEffect) : base(_effectID,_duration,_uniqueEffect)
    {

    }

    public override float ActivateEffect(StatsComponent _statOwner, PlayerLevelComponent _level)
    {
        return 0.0f;
    }
}
