using UnityEngine;
using TMPro;

public static class CardSystems
{
    /// <summary>
    /// Higher Number is displayed on top. Default number is 1.
    /// </summary>
    public static Card SetCanvasSortingOrder(this Card Card, int i) { Card.Canvas.sortingOrder = i; return Card; }

    public static Card SetFont(this Card Card, TMP_FontAsset f) { Card.TMP.font = f; return Card; }

    public static Card SetFontStyle(this Card Card, FontStyles f) { Card.TMP.fontStyle = f; return Card; }

    public static Card SetFontScale(this Card Card, float f) { Card.TMP.fontSizeMax = Card.CanvasScaler.referenceResolution.x * .043125f * f; return Card; }

    public static Card AutoSizeFont(this Card Card, bool tf) { Card.TMP.enableAutoSizing = tf; return Card; }
    public static Card WordWrap(this Card Card, bool tf) { Card.TMP.enableWordWrapping = tf; return Card; }

    public static Card SetTextString(this Card Card, string s) { Card.TMP.text = s; return Card; }
    public static Card SetTextColor(this Card Card, Color c) { Card.TMP.color = c; return Card; }
    public static Card SetTextAlignment(this Card Card, TextAlignmentOptions a) { Card.TMP.alignment = a; return Card; }


    public static Card AutoSizeTextContainer(this Card Card, bool tf) { Card.TMP.autoSizeTextContainer = tf; return Card; }

    /// <summary>
    /// Use this to set the TMP Size.
    /// </summary>
    public static Card SetTMPSize(this Card Card, Vector2 v)
    {
        Card.TMP.rectTransform.sizeDelta = .45f * Card.CanvasScaler.referenceResolution.y * v / Cam.Io.Camera.orthographicSize;
        ; return Card;
    }

    /// <summary>
    /// Use this to set the sprites size.
    /// </summary>
    public static Card SetSpriteSize(this Card Card, Vector2 v) { Card.Parent.transform.localScale = v; return Card; }

    /// <summary>
    /// Use this to set the sprite & TMP size. Don't use if there is no TMP or the call will create an empty canvas etc.
    /// </summary>
    public static Card SetSizeAll(this Card Card, Vector2 v)
    {
        Card.Parent.transform.localScale = v;
        Card.TMP.rectTransform.sizeDelta = .45f * Card.CanvasScaler.referenceResolution.y * v / Cam.Io.Camera.orthographicSize;
        return Card;
    }

    /// <summary>
    /// Use this to set the sprites position.
    /// </summary>
    public static Card SetSpritePosition(this Card Card, Vector3 v) { Card.Parent.transform.position = v; return Card; }

    /// <summary>
    /// Use this to set the TMP position.
    /// </summary>
    public static Card SetTMPPosition(this Card Card, Vector3 v)
    {
        Vector2 spos = Cam.Io.Camera.WorldToScreenPoint(v);
        Vector2 ssize = new Vector2(Cam.Io.Camera.pixelWidth, Cam.Io.Camera.pixelHeight);

        Card.TMP.rectTransform.localPosition = new Vector2(spos.x - (ssize.x * .5f), spos.y - (ssize.y * .5f));
        return Card;
    }

    /// <summary>
    /// Use this to set the sprite & TMP position. Don't use if there is no TMP or the call will create an empty canvas etc.
    /// </summary>
    public static Card SetPositionAll(this Card Card, Vector3 v)
    {
        Vector2 spos = Cam.Io.Camera.WorldToScreenPoint(v);
        Vector2 ssize = new Vector2(Cam.Io.Camera.pixelWidth, Cam.Io.Camera.pixelHeight);

        Card.Parent.transform.position = v;
        Card.TMP.rectTransform.localPosition = new Vector2(spos.x - (ssize.x * .5f), spos.y - (ssize.y * .5f));
        return Card;
    }

    public static Card SetSprite(this Card Card, Sprite s) { Card.SpriteRenderer.sprite = s; return Card; }
    public static Card SetSpriteColor(this Card Card, Color c) { Card.SpriteRenderer.color = c; return Card; }

}


