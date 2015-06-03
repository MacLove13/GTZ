using System;
using GTA;
using GTAZ.Inventory;

namespace GTAZ.Items {

    public delegate void ItemFoodEventHandler(object sender, EventArgs e);

    public class ItemFood : Item {

        protected event ItemFoodEventHandler Eaten;

        private readonly int _heartsRestore;

        protected ItemFood(int id, string name, int hrestore) : base(id, name) {
            _heartsRestore = hrestore;
        }

        public int GetRestoreAmount() {
            return _heartsRestore;
        }

        protected virtual void Eat(Ped target) {

            // TODO: Restore the amount of hearts specified in the field.
            UI.Notify("Ate " + Name + " and restored " + _heartsRestore + " hearts");

        }

        public sealed override void Use(Player trigger, Ped target) {
            Eat(target);
            if (Eaten != null)
                Eaten(this, EventArgs.Empty);
        }

        public virtual bool IsDrink() {
            return false;
        }

        public sealed override bool IsFood() {
            return true;
        }

    }

}
