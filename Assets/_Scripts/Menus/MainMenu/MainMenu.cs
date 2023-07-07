using UnityEngine;
using TMPro;

namespace Menus.Main
{
    public class MainMenu
    {
        #region INSTANCE

        public MainMenu()
        {
            _ = Parent;

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

        public void SelfDestruct()
        {
            Object.Destroy(_parent.gameObject);
            Resources.UnloadUnusedAssets();
        }

        private Transform _parent;
        public Transform Parent => _parent != null ? _parent : _parent = new GameObject(nameof(MainMenu)).transform;

        #endregion INSTANCE

        #region STRINGs

        public const string _continueString = "CONTINUE";
        public const string _loadString = "LOAD GAME";
        public const string _newGameString = "NEW GAME";
        public const string _optionsString = "OPTIONS";
        public const string _howToString = "HOW TO PLAY";
        public const string _quitString = "QUIT";

        #endregion STRINGs

        #region MENU OBJECTS

        public MainMenuItem? CurrItem = 0;

        private Card _continue;
        public Card Continue => _continue ??= new Card(nameof(Continue), Parent)
            .SetTextString(_continueString)
            .SetTMPSize(new Vector2(4, 1))
            .SetTextAlignment(TextAlignmentOptions.Right)
            .WordWrap(false)
            .SetTMPPosition(new Vector2(Cam.Io.OrthoX() - 2.5f, .8f))
            .TMPClickable()
            .SetFontScale(1);

        private Card _loadGame;
        public Card LoadGame => _loadGame ??= new Card(nameof(LoadGame), Parent)
            .SetTextString(_loadString)
            .SetTMPSize(new Vector2(4, 1))
            .SetTMPPosition(new Vector2(Cam.Io.OrthoX() - 2.5f, 0.01f))
            .SetTextAlignment(TextAlignmentOptions.Right)
            .WordWrap(false)
            .TMPClickable()
            .SetFontScale(1);


        private Card _newGame;
        public Card NewGame => _newGame ??= new Card(nameof(NewGame), Parent)
            .SetTextString(_newGameString)
            .SetTMPSize(new Vector2(4, 1))
            .SetTMPPosition(new Vector2(Cam.Io.OrthoX() - 2.5f, -.8f))
            .SetTextAlignment(TextAlignmentOptions.Right)
            .WordWrap(false)
            .TMPClickable()
            .SetFontScale(1);

        private Card _options;
        public Card Options => _options ??= new Card(nameof(Options), Parent)
            .SetTextString(_optionsString)
            .SetTMPSize(new Vector2(4, 1))
            .SetTMPPosition(new Vector2(Cam.Io.OrthoX() - 2.5f, -1.6f))
            .SetTextAlignment(TextAlignmentOptions.Right)
            .WordWrap(false)
            .TMPClickable()
            .SetFontScale(1);

        private Card _howToPlay;
        public Card HowToPlay => _howToPlay ??= new Card(nameof(HowToPlay), Parent)
            .SetTextString(_howToString)
            .SetTMPSize(new Vector2(4, 1))
            .SetTMPPosition(new Vector2(Cam.Io.OrthoX() - 2.5f, -2.4f))
            .SetTextAlignment(TextAlignmentOptions.Right)
            .WordWrap(false)
            .TMPClickable()
            .SetFontScale(1);

        private Card _quit;
        public Card Quit => _quit ??= new Card(nameof(Quit), Parent)
            .SetTextString(_quitString)
            .SetTMPSize(new Vector2(4, 1))
            .SetTMPPosition(new Vector2(Cam.Io.OrthoX() - 2.5f, -4f))
            .SetTextAlignment(TextAlignmentOptions.Right)
            .WordWrap(false)
            .TMPClickable()
            .SetFontScale(1);

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

    public enum MainMenuItem { Continue, LoadGame, NewGame, Options, HowToPlay, Quit }


}