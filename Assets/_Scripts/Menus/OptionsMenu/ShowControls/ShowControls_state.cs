using System;
using UnityEngine;
using Menus.OptionsMenu;
using Menus;

public class ShowControls_State : State
{
    public ShowControls_State(State restoreState)
    {
        RestoreState = restoreState;
    }

    readonly State RestoreState;
    OptionsMenu Options;


    protected override void PrepareState(Action callback)
    {
        // Options = new OptionsMenu().Initialize(OptionsMenu.OptionsItem.Controls);

        callback();
    }

    protected override void DisengageState()
    {
        Options.SelfDestruct();
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
        else if (Options.Selection == Options.MenuItems[OptionsMenu.OptionsItem.GamePlay])
        {
            SetStateDirectly(new GamePlayMenu_State(RestoreState));
        }
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
