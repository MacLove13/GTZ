using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using GTA;
using GTA.Math;
using GTAZ.Assembly;
using GTAZ.Menus;
using mlgthatsme.GUI;

namespace GTAZ.Inventory {

    public delegate void InventoryChangedEventHandler(object sender, EventArgs e);
    public delegate void InventoryItemUseEventHandler(int index, Player trigger, Ped target, object sender, EventArgs e);
    public delegate void InventoryItemAddedEventHandler(ItemStack add, object sender, EventArgs e);

    /// <summary>
    /// Represents an inventory, storing ItemStacks.
    /// </summary>
    public abstract class Inventory : EntityPart {

        protected event InventoryChangedEventHandler Shown, Closed;

        protected event InventoryItemUseEventHandler ItemUsed;
        protected event InventoryItemAddedEventHandler ItemAdded;

        private readonly string _name;

        private readonly int _capacity;
        private readonly List<ItemStack> _items;

        protected Inventory(string name, int capacity = 16) : base(name) {
            _name = name;
            _items = new List<ItemStack>(capacity);
            _capacity = capacity;
        }

        //

        protected abstract BaseMenu GetMenu();

        protected void UseItem(int index, Player trigger, Ped target) {
            _items[index].UseItem(trigger, target);
            if (ItemUsed != null)
                ItemUsed(index, trigger, target, this, EventArgs.Empty);
        }

        public void DropItem(int index) {
            
        }

        /// <summary>
        /// Shows the menu of this inventory.
        /// </summary>
        public void ShowInventory() {

            // TODO: Show the inventory as a menu.
            Main.WindowManager.AddMenu(GetMenu());
            if (Shown != null) Shown(this, EventArgs.Empty);

        }

        /// <summary>
        /// Closes the menu of this inventory.
        /// </summary>
        public void CloseInventory() {

            // TODO: Close the inventory.
            GetMenu().Close();
            if (Closed != null) Closed(this, EventArgs.Empty);

        }

        public Inventory AddItem(ItemStack item) {

            if (_items.Count + 1 <= _capacity) {

                if (ContainsItem(item.Item)) {

                    var stacks = Get(item.Item).ToArray();
                    var stack = stacks[stacks.Length - 1];

                    if (stack.Size + item.Size >= item.Item.GetMaxStackSize()) {

                        var diff = stack.Size - (stack.Size + item.Size) - item.Item.GetMaxStackSize();

                        stack.SetSize(diff);
                        _items.Add(new ItemStack(item.Item, diff));

                    }

                    stack.SetSize(stack.Size + item.Size);
                    return this;

                }

                _items.Add(item);

            }

            return this;

        }

        public Inventory AddItem(Item item, int size = 1) {
            return AddItem(new ItemStack(item, size));
        }

        public Inventory AddItems(params ItemStack[] stacks) {
            stacks.ToList().ForEach(s => AddItem(s));
            return this;
        }

        public Inventory AddItem(int index, ItemStack item) {
            if (_items.Count + 1 <= _capacity) {
                _items[index] = item;
                if (ItemAdded != null) ItemAdded(item, this, EventArgs.Empty);
            }
            return this;
        }

        public IEnumerable<ItemStack> GetGreaterOrEqual(Item item, int quantity) {
            return _items.Where(i => i.Item == item && i.Size >= quantity);
        }

        public IEnumerable<ItemStack> GetEqual(Item item, int quantity) {
            return _items.Where(i => i.Item == item && i.Size == quantity);
        } 

        public IEnumerable<ItemStack> Get(Item item) {
            return _items.Where(i => i.Item == item);
        }

        public bool ContainsItem(Item item) {
            return _items.Any(i => i.Item == item);
        }

        public bool ContainsItemEqual(Item item, int quantity) {
            return _items.Any(i => i.Item == item && i.Size == quantity);
        }

        public bool ContainsItemGreaterOrEqual(Item item, int quantity) {
            return _items.Any(i => i.Item == item && i.Size >= quantity);
        }

        public bool ContainsItem(ItemStack item) {
            return _items.Any(i => i == item);
        }

        public bool ContainsItem(int index, ItemStack item) {
            return _items[index] == item;
        }

        public Inventory Remove(Item item, int quantity) {

            if (ContainsItemEqual(item, quantity)) {
                _items.Remove(GetEqual(item, quantity).ToArray()[0]);
            }

            if (ContainsItemGreaterOrEqual(item, quantity)) {

                var stack = GetGreaterOrEqual(item, quantity).ToArray()[0];
                if (stack.Size > quantity) {

                    var difference0 = stack.Size - quantity;
                    stack.SetSize(difference0);

                }

            }

            return this;

        }

        public Inventory RemoveItem(ItemStack item) {
            _items.Remove(item);
            return this;
        }

        public Inventory RemoveItem(int index) {
            _items.RemoveAt(index);
            return this;
        }

        //

        public string Name {
            get { return _name; }
        }

        public int Capacity {
            get { return _capacity; }
        }

        public List<ItemStack> Items {
            get { return _items; }
        }

        public int Count(Item item) {
            return _items.Count(i => i.Item.Id == item.Id);
        }

        public int Count() {
            return _items.Count;
        }

    }

}
