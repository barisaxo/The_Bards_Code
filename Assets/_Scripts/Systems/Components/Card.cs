using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card
{
    /// <summary>
    /// The basis for any simple 2D sprite and/or text.
    /// </summary>
    public Card(string name, Transform parent)
    {
        GO = new GameObject(name);
        GO.transform.SetParent(parent, false);
    }

    /// <summary>
    /// The parent GameObject. SpriteRenderer will be attatched to this, Canvas & TMP will be children.
    /// </summary>
    public GameObject GO { get; private set; }

    /// <summary>
    /// Do you want this card and/or Text to be clickable?
    /// </summary>
    public Clickable Clickable;

    private SpriteRenderer _sr;
    /// <summary>
    /// Lives on GO (the parent GameObject).
    /// </summary>
    public SpriteRenderer SpriteRenderer => _sr != null ? _sr : _sr = GO.AddComponent<SpriteRenderer>();

    private Canvas _canvas;
    public Canvas Canvas
    {
        get
        {
            return _canvas != null ? _canvas : _canvas = SetUpCanvas();
            Canvas SetUpCanvas()
            {
                Canvas canvas = new GameObject(nameof(Canvas)).AddComponent<Canvas>();
                canvas.transform.SetParent(GO.transform, false);
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvas.sortingOrder = 1;

                if (_canvasScaler == null)
                {
                    _canvasScaler = SetUpCanvasScaler(canvas);
                }

                return canvas;
            }
        }
    }

    private CanvasScaler _canvasScaler;
    public CanvasScaler CanvasScaler
    {
        get
        {
            if (_canvasScaler == null) { _canvasScaler = SetUpCanvasScaler(Canvas); }
            return _canvasScaler;
        }
    }

    CanvasScaler SetUpCanvasScaler(Canvas canvas)
    {
        if (canvas.gameObject.TryGetComponent<CanvasScaler>(out CanvasScaler ca)) { return ca; }

        CanvasScaler cs = canvas.gameObject.AddComponent<CanvasScaler>();
        cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        cs.matchWidthOrHeight = 1;
        cs.referenceResolution = new Vector2(Cam.Io.Camera.pixelWidth, Cam.Io.Camera.pixelHeight);

        return cs;
    }

    private TextMeshProUGUI _tmp;
    public TextMeshProUGUI TMP
    {
        get
        {
            return _tmp != null ? _tmp : _tmp = SetUpTMP();

            TextMeshProUGUI SetUpTMP()
            {
                TextMeshProUGUI t = new GameObject(nameof(TMP)).AddComponent<TextMeshProUGUI>();
                t.transform.SetParent(Canvas.transform, true);
                t.fontSizeMin = 8;
                t.fontSizeMax = 300;

                return t;
            }
        }
    }

    public string TextString { get => TMP.text; set => TMP.text = value; }
}


