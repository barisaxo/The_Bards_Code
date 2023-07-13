using UnityEngine;
using UnityEngine.Video;

namespace Dialog
{
    public sealed class Dialog
    {
        public Dialog(Dialogue dialogue)
        {
            Dialogue = dialogue;
            _ = DialogCard;
            _ = TextBackground;

            CurrentLine = Dialogue.FirstLine;
            NPCIcon(Dialogue.FirstLine);
        }

        public void SelfDestruct()
        {
            UnityEngine.Object.Destroy(_parent);
        }

        public bool LetType;
        public Line CurrentLine;
        public Dialogue Dialogue;

        private GameObject _parent;
        public GameObject Parent => _parent != null ? _parent : _parent = new GameObject(nameof(Dialog));

        private SpriteRenderer _textBackground;
        private SpriteRenderer TextBackground
        {
            get
            {
                return _textBackground != null ? _textBackground : _textBackground = SetUpBackGround(); SpriteRenderer SetUpBackGround()
                {
                    SpriteRenderer sr = new GameObject(nameof(TextBackground)).AddComponent<SpriteRenderer>();
                    sr.transform.SetParent(Parent.transform);
                    sr.transform.localScale = Vector2.one * 200;
                    sr.transform.position = Vector3.back * .5f;
                    sr.sprite = Assets.White;
                    sr.color = new Color(0f, 0f, 0f, .666f);
                    return sr;
                }
            }
        }

        private GameObject _npcIcon;
        public void NPCIcon(Line line) => NPCIcon(line.SpeakerIcon, line.SpeakerColor);
        void NPCIcon(Sprite[] sprites, Color c)
        {
            if (_npcIcon != null) UnityEngine.Object.DestroyImmediate(_npcIcon);
            if (sprites != null) _npcIcon = SetUpNPCIcon();

            GameObject SetUpNPCIcon()
            {
                GameObject go = new GameObject(nameof(NPCIcon));
                go.transform.SetParent(Parent.transform);

                SpriteRenderer[] srs = new SpriteRenderer[sprites.Length];

                for (int i = 0; i < srs.Length; i++)
                {
                    SpriteRenderer sr = new GameObject(nameof(Sprite)).AddComponent<SpriteRenderer>();
                    sr.transform.SetParent(go.transform);
                    sr.transform.position = DialogCard.GO.transform.position +
                        new Vector3((-DialogCard.GO.transform.localScale.x * .5f) - .75f,
                            (DialogCard.GO.transform.localScale.y * .35f),
                            -4 - (i * .1f));
                    sr.sprite = sprites[i];
                    sr.transform.localScale = Vector3.one * 2f;
                    sr.color = c;
                }
                return go;
            }
        }

        private Card _dialogCard;
        public Card DialogCard => _dialogCard ??= new Card(nameof(DialogCard), Parent.transform)
            .SetTextAlignment(TMPro.TextAlignmentOptions.TopLeft)
            .SetSizeAll(new Vector2(3.5f * Cam.Io.Camera.aspect * 2, 4f))
            .SetPositionAll(new Vector3(0, 2.5f, -1.5f))
            .SetFontScale(.65f)
            .AutoSizeFont(true)
            .WordWrap(true)
            .SetSprite(Assets.White)
            .SetSpriteColor(new Color(.15f, .15f, .15f, .65f))
            .SpriteClickable();


        private VideoPlayer _videoPlayer;
        public VideoPlayer VideoPlayer => _videoPlayer ??= SetUpVideo();
        VideoPlayer SetUpVideo()
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Quad);
            go.GetComponent<MeshRenderer>().material = Assets.Video_Mat;
            go.name = nameof(VideoPlayer);
            go.transform.SetParent(Parent.transform);
            go.transform.position = new Vector3(0, -1, -1f);
            VideoPlayer v = go.AddComponent<VideoPlayer>();
            v.playOnAwake = false;
            return v;
        }
    }
}