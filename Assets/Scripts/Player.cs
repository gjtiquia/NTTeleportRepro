using UnityEngine;
using Fusion;
using System.Collections.Generic;

public class Player : NetworkBehaviour
{
    // SERIALIZED MEMBERS
    [Header("Movement Settings")]
    [SerializeField] private float _speed;

    [Header("Interact Settings")]
    [SerializeField] private float _interactRadius;
    [SerializeField] private ContactFilter2D _contactFilter;

    // NETWORKED MEMBERS
    [Networked] private NetworkButtons _previousButtons { get; set; }

    // NetworkBehaviour INTERFACE
    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData input))
        {
            // Get pressed/released state and store latest input as previous
            var pressed = input.Buttons.GetPressed(_previousButtons);
            _previousButtons = input.Buttons;

            // Interact
            if (pressed.IsSet(EButton.Interact))
                TryInteract();

            // Movement
            input.Direction.Normalize();

            var newPosition = transform.position;
            newPosition += (Vector3)(_speed * input.Direction * Runner.DeltaTime);

            transform.position = newPosition;
        }
    }

    // PRIVATE METHODS
    private void TryInteract()
    {
        var colliderResults = new List<Collider2D>();
        var colliderCount = Runner.GetPhysicsScene2D().OverlapCircle(transform.position, _interactRadius, _contactFilter, colliderResults);
        if (colliderCount <= 0) return;

        foreach (var collider in colliderResults)
        {
            var pickup = collider.GetComponentInParent<Pickup>();
            if (pickup != null)
            {
                pickup.TryInteract();
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