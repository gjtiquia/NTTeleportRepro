using Fusion;
using UnityEngine;

public class Pickup : NetworkBehaviour
{
    [Networked] private NetworkBool _network_isPickedUp { get; set; }

    public override void Spawned()
    {
        _network_isPickedUp = false;
    }

    public void TryPickup()
    {
        if (_network_isPickedUp)
            return;

        Debug.Log("Picked up!");
        _network_isPickedUp = true;
    }
}