using System;
using UnityEngine;

public class BootStrap_State : State
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Initialize()
    {
        BootStrap_State state = new();

        state.SetStateDirectly(state);
    }

    //public BootStrap_State()
    //{

    //}

    protected override void PrepareState(Action callback)
    {
        _ = Cam.Io;
        callback();
    }

    protected override void EngageState()
    {
        SetStateDirectly(new InputTest_State());
    }
}
