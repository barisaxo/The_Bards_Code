using System;
using UnityEngine;

public class BootStrap_State : State
{
    private BootStrap_State() { }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Initialize()
    {
        BootStrap_State state = new();
        state.SetStateDirectly(state);
    }

    protected override void PrepareState(Action callback)
    {
        _ = Cam.Io;
        callback();
    }

    protected override void EngageState()
    {
        SetStateDirectly(new MainMenu_State());//new InputTest_State());//
    }
}
