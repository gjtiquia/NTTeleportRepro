using Fusion;
using UnityEngine;

public class Pickup : NetworkBehaviour
{
    // NETWORKED MEMBERS
    [Networked(OnChanged = nameof(OnPlayerThatPickedUpChanged))] private Player _network_playerThatPickedUp { get; set; }
    private NetworkBool _network_isPickedUp => _network_playerThatPickedUp != null;

    // NetworkBehaviour INTERFACE
    public override void Spawned()
    {
        _network_playerThatPickedUp = null;
    }

    // PUBLIC METHODS
    public void Network_TryInteract(Player fromPlayer)
    {
        if (_network_isPickedUp)
            Network_TryDropdown();
        else
            Network_TryPickup(fromPlayer);
    }

    // PRIVATE METHODS
    private void Network_TryPickup(Player playerThatPickedUp)
    {
        if (_network_isPickedUp)
            return;

        Debug.Log("Picked up!");
        _network_playerThatPickedUp = playerThatPickedUp;
    }

    private void Network_TryDropdown()
    {
        if (!_network_isPickedUp)
            return;

        Debug.Log("Drop down!");
        _network_playerThatPickedUp = null;
    }

    private void Local_OnPlayerThatPickedUpChanged()
    {
        Debug.Log($"Local_OnPlayerThatPickedUpChanged: isPickedUp: {_network_isPickedUp}");

        if (_network_playerThatPickedUp != null)
        {
            transform.SetParent(_network_playerThatPickedUp.InterpolationTarget);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
        else
        {
            transform.SetParent(null);
        }
    }

    // OnChanged Callbacks
    private static void OnPlayerThatPickedUpChanged(Changed<Pickup> changed) => changed.Behaviour.Local_OnPlayerThatPickedUpChanged();
}