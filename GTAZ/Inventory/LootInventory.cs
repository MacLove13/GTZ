using System;
using System.Linq;
using GTA;
using GTAZ.Menus;
using mlgthatsme.GUI;

namespace GTAZ.Inventory {

    public class LootInventory : Inventory {

        public LootInventory(params Item[] loot) : base("Loot Inventory", 16) {
            GenerateLoot(loot);
        }

        private void GenerateLoot(params Item[] par1) {

            var items = par1;

            var rand1 = new Random(Game.GameTime);
            par1.ToList().ForEach(i => {

                if (rand1.Next(0, 100) < (int) i.Rarity)
                    AddItem(i, rand1.Next(1, i.GetMaxStackSize()));

            } );

        }

        protected override BaseMenu GetMenu() {
            return new ManageItemMenu(this);
        }

    }

}
