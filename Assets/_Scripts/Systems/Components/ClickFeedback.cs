using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading.Tasks;
using TMPro;

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
    private readonly List<(SpriteRenderer sr, Color nativeColor)> spriteFeedback = new();
    private readonly List<(TextMeshProUGUI tmp, Color nativeColor)> tmpFeedback = new();

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
                    spriteFeedback.Add((sr, sr.color));
                }

                foreach (TextMeshProUGUI tmp in _clickedGO.GetComponentsInChildren<TextMeshProUGUI>())
                {
                    tmpFeedback.Add((tmp, tmp.color));
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

                RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero);

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
        for (int i = 0; i < spriteFeedback.Count; i++)
        {
            spriteFeedback[i].sr.color *= Color.gray;
        }

        for (int i = 0; i < tmpFeedback.Count; i++)
        {
            tmpFeedback[i].tmp.color *= new Color(1, 1, 1, .65f);
        }
    }

    private void RestoreColor()
    {
        for (int i = 0; i < spriteFeedback.Count; i++)
        {
            spriteFeedback[i].sr.color = spriteFeedback[i].nativeColor;
        }
        for (int i = 0; i < tmpFeedback.Count; i++)
        {
            tmpFeedback[i].tmp.color = tmpFeedback[i].nativeColor;
        }
        spriteFeedback.Clear();
        tmpFeedback.Clear();
    }

}

public static class ClickableSystems
{
    public static GameObject GetClickable(this Card c)
    {
        var clickable = c.CardGO.GetComponentInChildren<Clickable>();
        return clickable.gameObject != null ? clickable.gameObject : null;
    }
}