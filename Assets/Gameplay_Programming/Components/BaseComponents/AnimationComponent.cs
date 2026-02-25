using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationComponent : MonoBehaviour
{
    Animator Animator;

    [SerializeField] List<string> blockedAnimations = new List<string>();

    private void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
    }

    public void SetBool(string _transitionName, bool _value)
    {
        // if no animator => return
        if (!Animator) return;

        // if the animation is locked (name in list and ask to be true) => return
        if (blockedAnimations.Contains(_transitionName) && _value) return;

        // if the animation is locked and we want to put it to false => remove from the locked list
        blockedAnimations.Remove(_transitionName);

        // set the animation value
        Animator.SetBool(_transitionName, _value);
    }

    public void SetTrigger(string _transitionName)
    {
        // if no animator => return
        if (!Animator) return;

        // if the animation is locked (name in list and ask to be true) => return
        if (blockedAnimations.Contains(_transitionName)) return;

        // set the animation value
        Animator.SetTrigger(_transitionName);
    }

    void Anim_EndAttack()
    {
        // if no animator => return
        if (!Animator) return;

        // set the animation value
        Animator.SetBool("attack", false);

        // unlock the attack animation with 0.1 sec delay => can't block by spamming 
        Invoke(nameof(UnlockAttackAnimation), 0.1f);
    }

    void Anim_Death()
    {
        StartCoroutine(DownToDespawn());
    }

    IEnumerator DownToDespawn()
    {
        float _current = 0.0f;

        while (_current < 1.0f)
        {
            Vector3 _value = Vector3.down * Time.deltaTime / 2.0f;
            transform.position += _value;

            _current += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }

    public void LockAnimation(string _animName)
    {
        if (blockedAnimations.Contains(_animName)) return;

        blockedAnimations.Add(_animName);
    }

    void UnlockAttackAnimation() => blockedAnimations.Remove("attack");

}
