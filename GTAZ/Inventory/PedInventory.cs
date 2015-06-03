using GTA;
using GTAZ.Menus;
using mlgthatsme.GUI;

namespace GTAZ.Inventory {

    public class PedInventory : Inventory {

        public PedInventory() : base("Ped Inventory", 16) {}

        protected override BaseMenu GetMenu() {
            return new ManageItemMenu(this);
        }
    }

}
