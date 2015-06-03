using System;
using System.Collections.Generic;
using System.Linq;
using GTAZ.Inventory;
using mlgthatsme.GUI;

namespace GTAZ.Shopping {

    public delegate void StoreItemSoldEventHandler(StoreItem item, int quantity, object sender, EventArgs e);

    public abstract class Store {

        protected event StoreItemSoldEventHandler Sold;

        private readonly string _name;

        private readonly List<StoreItem> _selection = new List<StoreItem>();
        private readonly List<ItemStack> _stock = new List<ItemStack>();

        private readonly BaseMenu _menu;

        /// <summary>
        /// Constructs a new Store instance with the specified name, menu, stock and selection.
        /// </summary>
        /// <param name="name">The name of the new Store.</param>
        /// <param name="menu">The menu of the Store to opened when a user enters it.</param>
        /// <param name="stock">The stock of the Store.</param>
        /// <param name="selection">The selection of StoreItems.</param>
        protected Store(string name, BaseMenu menu, IEnumerable<ItemStack> stock, params StoreItem[] selection) {

            _name = name;
            _menu = menu;

            stock.ToList().ForEach(i => _stock.Add(i));
            selection.ToList().ForEach(i => _selection.Add(i));

        }

        /// <summary>
        /// Set the availability of the specified StoreItems.
        /// </summary>
        /// <param name="value">The new availability.</param>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        protected Store SetAvailability(bool value, params StoreItem[] items) {
            items.ToList().ForEach(i => i.SetAvailable(value));
            return this;
        }

        /// <summary>
        /// Purchases the specified Item from this Store into the specified Inventory.
        /// </summary>
        /// <param name="item">The Item to sell.</param>
        /// <param name="units">The amount of units to sell.</param>
        /// <param name="inventory">The inventory to retrieve the sold items.</param>
        /// <returns></returns>
        public bool Sell(Item item, int units, Inventory.Inventory inventory) {
            return Sell(new ItemStack(item, units), inventory);
        }

        /// <summary>
        /// Sells the specified stack from this Store into the specified Inventory.
        /// </summary>
        /// <param name="stack">The ItemStack to sell from this Store.</param>
        /// <param name="inventory">The Inventory to retrieve the sold items.</param>
        /// <returns></returns>
        public bool Sell(ItemStack stack, Inventory.Inventory inventory) {

            if (!IsBuyable(stack)) {
                return false;
            }

            var itemStack1 = GetStacksWith(stack.Item, stack.Size).ToArray()[0];
            var itemStackSize = itemStack1.Size;

            if (itemStackSize - stack.Size >= 1) {
                itemStack1.SetSize(itemStackSize - stack.Size);
            }

            if (itemStackSize - stack.Size == 0) {
                _stock.Remove(itemStack1);
            }

            inventory.AddItem(itemStack1);
            GetStoreItem(stack).OnItemPurchase(Main.Player, inventory);

            if (Sold != null)
                Sold(GetStoreItem(stack), stack.Size, this, EventArgs.Empty);

            return !(itemStackSize - stack.Size < 0);

        }

        private IEnumerable<ItemStack> GetStacksWith(Item item, int quantity) {
            return _stock.Where(i => i.Item == item && i.Size == quantity);
        }
         
        /// <summary>
        /// Returns whether a StoreItem of the specified type exists in the selection of this Store.
        /// </summary>
        /// <param name="item">The type of Item.</param>
        /// <returns></returns>
        public bool IsInSelection(Item item) {
            return _selection.Any(s => s.GetItem() == item);
        }

        /// <summary>
        /// Returns whether the specified ItemStack is buyable in this Store.
        /// </summary>
        /// <param name="stack">The ItemStack to request.</param>
        /// <returns></returns>
        public bool IsBuyable(ItemStack stack) {
            return IsInSelection(stack.Item) && IsInStock(stack) && GetStoreItem(stack).GetAvailability();
        }

        /// <summary>
        /// Returns whether the specified ItemStack is in stock in this Store.
        /// </summary>
        /// <param name="stack">The ItemStack to request.</param>
        /// <returns></returns>
        public bool IsInStock(ItemStack stack) {
            return IsInStock(stack.Item, stack.Size);
        }

        /// <summary>
        /// Returns whether the specified type of Item with the specified quantity is in stock within this Store.
        /// </summary>
        /// <param name="item">The type of Item.</param>
        /// <param name="quantity">The quantity of the Item.</param>
        /// <returns></returns>
        public bool IsInStock(Item item, int quantity = 1) {
            return _stock.Any(i => i.Item == item && i.Size == quantity);
        }

        /// <summary>
        /// Returns the price in total of the specified Item.
        /// </summary>
        /// <param name="item">The Item to calculate the price of.</param>
        /// <param name="units">The amount of units.</param>
        /// <returns></returns>
        public float GetPriceOf(Item item, int units = 1) {
            return GetPriceOf(new ItemStack(item, units));
        }

        /// <summary>
        /// Returns the price in total of the specified ItemStack.
        /// </summary>
        /// <param name="stack">The ItemStack to calculate the price of.</param>
        /// <returns></returns>
        public float GetPriceOf(ItemStack stack) {
            return GetStoreItem(stack.Item).GetPrice()*stack.Size;
        }

        /// <summary>
        /// Returns the name of this Store.
        /// </summary>
        /// <returns></returns>
        public string GetName() {
            return _name;
        }

        /// <summary>
        /// Returns the menu that will be opened when entering this Store.
        /// </summary>
        /// <returns></returns>
        public BaseMenu GetMenu() {
            return _menu;
        }

        /// <summary>
        /// Returns the selection of StoreItem in this Store.
        /// </summary>
        /// <returns></returns>
        public List<StoreItem> GetSelection() {
            return _selection;
        } 

        /// <summary>
        /// Returns what is in stock of this Store.
        /// </summary>
        /// <returns></returns>
        public List<ItemStack> GetStock() {
            return _stock;
        }

        private StoreItem GetStoreItem(ItemStack stack) {
            return GetStoreItem(stack.Item);
        }

        private StoreItem GetStoreItem(Item item) {
            return _selection.Where(i => i.GetItem() == item).ToArray()[0];
        }

    }

}
