using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card
{
    public Card(string name, Transform parent)
    {
        CardGO = new GameObject(name);
        CardGO.transform.SetParent(parent, false);
    }

    public GameObject CardGO { get; private set; }

    public Clickable Clickable;

    private SpriteRenderer _sr;
    public SpriteRenderer SpriteRenderer => _sr != null ? _sr : _sr = CardGO.AddComponent<SpriteRenderer>();

    private Canvas _c;
    public Canvas Canvas
    {
        get
        {
            return _c != null ? _c : _c = SetUpCanvas();
            Canvas SetUpCanvas()
            {
                Canvas canvas = new GameObject(nameof(Canvas)).AddComponent<Canvas>();
                canvas.transform.SetParent(CardGO.transform, false);
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvas.sortingOrder = 1;

                if (_cs == null)
                {
                    _cs = SetUpCanvasScaler(canvas);
                }

                return canvas;
            }
        }
    }

    private CanvasScaler _cs;
    public CanvasScaler CanvasScaler
    {
        get
        {
            if (_cs == null) { _ = Canvas; _cs = SetUpCanvasScaler(Canvas); }
            return _cs;
        }
    }

    CanvasScaler SetUpCanvasScaler(Canvas canvas)
    {
        if (canvas.gameObject.TryGetComponent<CanvasScaler>(out CanvasScaler ca)) { return ca; }

        CanvasScaler cs = canvas.gameObject.AddComponent<CanvasScaler>();
        cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        cs.matchWidthOrHeight = 1;

        //This keeps text the same size on every screen regardless of aspect or resolution. Chose 1024x768 arbitrarily.
        cs.referenceResolution = new Vector2(1024 * Cam.Io.Camera.aspect, 768);

        //This set's the reference size to act like orthographic
        cs.referencePixelsPerUnit = cs.referenceResolution.y / (Cam.Io.Camera.orthographicSize * 2);

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


