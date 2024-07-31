using UnityEngine;
using Fusion;

public struct NetworkInputData : INetworkInput
{
    public const byte INTERACT_BUTTON = 0x01;

    public byte buttons;
    public Vector2 direction;
}