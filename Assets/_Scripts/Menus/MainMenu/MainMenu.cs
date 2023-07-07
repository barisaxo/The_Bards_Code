using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Menus.Main
{
    public class MainMenu
    {
        #region INSTANCE

        public MainMenu()
        {
            _ = Parent;

            //_ = Canvas;
            //_ = CanvasScaler;

            //_ = Continue_Text;
            //_ = Options_Text;
            //_ = NewGame_Text;
            //_ = LoadGame_Text;
            //_ = HowTo_Text;
            //_ = Quit_Text;

            _ = CatBoat;
            _ = LightHouse;
            _ = LightA;
            _ = LightB;

            this.SetActiveItems();
            this.ColorTexts();
            this.ScrollMenuOptions(Dir.Reset);
        }

        //public static MainMenu Io => Instance.Io;

        //private class Instance
        //{
        //    static Instance() { }
        //    static MainMenu io; internal static MainMenu Io => io ??= new MainMenu();
        //    internal static void Destruct() => io = null;
        //}

        public void SelfDestruct()
        {
            Object.Destroy(_parent.gameObject);
            Resources.UnloadUnusedAssets();
        }

        private Transform _parent;
        public Transform Parent => _parent != null ? _parent : _parent = new GameObject(nameof(MainMenu)).transform;

        #endregion INSTANCE

        //#region CANVAS


        //public Camera Camera => Cam.Io.Camera;

        ////private Canvas canvas;
        ////public Canvas Canvas => canvas != null ? canvas : canvas = canvas.GetCanvas(Parent.transform);

        ////private CanvasScaler canvasScaler;
        ////public CanvasScaler CanvasScaler => canvasScaler == null ?
        ////   canvasScaler = canvasScaler.GetCanvasScaler(Canvas, Camera) : canvasScaler;

        //#endregion CANVAS

        #region STRING FIELDS

        public const string _continueString = "CONTINUE";
        public const string _loadString = "LOAD GAME";
        public const string _newString = "NEW GAME";
        public const string _optionsString = "OPTIONS";
        public const string _howToString = "HOW TO PLAY";
        public const string _quitString = "QUIT";

        #endregion STRING FIELDS

        #region MENU OBJECTS

        public MainMenuItem CurrItem = 0;

        private Card _continue;
        public Card Continue => _continue ??= new Card(nameof(Continue), Parent)
            .SetTextString(_continueString)
            .SetTMPSize(new Vector2(3, 1))
            .SetTMPPosition(new Vector2(3, 0.8f))
            .SetTextAlignment(TextAlignmentOptions.Right)
            .SetFontScale(1);

        private Card _loadGame;
        public Card LoadGame => _loadGame ??= new Card(nameof(LoadGame), Parent)
            .SetTextString(_loadString)
            .SetTMPSize(new Vector2(3, 1))
            .SetTMPPosition(new Vector2(3, 0f))
            .SetTextAlignment(TextAlignmentOptions.Right)
            .SetFontScale(1);


        //private TextMeshProUGUI newGame_Text;
        //public TextMeshProUGUI NewGame_Text => newGame_Text != null ? newGame_Text :
        //    newGame_Text = newGame_Text.SetUpText(
        //        canvas: Canvas,
        //        canvasScaler: CanvasScaler,
        //        cam: Camera,
        //        name: nameof(NewGame_Text),
        //        text: _new,
        //        size: new Vector2(3, 1),
        //        pos: new Vector2(Helper.PerWidth(3, Camera), -.8f),
        //        alignment: TextAlignmentOptions.Right,
        //        fontScale: 1f);

        //private TextMeshProUGUI options_Text;
        //public TextMeshProUGUI Options_Text => options_Text != null ? options_Text :
        //    options_Text = options_Text.SetUpText(
        //        canvas: Canvas,
        //        canvasScaler: CanvasScaler,
        //        cam: Camera,
        //        name: nameof(Options_Text),
        //        text: _options,
        //        size: new Vector2(3, 1),
        //        pos: new Vector2(Helper.PerWidth(3, Camera), -1.6f),
        //        alignment: TextAlignmentOptions.Right,
        //        fontScale: 1f);

        //private TextMeshProUGUI howTo_Text;
        //public TextMeshProUGUI HowTo_Text => howTo_Text != null ? howTo_Text :
        //    howTo_Text = howTo_Text.SetUpText(
        //        canvas: Canvas,
        //        canvasScaler: CanvasScaler,
        //        cam: Camera,
        //        name: nameof(HowTo_Text),
        //        text: _howTo,
        //        size: new Vector2(3, 1),
        //        pos: new Vector2(Helper.PerWidth(3, Camera), -2.4f),
        //        alignment: TextAlignmentOptions.Right,
        //        fontScale: 1f);

        //private TextMeshProUGUI quit_Text;
        //public TextMeshProUGUI Quit_Text => quit_Text != null ? quit_Text :
        //    quit_Text = quit_Text.SetUpText(
        //        canvas: Canvas,
        //        canvasScaler: CanvasScaler,
        //        cam: Camera,
        //        name: nameof(Quit_Text),
        //        text: _quit,
        //        size: new Vector2(3, 1),
        //        pos: new Vector2(Helper.PerWidth(3, Camera), -4f),
        //        alignment: TextAlignmentOptions.Right,
        //        fontScale: 1f);

        #endregion MENU OBJECTS

        #region LIGHTHOUSES

        public float LightRotY = -40;

        private GameObject lightHouse; public GameObject LightHouse
        {
            get
            {
                return lightHouse != null ? lightHouse : (lightHouse = SetUpLightHouse());

                GameObject SetUpLightHouse()
                {
                    GameObject lh = new GameObject(nameof(LightHouse));
                    lh.transform.SetParent(Parent.transform);
                    lh.transform.position = new Vector3(0, 10, -8);
                    return lh;
                }
            }
        }

        private Light lightA; public Light LightA
        {
            get
            {
                return lightA != null ? lightA : lightA = SetupLight();

                Light SetupLight()
                {
                    Light light = new GameObject(nameof(LightA)).AddComponent<Light>();
                    light.lightmapBakeType = LightmapBakeType.Baked;
                    light.transform.SetParent(LightHouse.transform);
                    light.transform.SetPositionAndRotation(
                        LightHouse.transform.position,
                        Quaternion.Euler(new Vector3(50, 180, 0)));
                    light.type = LightType.Spot;
                    light.range = 40;
                    light.spotAngle = 45;
                    light.intensity = 5;
                    light.shadows = LightShadows.Soft;
                    light.color = new Color(.9f, .85f, .8f);
                    return light;
                }
            }
        }

        private Light lightB; public Light LightB
        {
            get
            {
                return lightB != null ? lightB : lightB = SetupLight();

                Light SetupLight()
                {
                    Light light = new GameObject(nameof(LightB)).AddComponent<Light>();
                    light.lightmapBakeType = LightmapBakeType.Baked;
                    light.transform.SetParent(LightHouse.transform);
                    light.transform.SetPositionAndRotation(
                        LightHouse.transform.position,
                        Quaternion.Euler(new Vector3(50, 0, 0)));
                    light.type = LightType.Spot;
                    light.range = 40;
                    light.spotAngle = 45;
                    light.intensity = 5;
                    light.shadows = LightShadows.Soft;
                    light.color = new Color(.85f, .75f, .7f);
                    return light;
                }
            }
        }

        private GameObject catBoat; public GameObject CatBoat
        {
            get
            {
                return catBoat != null ? catBoat : catBoat = SetUpCatBoat();

                GameObject SetUpCatBoat()
                {
                    //GameObject go = Object.Instantiate(Assets.Catboat);
                    GameObject go = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                    go.transform.SetParent(Parent.transform);
                    go.transform.position = new Vector3(-2, -1.5f, 0);
                    go.transform.localScale = Vector3.one * 3;
                    //go.AddComponent<RockTheBoat>();
                    return go;
                }
            }
        }

        #endregion LIGHTHOUSES
    }

    public enum MainMenuItem { Continue, Load }//, New, Options, HowToPlay, Quit }
}


//private TextMeshProUGUI loadGame_Text;
//public TextMeshProUGUI LoadGame_Text => loadGame_Text != null ? loadGame_Text :
//    loadGame_Text = loadGame_Text.SetUpText(
//        canvas: Canvas,
//        canvasScaler: CanvasScaler,
//        cam: Camera,
//        name: nameof(LoadGame_Text),
//        text: _load,
//        size: new Vector2(3, 1f),
//        pos: new Vector2(Helper.PerWidth(3, Camera), 0),
//        alignment: TextAlignmentOptions.Right,
//        fontScale: 1f);
//private TextMeshProUGUI continue_Text;
//public TextMeshProUGUI Continue_Text => continue_Text != null ? continue_Text :
//     continue_Text = continue_Text.SetUpText(
//        canvas: Canvas,
//        canvasScaler: CanvasScaler,
//        cam: Camera,
//        name: nameof(Continue_Text),
//        text: _continue,
//        size: new Vector2(3, 1),
//        pos: new Vector2(Helper.PerWidth(3, Camera), 0.8f),
//        alignment: TextAlignmentOptions.Right,
//        fontScale: 1f);