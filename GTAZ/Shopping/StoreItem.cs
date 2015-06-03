using System;
using GTA;
using GTAZ.Inventory;

namespace GTAZ.Shopping {

    public delegate void StoreItemPurchaseEventHandler(Player player, Inventory.Inventory inventory, object sender, EventArgs e);
    public delegate void StoreItemUseEventHandler(Ped ped, object sender, EventArgs e);

    public abstract class StoreItem {

        protected event StoreItemPurchaseEventHandler Purchase;
        protected event StoreItemUseEventHandler Use;

        private readonly string _name;

        private readonly int _id;
        private readonly Item _item;

        private float _price;

        private bool _available = true;

        protected StoreItem(Item item, int id, string name, float price) {
            _item = item;
            _id = id;
            _name = name;
            _price = price;
        }

        public void OnItemUse(Ped ped) {
            if (Use != null) Use(ped, this, EventArgs.Empty);
        }

        public void OnItemPurchase(Player player, Inventory.Inventory inventory) {
            if (Purchase != null) Purchase(player, inventory, this, EventArgs.Empty);
        }

        /// <summary>
        /// Returns the price of this StoreItem.
        /// </summary>
        /// <returns></returns>
        public float GetPrice() {
            return _price;
        }

        /// <summary>
        /// Returns the identifier of this StoreItem.
        /// </summary>
        /// <returns></returns>
        public int GetId() {
            return _id;
        }

        /// <summary>
        /// Returns the Item of this StoreItem.
        /// </summary>
        /// <returns></returns>
        public Item GetItem() {
            return _item;
        }

        /// <summary>
        /// Returns the availability of this StoreItem.
        /// </summary>
        /// <returns></returns>
        public bool GetAvailability() {
            return _available;
        }

        /// <summary>
        /// Returns the display name of this StoreItem.
        /// </summary>
        /// <returns></returns>
        public string GetDisplayName() {
            return _name;
        }

        /// <summary>
        /// Sets the new price of this StoreItem.
        /// </summary>
        /// <param name="price">The new price.</param>
        /// <returns></returns>
        public StoreItem SetPrice(float price) {
            _price = price;
            return this;
        }

        /// <summary>
        /// Sets the availability of this StoreItem.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public StoreItem SetAvailable(bool value) {
            _available = value;
            return this;
        }

    }

}
