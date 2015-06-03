using System;
using System.Linq;
using GTA;
using GTAZ.Inventory;
using GTAZ.Items;

namespace GTAZ {

    /// <summary>
    /// This class holds all Item definitions.
    /// </summary>
    public static class ItemsDef {

        public static ItemExample ItemExample = (ItemExample) new ItemExample().SetStackSize(16);

        public static ItemApple ItemApple = (ItemApple) new ItemApple().SetStackSize(16);
        public static ItemBanana ItemBanana = (ItemBanana) new ItemBanana().SetStackSize(16);
        public static ItemBread ItemBread = (ItemBread) new ItemBread().SetStackSize(16);
        public static ItemCoffee ItemCoffee = (ItemCoffee) new ItemCoffee().SetStackSize(16);
        public static ItemOrange ItemOrange = (ItemOrange) new ItemOrange().SetStackSize(16);
        public static ItemGrapes ItemGrapes = (ItemGrapes) new ItemGrapes().SetStackSize(16);

        /// <summary>
        /// All items of the mod.
        /// </summary>
        public readonly static Item[] Items = {
            ItemExample,
            ItemApple,
            ItemBanana,
            ItemBread,
            ItemCoffee,
            ItemOrange,
            ItemGrapes
        };

        /// <summary>
        /// Picks the specified amount of random items and returns them.
        /// </summary>
        /// <param name="min">The minimum amount of new items.</param>
        /// <param name="max">The maximum amount of new items.</param>
        /// <returns></returns>
        public static ItemStack[] PickRandom(int min, int max) {
            return PickRandom(min, max, Items);
        }

        /// <summary>
        /// Picks the specified amount of random items and returns them.
        /// </summary>
        /// <param name="min">The minimum amount of new items.</param>
        /// <param name="max">The maximum amount of new items.</param>
        /// <param name="items">The array of items to pick from.</param>
        /// <returns></returns>
        public static ItemStack[] PickRandom(int min, int max, params Item[] items) {

            if (!(min > 0 && max > min && items != null)) {
                return null;
            }

            ItemStack[] resultItems;

            var rand = new Random(Game.GameTime);
            var itemsCount = rand.Next(min, max);

            // Initialize a new array with the random capacity.
            resultItems = new ItemStack[itemsCount];

            for (var i = 0; i < itemsCount; i++) {

                // Generate a random item and stack-size.
                var item = ItemsDef.Items[rand.Next(0, ItemsDef.Items.Length - 1)];
                var size = rand.Next(0, item.GetMaxStackSize() - 1);

                // Put it into the i index element.
                resultItems[i] = new ItemStack(item, size);

            }

            return resultItems;

        }

        /// <summary>
        /// Returns the specified items as a String[].
        /// </summary>
        /// <param name="items">The items to return.</param>
        /// <returns></returns>
        public static string[] ItemsToArray(params Item[] items) {
            
            var result = new string[items.Length];
            for (var i = 0; i < items.Length; i++) {
                result[i] = items[i].ToString();
            }

            return result;

        }

    }

}
