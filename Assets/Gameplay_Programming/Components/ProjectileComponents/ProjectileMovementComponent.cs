using UnityEngine;

public class ProjectileMovementComponent : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] float movementSpeed = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveUpdate();
    }

    void MoveUpdate()
    {
        transform.position += transform.forward * Time.deltaTime * movementSpeed;
    }
}
