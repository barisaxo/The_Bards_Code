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
    public SpriteRenderer SpriteRenderer
    {
        get
        {
            return _sr != null ? _sr : _sr = SetUpSR();

            SpriteRenderer SetUpSR()
            {
                SpriteRenderer sr = CardGO.AddComponent<SpriteRenderer>();
                return sr;
            }
        }
    }

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
                // canvas.worldCamera = Cam.Io.UICam;
                canvas.sortingOrder = 1;
                // canvas.gameObject.layer = 5;
                _cs = SetUpCanvasScaler(canvas);
                return canvas;
            }
        }
    }

    private CanvasScaler _cs;
    public CanvasScaler CanvasScaler => _cs;

    CanvasScaler SetUpCanvasScaler(Canvas canvas)
    {
        CanvasScaler cs = canvas.gameObject.AddComponent<CanvasScaler>();
        cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        cs.matchWidthOrHeight = 1;
        cs.referenceResolution = new Vector2(1024 * Cam.Io.Camera.aspect, 768);
        cs.referencePixelsPerUnit = cs.referenceResolution.y * .1f;//new Vector2(Cam.Io.Camera.pixelWidth, Cam.Io.Camera.pixelHeight);
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


