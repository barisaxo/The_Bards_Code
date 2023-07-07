using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using System.Threading.Tasks;
using UnityEngine.InputSystem.EnhancedTouch;

[RequireComponent(typeof(BoxCollider2D))]
public class Clickable : MonoBehaviour { }

internal sealed class ClickFeedback
{
    #region INSTANCE
    private ClickFeedback() { }

    static private ClickFeedback Io => Instance.Io;

    private class Instance
    {
        static Instance() { }
        static ClickFeedback _io;
        internal static ClickFeedback Io => _io ??= new ClickFeedback();
        internal static void Destruct() => _io = null;
    }

    //public void SelfDestruct()
    //{
    //    InputKey.MouseClickEvent -= Io.Clicked;
    //    Instance.Destruct();
    //}
    #endregion INSTANCE

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void AutoInit() => InputKey.MouseClickEvent += Io.Clicked;

    private bool holding;
    private readonly List<(SpriteRenderer spriteRenderer, Color nativeColor)> feedback = new();

    private GameObject _clickedGO;
    private GameObject ClickedGO
    {
        set
        {
            if (value == null && _clickedGO != null)
            {
                RestoreColor();
                _clickedGO = null;
            }
            else if (_clickedGO != value)
            {
                if (_clickedGO != null)
                {
                    RestoreColor();
                }

                _clickedGO = value;

                foreach (SpriteRenderer sr in _clickedGO.GetComponentsInChildren<SpriteRenderer>())
                {
                    feedback.Add((sr, sr.color));
                }

                AlterColor();
            }
        }
    }

    private void Clicked(MouseAction action, Vector2 position)
    {
        switch (action)
        {
            case MouseAction.LUp:
                holding = false;
                Io.ClickedGO = null;
                break;

            case MouseAction.LHold:
                holding = true;
                // LHolding();

                RaycastHit2D hit = Physics2D.Raycast(Cam.Io.Camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (hit.collider != null && hit.collider.gameObject.TryGetComponent<Clickable>(out _))
                {
                    Io.ClickedGO = hit.collider.gameObject;
                }
                else
                {
                    Io.ClickedGO = null;
                }
                break;
        }
    }

    private async void LHolding()
    {
        while (holding)
        {
            await Task.Yield();
            if (!Application.isPlaying) return;

            RaycastHit2D hit = Physics2D.Raycast(Cam.Io.Camera.ScreenToWorldPoint(GetPositionOrDefault()), Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject.TryGetComponent<Clickable>(out _))
            {
                Io.ClickedGO = hit.collider.gameObject;
            }
            else
            {
                Io.ClickedGO = null;
            }
        }
    }

    private Vector2 GetPositionOrDefault()
    {
        if (Touchscreen.current != null && Touchscreen.current.position.ReadValue() != Vector2.zero)
        {
            return Touchscreen.current.position.ReadValue();
        }

        if (Mouse.current != null && Mouse.current.position.ReadValue() != Vector2.zero)
        {
            return Mouse.current.position.ReadValue();
        }

        return Vector2.zero;
    }

    private void AlterColor()
    {
        for (int i = 0; i < feedback.Count; i++)
        {
            feedback[i].spriteRenderer.color *= Color.gray;
        }
    }

    private void RestoreColor()
    {
        for (int i = 0; i < feedback.Count; i++)
        {
            feedback[i].spriteRenderer.color = feedback[i].nativeColor;
        }
        feedback.Clear();
    }

}

public static class ClickableSystems
{
    public static GameObject GetClickable(this Card c)
    {
        var clickable = c.CardGO.GetComponentInChildren<Clickable>();
        Debug.Log(clickable);
        return clickable.gameObject != null ? clickable.gameObject : null;
    }
}