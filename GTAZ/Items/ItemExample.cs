using GTA;
using GTAZ.Inventory;

namespace GTAZ.Items {

    public sealed class ItemExample : Item {

        public ItemExample() : base(0, "Example") {
            
        }

        public override void Use(Player trigger, Ped target) {
        }
    }

}
