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
            // Interact
            if ((data.buttons & NetworkInputData.INTERACT_BUTTON) != 0)
            {
                // TODO
                Debug.Log("Interact!");
            }

            // Movement
            data.direction.Normalize();

            var newPosition = transform.position;
            newPosition += (Vector3)(_speed * data.direction * Runner.DeltaTime);

            transform.position = newPosition;
        }
    }
}