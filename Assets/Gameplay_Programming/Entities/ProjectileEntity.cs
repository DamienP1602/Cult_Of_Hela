using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ProjectileEntity : GameEntity
{



    protected override void EventAssignation()
    {

    }

    protected override void Init()
    {

    }

    private void Update()
    {
        transform.position += (transform.forward * 2.0f) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyEntity>())
        {
            Destroy(gameObject);
        }

    }

}
