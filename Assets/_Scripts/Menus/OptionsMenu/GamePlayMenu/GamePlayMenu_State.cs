using System;
using UnityEngine;
using Menus.OptionsMenu;
using Menus;

public class GamePlayMenu_State : State
{
    public GamePlayMenu_State(State restoreState)
    {
        RestoreState = restoreState;
    }

    readonly State RestoreState;
    OptionsMenu Options;
    GamePlayMenu GamePlayMenu;

    protected override void PrepareState(Action callback)
    {
        Options = (OptionsMenu)new OptionsMenu().Initialize(OptionsMenu.OptionsItem.GamePlay);
        GamePlayMenu = (GamePlayMenu)new GamePlayMenu().Initialize(Data.GamePlay);
        callback();
    }

    protected override void DisengageState()
    {
        //LoadSaveSystems.SaveCurrentGame();
        Options.SelfDestruct();
        GamePlayMenu.SelfDestruct();
    }

    protected override void ClickedOn(GameObject go)
    {
        if (go.transform.IsChildOf(Options.Back.GO.transform))
        {
            SetStateDirectly(RestoreState);
            return;
        }

        for (int i = 0; i < Options.MenuItems.Count; i++)
        {
            if (go.transform.IsChildOf(Options.MenuItems[i].Card.GO.transform))
            {
                if (Options.MenuItems[i] == Options.Selection) { return; }
                Options.Selection = Options.MenuItems[i];
                UpdateMenu();
                return;
            }
        }

        for (int i = 0; i < GamePlayMenu.MenuItems.Count; i++)
        {
            if (go.transform.IsChildOf(GamePlayMenu.MenuItems[i].Card.GO.transform))
            {
                if (GamePlayMenu.Selection == GamePlayMenu.MenuItems[i])
                {
                    IncreaseItem(GamePlayMenu.Selection);
                    return;
                }
                else
                {
                    GamePlayMenu.Selection = GamePlayMenu.MenuItems[i];
                    GamePlayMenu.UpdateTextColors();
                    return;
                }
            }
        }
    }

    protected override void DirectionPressed(Dir dir)
    {
        GamePlayMenu.ScrollMenuOptions(dir);
    }

    protected override void L1Pressed()
    {
        Options.ScrollMenuOptions(Dir.Left);
        UpdateMenu();
    }
    protected override void R1Pressed()
    {
        Options.ScrollMenuOptions(Dir.Right);
        UpdateMenu();
    }

    private void UpdateMenu()
    {
        if (Options.Selection == Options.MenuItems[OptionsMenu.OptionsItem.Volume])
        {
            SetStateDirectly(new VolumeMenu_State(RestoreState));
        }
        // else if (Options.Selection == Options.MenuItems[OptionsMenu.OptionsItem.Controls])
        // {
        //     SetStateDirectly(new ShowControls_State(RestoreState));
        // }
    }

    protected override void ConfirmPressed()
    {
        IncreaseItem(GamePlayMenu.Selection);
    }

    void IncreaseItem(MenuItem<GameplayData.DataItem> item)
    {
        Data.GamePlay.IncreaseItem(item.Item);
        item.Card.SetTextString(item.Item.DisplayData(Data.GamePlay));
    }

    protected override void CancelPressed()
    {
        SetStateDirectly(RestoreState);
    }

    protected override void StartPressed()
    {
        SetStateDirectly(RestoreState);
    }

}
