using System;
using UnityEngine;
using TMPro;

namespace Menus.MainMenu
{
    public static class MainMenu_Systems
    {
        static MainMenuItem ItemCount => (MainMenuItem)Enum.GetNames(typeof(MainMenuItem)).Length;

        public static void RotateLighthouse(this MainMenu mm)
        {
            mm.LightRotY += Time.deltaTime * 25;
            mm.LightHouse.transform.rotation = Quaternion.Euler(0, mm.LightRotY, 0);
        }

        public static void SetActiveItems(this MainMenu mm)
        {
            //MainMenu_Setup.Io.LoadGame_Text.gameObject.SetActive(SaveSystem.AnySavedGameExists());
            //MainMenu_Setup.Io.Continue_Text.gameObject.SetActive(SaveSystem.SavedGameExists());
        }

        public static void ScrollMenuOptions(this MainMenu mm, Dir dir)
        {
            switch (dir)
            {
                case Dir.Up: mm.CurrItem = PrevItem(); break;
                case Dir.Down: mm.CurrItem = NextItem(); break;
            }
            DontScrollDisabledOptions();
            mm.ColorTexts();

            void DontScrollDisabledOptions()
            {
                if (!mm.GetCardItem(mm.CurrItem).CardGO.activeInHierarchy)
                {
                    mm.ScrollMenuOptions(Dir.Down);
                    return;
                }
            }

            MainMenuItem? PrevItem() => mm.CurrItem == null ? null : mm.CurrItem <= 0 ? 0 : mm.CurrItem - 1;
            MainMenuItem? NextItem() => mm.CurrItem == null ? null : mm.CurrItem == ItemCount - 1 ? mm.CurrItem : mm.CurrItem + 1;
        }

        public static void ColorTexts(this MainMenu mm)
        {
            for (MainMenuItem i = 0; i < ItemCount; i++)
            {
                if (mm.GetCardItem(i).CardGO.activeInHierarchy)
                {
                    mm.GetCardItem(i).SetTextColor(mm.CurrItem == i ? Color.white : Color.gray);
                }
            }
        }

        private static Card GetCardItem(this MainMenu mm, MainMenuItem? item) => item switch
        {
            MainMenuItem.Continue => mm.Continue,
            MainMenuItem.LoadGame => mm.LoadGame,
            MainMenuItem.NewGame => mm.NewGame,
            MainMenuItem.Options => mm.Options,
            MainMenuItem.HowToPlay => mm.HowToPlay,
            MainMenuItem.Quit => mm.Quit,
            _ => null,
        };

    }
}