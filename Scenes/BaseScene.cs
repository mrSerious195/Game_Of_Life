using Godot;
using System;
using System.Runtime.InteropServices.Marshalling;

public abstract partial class BaseScene : Node
{
    [Signal]
    public delegate void ChangeSceneEventHandler(int IndexNewScene);
    public abstract int SceneIndex{ get; }
}
