using System.Drawing;
using GTA;
using GTAZ.Inventory;
using mlgthatsme.GUI;

namespace GTAZ.Menus {

    public class DeveloperItemsMenu : BaseMenu {

        public DeveloperItemsMenu() {

            TitleText = "Items DEV";
            TitleColor = Color.LightBlue;
            CustomThemeColor = Color.DeepPink;
            TitleOverlay = new SpriteDefinition("www_merryweathersecurity_com", "texturesheet_map");

        }

        public override void InitControls() {

            base.InitControls();

            var dividerOptions = new Devider("Inventory");

            var multiItems = new Multichoice() {
                Text = "Item",
                Choices = ItemsDef.ItemsToArray(ItemsDef.Items)
            };

            var sliderQuantity = new Slider("Quantity (1)") {
                Value = 1,
                MaxValue = 16,
                IncrementValue = 1
            };

            sliderQuantity.ValueChanged += (sender, args) => {
                sliderQuantity.Text = "Quantity (" + sliderQuantity.Value + ")";
            };

            var multiWhere = new Multichoice() {
                Text = "Where",
                Choices = new [] {"Inventory", "On ground (lootbag)"}
            };

            var buttonSpawn = new Button("Spawn");
            buttonSpawn.OnPress += (sender, args) => {
                
                switch (multiWhere.GetSelectedChoice()) {
                    case "On ground (lootbag)":
                        Main.Populator.SpawnLootbag(new [] { new ItemStack(ItemsDef.Items[multiItems.GetChoiceIndex()], sliderQuantity.Value) }, Main.Player.Character.Position);
                        break;
                    case "Inventory":
                        Main.PlayerInventory.AddItem(ItemsDef.Items[multiItems.GetChoiceIndex()], sliderQuantity.Value);
                        break;
                }

            };

            AddMenuItem(dividerOptions);
            AddMenuItem(multiItems);
            AddMenuItem(sliderQuantity);
            AddMenuItem(multiWhere);
            AddMenuItem(buttonSpawn);

        }
    }

}
