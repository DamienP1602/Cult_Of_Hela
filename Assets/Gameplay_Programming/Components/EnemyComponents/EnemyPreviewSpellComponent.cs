using UnityEngine;
using UnityEngine.VFX;

public class EnemyPreviewSpellComponent : MonoBehaviour
{
    [SerializeField] VisualEffectAsset previousEffect;
    VisualEffect createdEffect;
    bool updateAlpha;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (updateAlpha)
        {
            float _value = createdEffect.GetFloat("alpha");
            _value += Time.deltaTime;

            createdEffect.SetFloat("alpha", _value);
            if (_value >= 0.3f)
                updateAlpha = false;
        }
    }

    void Init()
    {
        SpellBookComponent _spellBook = GetComponent<SpellBookComponent>();
        _spellBook.OnLaunchSpell += CreateEffect;
        _spellBook.OnStartSpell += DestroyEffect;
    }

    void CreateEffect(Spell _spell)
    {
        createdEffect = Instantiate(GameManager.Instance.EmptyVisualEffect,transform);
        createdEffect.visualEffectAsset = previousEffect;

        createdEffect.SetFloat("size",_spell.areaOfEffect * 2.0f);
        updateAlpha = true;
    }

    void DestroyEffect()
    {
        Destroy(createdEffect.gameObject);
        updateAlpha = false;
    }
}
