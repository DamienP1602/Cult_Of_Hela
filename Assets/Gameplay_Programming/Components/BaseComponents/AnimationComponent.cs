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
        StartCoroutine(DownToDespawn());
    }

    IEnumerator DownToDespawn()
    {
        float _current = 0.0f;

        while (_current < 1.0f)
        {
            Vector3 _value = Vector3.down * Time.deltaTime / 3.0f;
            transform.position += _value;

            _current += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }

}
