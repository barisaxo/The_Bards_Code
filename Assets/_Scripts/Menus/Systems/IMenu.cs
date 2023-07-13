using System.Collections.Generic;

namespace Menus
{
    public interface IMenu<T> where T : Enumeration, new()
    {
        public MenuItem<T> Selection { get; set; }
        //public List<T> DataItems { get; }
        public List<MenuItem<T>> MenuItems { get; }
        public MenuLayoutStyle Style { get; }
    }

}