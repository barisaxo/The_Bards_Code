using UnityEngine;
using System.Collections.Generic;

namespace Menus.OptionsMenu
{
    public class VolumeMenu : Menu<VolumeData.DataItem>
    {
        #region INSTANCE

        public VolumeMenu() : base(nameof(VolumeMenu)) { }

        public VolumeMenu Initialize(VolumeData data)
        {
            Selection = MenuItems[0];
            this.UpdateAllItems(data);
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

        public override MenuLayoutStyle Style => MenuLayoutStyle.TwoCollumns;

        //public class MenuItem : Enumeration
        //{
        //    public MenuItem() : base(0, "") { }
        //    public MenuItem(int id, string name) : base(id, name) { }
        //    public static MenuItem BGMusic = new(0, "BG MUSIC");
        //    public static MenuItem SoundFX = new(1, "SOUND FX");
        //    public static MenuItem Chords = new(2, "CHORDS");
        //    public static MenuItem Bass = new(3, "BASS");
        //    public static MenuItem Drums = new(4, "DRUMS");
        //    public static MenuItem Battery = new(5, "BATTERY");
        //    public static MenuItem Click = new(6, "CLICK");
        //}

        #endregion MENU OBJECTS
    }

}
