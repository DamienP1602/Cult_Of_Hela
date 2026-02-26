using System;
using UnityEngine;

[Serializable]
public class ProjectileOnHitComponent : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] int damageAmount;
    [SerializeField] CustomEffect debuffEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitOnHitEffects(int _damage, CustomEffect _effect)
    {
        damageAmount = _damage;
        debuffEffect = _effect;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BaseEntity>() is BaseEntity _entity)
        {
            _entity.StatsComponent.LooseHealth(damageAmount);

            if (debuffEffect)
            {
                // effect
            }

            Destroy(gameObject);
        }
    }
}
