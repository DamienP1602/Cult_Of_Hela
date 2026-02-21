using System.Collections;
using UnityEngine;

public class AnimationComponent : MonoBehaviour
{
    Animator Animator;

    Material[] mats;

    private void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
    }

    public void SetBool(string _transitionName, bool _value)
    {
        if (Animator)
            Animator.SetBool(_transitionName, _value);
    }

    public void SetTrigger(string _transitionName)
    {
        if (Animator)
            Animator.SetTrigger(_transitionName);
    }

    void Anim_EndAttack()
    {
        if (Animator)
            Animator.SetBool("attack", false);
    }

    void Anim_Death()
    {
        Destroy(gameObject);
    }

}
