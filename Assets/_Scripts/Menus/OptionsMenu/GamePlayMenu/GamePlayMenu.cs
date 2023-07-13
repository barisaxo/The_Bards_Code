using UnityEngine;

namespace Menus.OptionsMenu
{
    public class GamePlayMenu : Menu<GamePlayMenu.MenuItem>
    {
        #region INSTANCE
        public GamePlayMenu() : base(nameof(GamePlayMenu)) { }

        public GamePlayMenu Initialize()
        {
            Selection = MenuItems[0];
            this.ColorTexts();
            this.ScrollMenuOptions(Dir.Reset);
            return this;
        }

        public void SelfDestruct()
        {
            GameObject.Destroy(_parent.gameObject);
            Resources.UnloadUnusedAssets();
        }

        #endregion INSTANCE


        #region MENU OBJECTS

        public override MenuLayoutStyle Style => MenuLayoutStyle.AlignLeft;

        public class MenuItem : Enumeration
        {
            public MenuItem() : base(0, "") { }
            public MenuItem(int id, string name) : base(id, name) { }
            public static MenuItem Transpose = new(0, "TRANSPOSE");
            public static MenuItem Tuning = new(1, "TUNING");
        }

        #endregion MENU OBJECTS
    }

}
