using UnityEngine;
using Fusion;
using System.Collections.Generic;

public class Player : NetworkBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _speed;

    [Header("Interact Settings")]
    [SerializeField] private float _interactRadius;
    [SerializeField] private ContactFilter2D _contactFilter;

    // NetworkBehaviour INTERFACE
    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            // Interact
            if ((data.buttons & NetworkInputData.INTERACT_BUTTON) != 0)
            {
                TryPickup();
            }

            // Movement
            data.direction.Normalize();

            var newPosition = transform.position;
            newPosition += (Vector3)(_speed * data.direction * Runner.DeltaTime);

            transform.position = newPosition;
        }
    }

    // PRIVATE METHODS
    private void TryPickup()
    {
        var colliderResults = new List<Collider2D>();
        var colliderCount = Runner.GetPhysicsScene2D().OverlapCircle(transform.position, _interactRadius, _contactFilter, colliderResults);
        if (colliderCount <= 0) return;

        foreach (var collider in colliderResults)
        {
            var pickup = collider.GetComponentInParent<Pickup>();
            if (pickup != null)
            {
                pickup.TryPickup();
                return;
            }
        }
    }

    // HELPERS
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _interactRadius);
    }
}