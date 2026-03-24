using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VisualEffectComponent : MonoBehaviour
{
    [Serializable]
    public class CreatedEffectData
    {
        public VisualEffect effect;
        float currentCooldown;
        float cooldown;

        public CreatedEffectData(VisualEffect _effect, float _cooldown)
        {
            effect = _effect;
            currentCooldown = 0.0f;
            cooldown = _cooldown;
        }

        public bool LifetimeUpdate()
        {
            currentCooldown += Time.deltaTime;
            if (currentCooldown >= cooldown)
            {
                return true;
            }
            return false;
        }
    }

    [Header("Parameters")]
    [SerializeField] List<CreatedEffectData> createdVisualEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (CreatedEffectData _data in createdVisualEffect)
        {
            if (_data.LifetimeUpdate())
            {
                Destroy(_data.effect.gameObject);
                createdVisualEffect.Remove(_data);
                return;
            }
        }
    }

    public void CreateVisualEffect(VisualEffectAsset _visualEffect, EquipmentSlotType _slotEmplacement, float _lifetime)
    {
        VisualEffect _newEffect = null;
        Transform _parent = SearchTransfromFromSlot(_slotEmplacement);

        if (_parent)
        {
            _newEffect = Instantiate(GameManager.Instance.EmptyVisualEffect, _parent);
        }
        else
        {
            _newEffect = Instantiate(GameManager.Instance.EmptyVisualEffect, Vector3.zero, Quaternion.identity);
        }

        _newEffect.transform.localScale = Vector3.one;
        _newEffect.visualEffectAsset = _visualEffect;
        _newEffect.Play();

        createdVisualEffect.Add(new CreatedEffectData(_newEffect, _lifetime));
    }

    Transform SearchTransfromFromSlot(EquipmentSlotType _slotEmplacement)
    {
        PlayerVisualEquipmentComponent _component = GetComponent<PlayerVisualEquipmentComponent>();
        if (!_component) return null;

        return _component.GetTransformFromSlot(_slotEmplacement);
    }
}
