﻿
namespace Menus.OptionsMenu
{
    public class GamePlayMenu : Menu<GameplayData.DataItem, GamePlayMenu>
    {
        public GamePlayMenu() : base(nameof(GamePlayMenu)) { }

        public Menu<GameplayData.DataItem, GamePlayMenu> Initialize(GameplayData data)
        {
            this.UpdateAllItems(data);
            return base.Initialize();
        }

        public override MenuLayoutStyle Style => MenuLayoutStyle.AlignLeft;
    }

    public static class GamePlayMenuSystems
    {
        public static string DisplayData(this GameplayData.DataItem item, GameplayData data)
        {
            UnityEngine.Debug.Log(data.GetData(item));
            return item.Name + ": " + data.GetData(item);
        }

        public static void UpdateAllItems(this GamePlayMenu menu, GameplayData data)
        {
            foreach (var item in menu.MenuItems)
            {
                item.Card.SetTextString(item.Item.DisplayData(data));
            }
        }
    }
}
