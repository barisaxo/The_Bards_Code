using UnityEngine;

namespace Menus.MainMenu
{
    public class MainMenu : Menu<MainMenu.MainMenuItem>
    {
        #region INSTANCE

        public MainMenu() : base(nameof(MainMenu)) { }

        public MainMenu Initialize()
        {
            _ = Scene;
            Selection = MenuItems[0];
            //this.SetUpMenuCards(Parent, Style);
            this.ColorTexts();
            this.ScrollMenuOptions(Dir.Reset);
            return this;
        }

        public void SelfDestruct()
        {
            Scene.SelfDestruct();
            GameObject.Destroy(_parent.gameObject);
            Resources.UnloadUnusedAssets();
        }

        private MainMenuScene _scene;
        public MainMenuScene Scene => _scene ??= new();

        #endregion INSTANCE

        #region MENU OBJECTS

        public override MenuLayoutStyle Style => MenuLayoutStyle.AlignRight;

        public class MainMenuItem : Enumeration
        {
            public MainMenuItem() : base(0, "") { }
            public MainMenuItem(int id, string name) : base(id, name) { }
            public static MainMenuItem Continue = new(0, "Continue");
            public static MainMenuItem LoadGame = new(1, "Load Game");
            public static MainMenuItem NewGame = new(2, "New Game");
            public static MainMenuItem Options = new(3, "Options");
            public static MainMenuItem HowToPlay = new(4, "How To Play");
            public static MainMenuItem Quit = new(5, "Quit");
        }

        #endregion MENU OBJECTS
    }
}