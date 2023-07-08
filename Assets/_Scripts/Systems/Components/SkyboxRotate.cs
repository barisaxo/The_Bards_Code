using UnityEngine;
//using System;

public sealed class SkyboxRotate
{
    private SkyboxRotate() { }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void AutoInit()
    {
        Skybox = RenderSettings.skybox = Assets.Stars;
        Skybox.SetFloat("_Rotation", Random.Range(-180, 180));
        RotSpeed = .04f * Random.value < .5f ? 1 : -1;
        MonoHelper.OnUpdate += Rotate;
    }

    static Material Skybox;
    static float RotSpeed;

    static void Rotate() =>
        Skybox.SetFloat("_Rotation", Skybox.GetFloat("_Rotation") + Time.deltaTime * RotSpeed);
}
