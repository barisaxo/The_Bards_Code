using System;
using UnityEngine;
using TMPro;

namespace Menus.Main
{
    public static class MainMenu_Systems
    {
        static MainMenuItem ItemCount => (MainMenuItem)Enum.GetNames(typeof(MainMenuItem)).Length;

        //public static void Engage(this MainMenu mm)
        //{
        //    _ = mm;

        //    SetActiveItems();
        //    mm.ColorTexts();
        //    mm.ScrollMenuOptions(Dir.Reset);
        //}

        //public static void Disengage(this MainMenu mm)
        //{
        //    mm.SelfDestruct();
        //}

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
                case Dir.Up: mm.CurrItem = PrevItem(); mm.ColorTexts(); break;
                case Dir.Down: mm.CurrItem = NextItem(); mm.ColorTexts(); break;
            }
            DontScrollDisabledOptions();
            mm.ColorTexts();

            void DontScrollDisabledOptions()
            {
                if (!mm.ItemText(mm.CurrItem).CardGO.activeInHierarchy)
                {
                    mm.ScrollMenuOptions(Dir.Down); return;
                }
            }

            MainMenuItem PrevItem() => mm.CurrItem == 0 ? 0 : mm.CurrItem - 1;
            MainMenuItem NextItem() => mm.CurrItem == ItemCount - 1 ? mm.CurrItem : mm.CurrItem + 1;
        }

        public static void ColorTexts(this MainMenu mm)
        {
            for (MainMenuItem i = 0; i < ItemCount; i++)
            {
                if (mm.ItemText(i).CardGO.activeInHierarchy)
                { mm.ItemText(i).SetTextColor(mm.CurrItem == i ? Color.white : Color.gray); }
            }
        }

        private static Card ItemText(this MainMenu mm, MainMenuItem item) => item switch
        {
            MainMenuItem.Continue => mm.Continue,
            MainMenuItem.Load => mm.LoadGame,
            //MainMenuItem.New => mm.NewGame_Text,
            //MainMenuItem.Options => mm.Options_Text,
            //MainMenuItem.HowToPlay => mm.HowTo_Text,
            //MainMenuItem.Quit => mm.Quit_Text,
            //_ => mm.Quit_Text,
            _ => mm.Continue,
        };

    }
}