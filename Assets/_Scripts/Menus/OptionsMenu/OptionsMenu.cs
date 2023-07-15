using UnityEngine;

namespace Menus.OptionsMenu
{
    public class OptionsMenu : Menu<OptionsMenu.OptionsItem, OptionsMenu>
    {
        #region INSTANCE

        public OptionsMenu() : base(nameof(OptionsMenu)) { }

        public override Menu<OptionsItem, OptionsMenu> Initialize(OptionsItem selection)
        {
            _ = Back;
            return base.Initialize(selection);
        }

        #endregion INSTANCE


        #region MENU OBJECTS
        public override MenuLayoutStyle Style => MenuLayoutStyle.Header;

        Card _back; public Card Back => _back ??= new Card(nameof(Back), Parent)
            //.SpriteClickable()
            .TMPClickable()
            .SpriteClickable()
            .SetTextString("Back")
            .SetGOSize(Vector2.one * .6f)
            .SetTMPSize(new Vector2(1f, 1f))
            .SetPositionAll(new Vector2(Cam.Io.OrthoX() - .5f, -Cam.Io.OrthoY()))
            .OffsetTMPPositionBy(Vector2.right * .85f)
            .SetSprite(Assets.SouthButton)
            .SetTextColor(new Color(1, 1, 1, .65f))
            .AutoSizeFont(true)
            .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
            .SetFontScale(.5f, .5f);



        public class OptionsItem : DataEnum
        {
            public OptionsItem() : base(0, "") { }
            public OptionsItem(int id, string name) : base(id, name) { }
            public static OptionsItem Volume = new(0, "VOLUME");
            public static OptionsItem GamePlay = new(1, "GAME PLAY");
            // public static OptionsItem Controls = new(2, "CONTROLS");
        }

        #endregion MENU OBJECTS

    }

}
