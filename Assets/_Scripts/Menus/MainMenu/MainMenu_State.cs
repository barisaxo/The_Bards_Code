using Menus.MainMenu;
using UnityEngine;

public class MainMenu_State : State
{
    MainMenu MainMenu;

    protected override void EngageState()
    {
        MainMenu = new MainMenu();
        MonoHelper.OnUpdate += RotateLightHouse;
    }

    protected override void DisengageState()
    {
        MonoHelper.OnUpdate -= RotateLightHouse;
        MainMenu.SelfDestruct();
    }

    protected override void ClickedOn(GameObject go)
    {
        MainMenu.CurrItem = go switch
        {
            _ when go.transform.IsChildOf(MainMenu.NewGame.CardGO.transform) => MainMenuItem.NewGame,
            _ when go.transform.IsChildOf(MainMenu.Continue.CardGO.transform) => MainMenuItem.Continue,
            _ when go.transform.IsChildOf(MainMenu.LoadGame.CardGO.transform) => MainMenuItem.LoadGame,
            _ when go.transform.IsChildOf(MainMenu.Quit.CardGO.transform) => MainMenuItem.Quit,
            _ when go.transform.IsChildOf(MainMenu.Options.CardGO.transform) => MainMenuItem.Options,
            _ when go.transform.IsChildOf(MainMenu.HowToPlay.CardGO.transform) => MainMenuItem.HowToPlay,
            _ => null
        };

        ConfirmPressed();
    }

    protected override void DirectionPressed(Dir dir)
    {
        if (dir == Dir.Reset) return;
        MainMenu.ScrollMenuOptions(dir);
    }

    protected override void ConfirmPressed()
    {
        switch (MainMenu.CurrItem)
        {
            case MainMenuItem.Continue:
                //FadeToState(new Aether.AetherExploreState());
                break;
            case MainMenuItem.LoadGame:
                //SetStateDirectly(new LoadSlotSelection_State());
                break;
            case MainMenuItem.NewGame:
                //    SetStateDirectly(new SaveSlotSelection_State());
                break;
            case MainMenuItem.Options:
                //SetStateDirectly(new OptionsMenu_State());
                break;
            case MainMenuItem.HowToPlay:
                //SetStateDirectly(new HowToPlay_State());
                break;
            case MainMenuItem.Quit:
                //SetStateDirectly(new QuitGameMenu_State());
                break;
        }

        MainMenu.ColorTexts();
    }

    protected override void LStickInput(Vector2 v2)
    {
        MainMenu.CatBoat.transform.Rotate(new Vector3(0, v2.x, 0), Space.World);
    }

    protected override void RStickInput(Vector2 v2)
    {
        MainMenu.CatBoat.transform.localScale = (Vector3.one * 3) + ((Vector3)v2 * 2);
    }

    protected override void StartPressed() { }
    //protected override void SelectPressed() { }

    void RotateLightHouse()
    {
        MainMenu.LightRotY += Time.deltaTime * 25;
        MainMenu.LightHouse.transform.rotation = Quaternion.Euler(0, MainMenu.LightRotY, 0);
    }
}
