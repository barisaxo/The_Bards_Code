using UnityEngine;

namespace Menus.MainMenu
{
    public class MainMenuScene
    {
        #region INSTANCE

        public MainMenuScene()
        {
            _ = LightHouse;
        }

        public void SelfDestruct()
        {
            GameObject.Destroy(_parent.gameObject);
            Resources.UnloadUnusedAssets();
        }

        private Transform _parent;
        public Transform Parent => _parent != null ? _parent : _parent = new GameObject(nameof(MainMenuScene)).transform;

        #endregion INSTANCE

        private GameObject lightHouse; public GameObject LightHouse
        {
            get
            {
                return lightHouse != null ? lightHouse : lightHouse = SetUpLightHouse();

                GameObject SetUpLightHouse()
                {
                    GameObject lh = new(nameof(LightHouse));
                    lh.transform.SetParent(Parent.transform);
                    lh.transform.position = new Vector3(0, 10, -8);

                    for (int i = 0; i < 2; i++)
                    {
                        Light light = new GameObject(nameof(Light) + i).AddComponent<Light>();
                        light.lightmapBakeType = LightmapBakeType.Baked;
                        light.transform.SetParent(lh.transform);
                        light.transform.SetPositionAndRotation(
                            lh.transform.position,
                            Quaternion.Euler(new Vector3(50, i * 180, 0)));
                        light.type = LightType.Spot;
                        light.range = 40;
                        light.spotAngle = 45;
                        light.intensity = 5;
                        light.shadows = LightShadows.Soft;
                        light.color = new Color(Random.Range(.85f, .95f),
                                                Random.Range(.5f, .6f),
                                                Random.Range(.05f, .15f));
                    }

                    return lh;
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
                    GameObject go = Object.Instantiate(Assets.CatBoat);
                    go.transform.SetParent(Parent.transform);
                    go.transform.position = new Vector3(-2, -1.5f, 0);
                    go.transform.localScale = Vector3.one * 3;
                    return go;
                }
            }
        }


    }
}