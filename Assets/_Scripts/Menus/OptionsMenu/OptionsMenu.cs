namespace Menus.OptionsMenu
{
    public class OptionsMenu : Menu<OptionsMenu.OptionsItem, OptionsMenu>
    {
        private BackButton _back;

        public OptionsMenu() : base(nameof(OptionsMenu))
        {
        }

        public override MenuLayoutStyle Style => MenuLayoutStyle.Header;
        public BackButton Back => _back ??= new BackButton(Parent);

        public override Menu<OptionsItem, OptionsMenu> Initialize(OptionsItem selection)
        {
            _ = Back;
            return base.Initialize(selection);
        }

        public class OptionsItem : DataEnum
        {
            public static readonly OptionsItem Volume = new(0, "VOLUME");
            public static readonly OptionsItem GamePlay = new(1, "GAME PLAY");

            public OptionsItem() : base(0, "")
            {
            }

            private OptionsItem(int id, string name) : base(id, name)
            {
            }
            // public static OptionsItem Controls = new(2, "CONTROLS");
        }
    }
}