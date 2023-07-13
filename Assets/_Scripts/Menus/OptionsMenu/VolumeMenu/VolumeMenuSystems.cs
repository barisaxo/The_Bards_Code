
namespace Menus.OptionsMenu
{
    public static class VolumeMenuSystems
    {
        public static string DisplayVolLVL(this VolumeData.DataItem item, VolumeData data) =>
            item.Name + ": " + data.GetLevel(item) + "%";

        public static void UpdateAllItems(this VolumeMenu menu, VolumeData data)
        {
            foreach (var item in menu.MenuItems)
            {
                item.Card.SetTextString(item.Item.DisplayVolLVL(data));
            }
        }
    }
}