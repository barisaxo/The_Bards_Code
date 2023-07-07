using Menus.MainMenu;
using UnityEngine;

public class MainMenu_State : State
{
    static MainMenu MainMenu;
    bool IsActive = true;

    protected override void EngageState()
    {
        MainMenu = new MainMenu();
        RotateLightHouse();
    }

    protected override void DisengageState()
    {
        IsActive = false;
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

    protected override void StartPressed() { IsActive = !IsActive; RotateLightHouse(); }
    //protected override void SelectPressed() { }

    async void RotateLightHouse()
    {
        while (IsActive)
        {
            await System.Threading.Tasks.Task.Yield();
            if (!Application.isPlaying) return;

            MainMenu.LightRotY += Time.deltaTime * 25;
            MainMenu.LightHouse.transform.rotation = Quaternion.Euler(0, MainMenu.LightRotY, 0);
        }
    }
}
