using UnityEngine;
using System.Collections.Generic;
using TMPro;

namespace Menus
{
    public static class Menu_Systems
    {
        public static void ScrollMenuOptions<T>(this IMenu<T> menu, Dir dir) where T : Enumeration, new()
        {
            switch (menu.Style)
            {
                case MenuLayoutStyle.Header:
                    switch (dir)
                    {
                        case Dir.Left: menu.Selection = PrevItem(); break;
                        case Dir.Right: menu.Selection = NextItem(); break;
                    }; break;

                case MenuLayoutStyle.TwoCollumns:
                    switch (dir)
                    {
                        case Dir.Up: menu.Selection = PrevItem(); break;
                        case Dir.Down: menu.Selection = NextItem(); break;
                        case Dir.Left: if (menu.Style == MenuLayoutStyle.TwoCollumns) menu.Selection = ScrollLeft(); break;
                        case Dir.Right: if (menu.Style == MenuLayoutStyle.TwoCollumns) menu.Selection = ScrollRight(); break;
                    }; break;

                default:
                    switch (dir)
                    {
                        case Dir.Up: menu.Selection = PrevItem(); break;
                        case Dir.Down: menu.Selection = NextItem(); break;
                    }; break;
            }

            menu.ColorTexts();

            MenuItem<T> PrevItem() => menu.Style switch
            {
                MenuLayoutStyle.TwoCollumns => (
                    menu.Selection == Mathf.CeilToInt((menu.MenuItems.Count - .5f) * .5f) ||
                    menu.Selection <= 0) ?
                        menu.Selection : menu.MenuItems[menu.Selection - 1],

                _ => menu.Selection <= 0 ? menu.Selection : menu.MenuItems[menu.Selection - 1]
            };

            MenuItem<T> NextItem() => menu.Style switch
            {
                MenuLayoutStyle.TwoCollumns => (
                 menu.Selection == Mathf.FloorToInt((menu.MenuItems.Count - .5f) * .5f) ||
                 menu.Selection == menu.MenuItems[^1]) ?
                    menu.Selection : menu.MenuItems[menu.Selection + 1],

                _ => menu.Selection == menu.MenuItems[^1] ? menu.Selection : menu.MenuItems[menu.Selection + 1]
            };

            MenuItem<T> ScrollRight() => menu.Selection + Mathf.CeilToInt((menu.MenuItems.Count - .5f) * .5f) < menu.MenuItems.Count ?
                menu.MenuItems[menu.Selection + Mathf.CeilToInt((menu.MenuItems.Count - .5f) * .5f)] : menu.Selection;

            MenuItem<T> ScrollLeft() => menu.Selection - Mathf.CeilToInt((menu.MenuItems.Count - .5f) * .5f) >= 0 ?
                menu.MenuItems[menu.Selection - Mathf.CeilToInt((menu.MenuItems.Count - .5f) * .5f)] : menu.Selection;
        }

        public static void ColorTexts<T>(this IMenu<T> menu) where T : Enumeration, new()
        {
            for (int i = 0; i < menu.MenuItems.Count; i++)
            {
                if (menu.MenuItems[i].Card.GO.activeInHierarchy)
                {
                    menu.MenuItems[i].Card.SetTextColor(menu.Selection == i ? Color.white : Color.gray);
                }
            }
        }

        public static List<MenuItem<T>> SetUpMenuCards<T>(this Menu<T> menu, Transform parent, MenuLayoutStyle style, List<T> dataItems) where T : Enumeration, new()
        {
            List<MenuItem<T>> items = new();
            for (int i = 0; i < dataItems.Count; i++)
            {
                items.Add(new()
                {
                    Item = dataItems[i],
                    Card = new Card(dataItems[i].Name, parent)
                       .SetTextString(dataItems[i].Name)
                       .SetTMPSize(new Vector2(4, 1))
                       .SetTMPPosition(GetPosition(i))
                       .AutoSizeFont(true)
                       .SetTextAlignment(style switch
                       {
                           MenuLayoutStyle.AlignRight => TextAlignmentOptions.Right,
                           MenuLayoutStyle.Header => TextAlignmentOptions.Center,
                           _ => TextAlignmentOptions.Left
                       })
                       .WordWrap(false)
                       .TMPClickable()
                       .SetFontScale(.65f)
                });
            }

            return items;

            Vector2 GetPosition(int i) => style switch
            {
                MenuLayoutStyle.AlignRight =>
                        new Vector2(Cam.Io.OrthoX() - 2.5f, 1.8f - (i * .8f)),

                MenuLayoutStyle.AlignLeft =>
                       new Vector2(-Cam.Io.OrthoX() + 2.5f, 1.8f - (i * .8f)),

                MenuLayoutStyle.TwoCollumns =>
                new Vector2(i < dataItems.Count * .5f ? -Cam.Io.OrthoX() + 2.5f : 2,
                -1.8f - (i % Mathf.CeilToInt(dataItems.Count * .5f) * .8f) + (dataItems.Count * .5f)),

                MenuLayoutStyle.Header => new Vector2(2 - Cam.Io.OrthoX() + (2 * (Cam.Io.OrthoX() - 2) / (dataItems.Count - 1) * i),
                Cam.Io.OrthoY() - 1),

                _ => Vector2.zero,
            };
        }




    }

    public enum MenuLayoutStyle { AlignRight, TwoCollumns, AlignLeft, Header }
}

public static class ListHelper
{
    public static object Last<T>(this List<T> values) => values[^1];

}