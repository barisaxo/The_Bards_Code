using System.Collections.Generic;
using UnityEngine;

public class RockTheBoat
{
    private bool _rocking;
    public bool Rocking
    {
        get => _rocking;
        set
        {
            if (_rocking = value) MonoHelper.OnLateUpdate += SetNewSwayPos;
            else MonoHelper.OnLateUpdate -= SetNewSwayPos;
        }
    }

    readonly List<(Transform transform, float amp, float period)> Boats = new();

    public void AddBoat(Transform t) =>
        Boats.Add((
                transform: t,
                amp: Random.Range(7f, 9f),
                period: Random.value + .5f));

    void SetNewSwayPos()
    {
        foreach (var (transform, amp, period) in Boats)
            transform.rotation =
                Quaternion.Euler(new Vector3(
                    transform.localEulerAngles.x,
                    transform.localEulerAngles.y,
                    Mathf.Sin(Time.time * period) * amp));
    }
}
