using System;
using System.Collections;
using UnityEngine;

public class MonoHelper : MonoBehaviour
{
    #region INSTANCE
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void AutoInit()
    {
        DontDestroyOnLoad(Io);
    }

    public static MonoHelper Io => Instance.Io;

    private class Instance
    {
        static Instance() { }
        static MonoHelper _io;
        internal static MonoHelper Io => _io != null ? _io :
            _io = new GameObject(nameof(MonoHelper)).AddComponent<MonoHelper>();
    }

    private void Start()
    {
        if (this != Io) { Destroy(gameObject); }
    }
    #endregion INSTANCE

    public static event Action OnUpdate;
    private void Update() => OnUpdate?.Invoke();
    public static event Action OnLateUpdate;
    private void LateUpdate() => OnLateUpdate?.Invoke();
}

public static class MonoSystems
{
    public static void StartCoroutine(this IEnumerator ie) => MonoHelper.Io.StartCoroutine(ie);
}