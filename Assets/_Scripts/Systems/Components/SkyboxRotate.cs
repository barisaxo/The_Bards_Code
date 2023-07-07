using UnityEngine;
using System.Threading.Tasks;

public sealed class SkyboxRotate
{
    private SkyboxRotate() { }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void AutoInit()
    {
        Material skybox = RenderSettings.skybox = Assets.Stars;
        skybox.SetFloat("_Rotation", Random.Range(-180, 180));
        Rotate(.04f * Random.value < .5f ? 1 : -1, skybox);
    }

    static async void Rotate(float rotSpeed, Material skybox)
    {
        while (Application.isPlaying)
        {
            await Task.Yield();
            if (!Application.isPlaying) return;
            skybox.SetFloat("_Rotation", skybox.GetFloat("_Rotation") + Time.deltaTime * rotSpeed);
        }
    }
}
