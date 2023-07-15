using UnityEngine;

namespace Menus.MainMenu
{
    public class MainMenu : Menu<MainMenu.MainMenuItem, MainMenu>
    {
        public MainMenu() : base(nameof(MainMenu)) { }

        public override Menu<MainMenuItem, MainMenu> Initialize()
        {
            _ = Scene;
            return base.Initialize();
        }

        public override void SelfDestruct()
        {
            Scene.SelfDestruct();
            base.SelfDestruct();
        }

        private MainMenuScene _scene;
        public MainMenuScene Scene => _scene ??= new();

        public override MenuLayoutStyle Style => MenuLayoutStyle.AlignRight;

        public class MainMenuItem : DataEnum
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

    }
}