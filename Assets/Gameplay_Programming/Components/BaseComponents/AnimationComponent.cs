using UnityEngine;

public class AnimationComponent : MonoBehaviour
{
    Animator Animator;

    private void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
    }

    public void SetBool(string _transitionName, bool _value)
    {
        if (Animator)
            Animator.SetBool(_transitionName, _value);
    }

    void Anim_EndAttack()
    {
        if (Animator)
            Animator.SetBool("attack", false);
    }

}
