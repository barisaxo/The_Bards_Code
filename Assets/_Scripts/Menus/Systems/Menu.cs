using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;


namespace Menus
{
    public abstract class Menu<T> : IMenu<T> where T : Enumeration, new()
    {
        public Menu(string name) { Name = name; }
        private readonly string Name;
        protected Transform _parent;
        public Transform Parent => _parent != null ? _parent : _parent = new GameObject(Name).transform;
        public MenuItem<T> Selection { get; set; }
        public List<T> DataItems => Enumeration.List<T>();
        private List<MenuItem<T>> _menuItems;
        public List<MenuItem<T>> MenuItems => _menuItems ??= this.SetUpMenuCards(Parent, Style, DataItems);
        public virtual MenuLayoutStyle Style => MenuLayoutStyle.AlignRight;
    }

    public struct MenuItem<T> where T : Enumeration, new()
    {
        public T Item;
        public Card Card;

        public static int operator +(MenuItem<T> a, int b) => a.Item.Id + b;
        public static int operator -(MenuItem<T> a, int b) => a.Item.Id - b;
        public static int operator +(MenuItem<T> a, MenuItem<T> b) => a.Item.Id + b.Item.Id;
        public static int operator -(MenuItem<T> a, MenuItem<T> b) => a.Item.Id - b.Item.Id;
        //public static int operator +(MenuItem<T> a, Enumeration b) => a.Item.Id + b.Id;
        //public static int operator -(MenuItem<T> a, Enumeration b) => a.Item.Id - b.Id;

        public static bool operator ==(MenuItem<T> a, int b) => a.Item.Id == b;
        public static bool operator !=(MenuItem<T> a, int b) => a.Item.Id != b;
        public static bool operator ==(MenuItem<T> a, MenuItem<T> b) => a.Item.Id == b.Item.Id;
        public static bool operator !=(MenuItem<T> a, MenuItem<T> b) => a.Item.Id != b.Item.Id;
        //public static bool operator ==(MenuItem<T> a, Enumeration b) => a.Item.Id == b.Id;
        //public static bool operator !=(MenuItem<T> a, Enumeration b) => a.Item.Id != b.Id;

        public static bool operator <=(MenuItem<T> a, int b) => a.Item.Id <= b;
        public static bool operator >=(MenuItem<T> a, int b) => a.Item.Id >= b;
        public static bool operator <=(MenuItem<T> a, MenuItem<T> b) => a.Item.Id <= b.Item.Id;
        public static bool operator >=(MenuItem<T> a, MenuItem<T> b) => a.Item.Id >= b.Item.Id;
        //public static bool operator <=(MenuItem<T> a, Enumeration b) => a.Item.Id <= b.Id;
        //public static bool operator >=(MenuItem<T> a, Enumeration b) => a.Item.Id >= b.Id;


        public static implicit operator int(MenuItem<T> a) => a.Item.Id;

        public override bool Equals(object obj) => obj is MenuItem<T> e && Item.Id == e.Item.Id;
        public override int GetHashCode() => HashCode.Combine(Item.Id);
    }
}