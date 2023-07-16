using System;
using Menus;
using Menus.OptionsMenu;
using UnityEngine;

public class VolumeMenu_State : State
{
    private readonly State ConsequentState;
    private OptionsMenu Options;
    private VolumeMenu VolumeMenu;

    public VolumeMenu_State(State consequentState)
    {
        ConsequentState = consequentState;
    }

    protected override void PrepareState(Action callback)
    {
        Options = (OptionsMenu)new OptionsMenu().Initialize(OptionsMenu.OptionsItem.Volume);
        VolumeMenu = (VolumeMenu)new VolumeMenu().Initialize(Data.Volume);
        callback();
    }

    protected override void DisengageState()
    {
        //LoadSaveSystems.SaveCurrentGame();
        Options.SelfDestruct();
        VolumeMenu.SelfDestruct();
    }

    protected override void ClickedOn(GameObject go)
    {
        if (go.transform.IsChildOf(Options.Back.Button.GO.transform))
        {
            SetStateDirectly(ConsequentState);
            return;
        }

        for (var i = 0; i < Options.MenuItems.Count; i++)
            if (go.transform.IsChildOf(Options.MenuItems[i].Card.GO.transform))
            {
                if (i == Options.Selection) return;
                Options.Selection = Options.MenuItems[i];
                UpdateMenu();
                return;
            }

        for (var i = 0; i < VolumeMenu.MenuItems.Count; i++)
            if (go.transform.IsChildOf(VolumeMenu.MenuItems[i].Card.GO.transform))
            {
                if (VolumeMenu.Selection.Item == i)
                {
                    IncreaseItem(VolumeMenu.MenuItems[i]);
                    return;
                }

                VolumeMenu.Selection = VolumeMenu.MenuItems[i];
                VolumeMenu.UpdateTextColors();
                return;
            }
    }

    protected override void DirectionPressed(Dir dir)
    {
        if (dir == Dir.Reset) return;
        VolumeMenu.ScrollMenuItems(dir);
    }

    protected override void L1Pressed()
    {
        Options.ScrollMenuItems(Dir.Left);
        UpdateMenu();
    }

    protected override void R1Pressed()
    {
        Options.ScrollMenuItems(Dir.Right);
        UpdateMenu();
    }

    protected override void ConfirmPressed()
    {
        IncreaseItem(VolumeMenu.MenuItems[VolumeMenu.Selection]);
    }

    protected override void CancelPressed()
    {
        SetStateDirectly(ConsequentState);
    }

    private void IncreaseItem(MenuItem<VolumeData.DataItem> item)
    {
        Data.Volume.IncreaseLevel(item.Item);
        item.Card.SetTextString(item.Item.DisplayData(Data.Volume));
    }

    private void UpdateMenu()
    {
        // if (Options.Selection == OptionsMenu.OptionsItem.Controls)
        // {
        //     SetStateDirectly(new ShowControls_State(ConsequentState));
        // }
        // else
        if (Options.Selection == OptionsMenu.OptionsItem.GamePlay)
            SetStateDirectly(new GamePlayMenu_State(ConsequentState));
    }
}