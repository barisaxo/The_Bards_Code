using UnityEngine;

namespace Menus.OptionsMenu
{
    public class OptionsMenu : Menu<OptionsMenu.OptionsItem>
    {
        #region INSTANCE

        public OptionsMenu() : base(nameof(OptionsMenu)) { }

        public OptionsMenu Initialize(OptionsItem selection)
        {
            _ = Back;
            Selection = MenuItems[selection];
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
        public override MenuLayoutStyle Style => MenuLayoutStyle.Header;

        Card _back; public Card Back => _back ??= new Card(nameof(Back), Parent)
            //.SpriteClickable()
            .TMPClickable()
            .SetTextString("Back")
            .SetGOSize(Vector2.one * .6f)
            .SetTMPSize(new Vector2(1f, 1f))
            .SetPositionAll(new Vector2(Cam.Io.OrthoX() - .5f, -Cam.Io.OrthoY()))
            .OffsetTMPPositionBy(Vector2.right * .85f)
            .SetSprite(Assets.SouthButton)
            .SetTextColor(new Color(1, 1, 1, .65f))
            .AutoSizeFont(true)
            .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
            .SetFontScale(.65f);



        public class OptionsItem : Enumeration
        {
            public OptionsItem() : base(0, "") { }
            public OptionsItem(int id, string name) : base(id, name) { }
            public static OptionsItem Volume = new(0, "VOLUME");
            public static OptionsItem GamePlay = new(1, "GAME PLAY");
            public static OptionsItem Controls = new(2, "CONTROLS");
        }

        #endregion MENU OBJECTS

    }

}
