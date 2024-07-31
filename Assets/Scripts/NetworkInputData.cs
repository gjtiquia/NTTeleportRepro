using UnityEngine;
using Fusion;

enum EButton
{
    Interact = 0,
}

public struct NetworkInputData : INetworkInput
{
    public NetworkButtons Buttons;
    public Vector2 Direction;
}