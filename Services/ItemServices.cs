using RazorPagesPizza.Models;

namespace RazorPagesPizza.Services
{
    public class ItemService
    {
        static List<Item> Items { get; }
        static int nextId = 3;
        static ItemService()
        {
            Items = new List<Item>{};
        }

        public static List<Item> GetAll() => Items;

        public static Item? Get(int id) => Items.FirstOrDefault(p => p.Id == id);

        public static void Add(Item item)
        {
            item.Id = nextId++;
            Items.Add(item);
        }

        public static void Delete(int id)
        {
            var item = Get(id);
            if (item is null)
                return;

            Items.Remove(item);
        }

        public static void Update(Item item)
        {
            var index = Items.FindIndex(p => p.Id == item.Id);
            if (index == -1)
                return;

            Items[index] = item;
        }
    }
}
