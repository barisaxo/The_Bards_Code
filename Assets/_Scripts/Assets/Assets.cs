using UnityEngine;

public static class Assets
{

    public static Material Video_Mat => Resources.Load<Material>("Materials/Video_Mat");

    public static Sprite White => Resources.Load<Sprite>("Sprites/Misc/White");
    public static Material Stars => Resources.Load<Material>("Skyboxes/Stars");

    public static Sprite EastButton => Resources.Load<Sprite>("Sprites/Gamepad_Button/East_Button");
    public static Sprite NorthButton => Resources.Load<Sprite>("Sprites/Gamepad_Button/North_Button");
    public static Sprite SouthButton => Resources.Load<Sprite>("Sprites/Gamepad_Button/South_Button");
    public static Sprite WestButton => Resources.Load<Sprite>("Sprites/Gamepad_Button/West_Button");

    public static GameObject BigBoat => Resources.Load<GameObject>("Prefabs/BigBoat");

    public static GameObject CatBoat => Resources.Load<GameObject>("Prefabs/CatBoat");

}
