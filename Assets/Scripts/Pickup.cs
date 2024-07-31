using Fusion;
using UnityEngine;

public class Pickup : NetworkBehaviour
{
    // NETWORKED MEMBERS
    [Networked] private NetworkBool _network_isPickedUp { get; set; }

    // NetworkBehaviour INTERFACE
    public override void Spawned()
    {
        _network_isPickedUp = false;
    }

    // PUBLIC METHODS
    public void TryInteract()
    {
        if (_network_isPickedUp)
            TryDropdown();
        else
            TryPickup();
    }

    // PRIVATE METHODS
    private void TryPickup()
    {
        if (_network_isPickedUp)
            return;

        Debug.Log("Picked up!");
        _network_isPickedUp = true;
    }

    private void TryDropdown()
    {
        if (!_network_isPickedUp)
            return;

        Debug.Log("Drop down!");
        _network_isPickedUp = false;
    }
}