using Menus.Main;
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

    protected override void DirectionPressed(Dir dir)
    {
        MainMenu.ScrollMenuOptions(dir);
    }

    protected override void ConfirmPressed()
    {
        switch (MainMenu.CurrItem)
        {
            case MainMenuItem.Continue:
                //FadeToState(new Aether.AetherExploreState());
                break;
            case MainMenuItem.Load:
                //SetStateDirectly(new LoadSlotSelection_State());
                break;
                //case MainMenuItem.New:
                //    //SetStateDirectly(new SaveSlotSelection_State());
                //    break;
                //case MainMenuItem.Options:
                //    //SetStateDirectly(new OptionsMenu_State());
                //    break;
                //case MainMenuItem.HowToPlay:
                //    //SetStateDirectly(new HowToPlay_State());
                //    break;
                //case MainMenuItem.Quit:
                //    //SetStateDirectly(new QuitGameMenu_State());
                //    break;
        }
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
