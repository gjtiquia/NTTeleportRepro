using UnityEngine;
using Fusion;

public class Player : NetworkBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _speed;

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            data.direction.Normalize();

            var newPosition = transform.position;
            newPosition += (Vector3)(_speed * data.direction * Runner.DeltaTime);

            transform.position = newPosition;
        }
    }
}