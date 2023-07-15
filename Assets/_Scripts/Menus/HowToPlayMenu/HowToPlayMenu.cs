using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Menus;
namespace Menus.HowToPlayMenu
{
    public class HowToPlayMenu : Menu<HowToPlayMenu.HowToPlayItem, HowToPlayMenu>
    {
        public HowToPlayMenu() : base(nameof(HowToPlayMenu)) { }

        //public HowToPlayMenu Initialize()
        //{
        //    return this;
        //}

        public class HowToPlayItem : DataEnum
        {
            public HowToPlayItem() : base(0, "") { }
            public HowToPlayItem(int id, string name) : base(id, name) { }
            public static HowToPlayItem Volume = new(0, "VOLUME");
            public static HowToPlayItem GamePlay = new(1, "GAME PLAY");
            // public static HowToPlayItem Controls = new(2, "CONTROLS");
        }
    }
}