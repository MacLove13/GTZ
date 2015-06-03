using System;
using System.Linq;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTAZ.Assembly;
using GTAZ.Controllable;
using GTAZ.Inventory;

namespace GTAZ.Props {

    public delegate void ItemPickupEventHandler(Ped newOwner, Vector3 pos, object sender, EventArgs e);

    public class DroppedLootbagProp : ControllableProp {

        public event ItemPickupEventHandler Pickup;

        private class ItemStackPart : EntityPart {

            private readonly ItemStack[] _itemStack;

            public ItemStackPart(params ItemStack[] stack) : base("Lootbag") {
                _itemStack = stack;
            }

            public ItemStack[] GetItemStack() {
                return _itemStack;
            }

        }

        private readonly ItemStack[] _itemStack;

        public DroppedLootbagProp(params ItemStack[] stack) : base("LOOTBAG_PROP", 25f) {

            _itemStack = stack;
            SetInteractionDistance(4f);

            PlayerKeyDown += OnPlayerKeyDown;

        }

        private void OnPlayerKeyDown(object sender, KeyEventArgs ke) {

            if (ke.KeyCode == Keys.E) {

                _itemStack.ToList().ForEach(i => {
                    Main.PlayerInventory.AddItem(i);
                });

                if (Pickup != null) Pickup(Main.Player.Character, Entity.Position, this, EventArgs.Empty);

                Main.WindowManager.RefreshAllMenus();

                RemoveEntity();
                Entity.Delete();

            }

        }

        public bool IsAllFood() {
            return _itemStack.All(i => i.Item.IsFood());
        }

        protected override void InitializeAssembly() {
            AddPart("ItemStack", new ItemStackPart(_itemStack));
        }
    }

}
