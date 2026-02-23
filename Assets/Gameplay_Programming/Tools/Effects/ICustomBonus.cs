using UnityEngine;

public interface ICustomBonus<T>
{
    void AddEffect(T _effect);
    void RemoveEffect(T _effect);
    void UpdateCustomEffect();
}
