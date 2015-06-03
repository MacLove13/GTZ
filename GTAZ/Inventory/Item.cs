
using System;
using GTA;
using GTA.Math;
using GTAZ.Assembly;

namespace GTAZ.Inventory {

    public delegate void ItemUseEventHandler(Player trigger, Ped target, object sender, EventArgs e);
    
    public abstract class Item : EntityPart {

        /// <summary>
        /// The rarities that an Item can wear.
        /// </summary>
        public enum ItemRarity {
            Common = 50,
            Uncommon = 5,
            Legendary = 1,
        }

        /// <summary>
        /// The categories that an Item can wear.
        /// </summary>
        public enum ItemCategory {
            Miscellaneous,
            Food,
            Utility,
            Fuel,
            Weapon,
            Ammunition
        }

        protected event ItemUseEventHandler Used;
        
        private readonly int _id;
        private readonly string _name;

        private int _maxStackSize;

        protected Item(int id, string name, float weight = 0) : base(name, weight) {
            _id = id;
            _name = name;
        }

        /// <summary>
        /// Uses this Item as the specified trigger and on the specified target.
        /// </summary>
        /// <param name="trigger">The player which triggered this use.</param>
        /// <param name="target">The ped which the Item should be used on.</param>
        public virtual void Use(Player trigger, Ped target) {
            if (trigger == null || target == null)
                return;
            if (Used != null)
                Used(trigger, target, this, EventArgs.Empty);
        }
                    
        /// <summary>
        /// Returns the unique identifier of this Item.
        /// </summary>
        public int Id {
            get { return _id; }
        }

        /// <summary>
        /// Returns the unique name of this Item.
        /// </summary>
        public string Name {
            get { return _name; }
        }

        /// <summary>
        /// Returns the rarity of this Item.
        /// </summary>
        public ItemRarity Rarity { get; set; } = ItemRarity.Common;

        /// <summary>
        /// Returns the category of this Item.
        /// </summary>
        public ItemCategory Category { get; set; } = ItemCategory.Miscellaneous;

        /// <summary>
        /// Returns whether this Item is a food.
        /// </summary>
        /// <returns></returns>
        public virtual bool IsFood() {
            return false;
        }

        /// <summary>
        /// Returns the maximum allowed stack-size for this Item.
        /// </summary>
        /// <returns></returns>
        public int GetMaxStackSize() {
            return _maxStackSize;
        }

        /// <summary>
        /// Sets the maximum stack-size for this Item.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public Item SetStackSize(int size) {
            _maxStackSize = size;
            return this;
        }

        /// <summary>
        /// Returns this Item as a String.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return  _id + " ("+  _name + ")";
        }
    }

}
