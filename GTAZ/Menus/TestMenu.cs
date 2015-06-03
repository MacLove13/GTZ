using System.Drawing;
using System.Linq;
using mlgthatsme.GUI;

namespace GTAZ.Menus {

    public class TestMenu : BaseMenu {

        private string[] _buttons;

        public TestMenu(params string[] buttons) {

            _buttons = buttons;

            TitleText = "Debug Menu";
            TitleColor = Color.White;
            TitleOverlay = new SpriteDefinition("www_merryweathersecurity_com", "texturesheet_map");
            CustomThemeColor = Color.Green;

        }

        public void SetButtons(string[] buttons) {
            _buttons = buttons;
        }

        public override void InitControls() {

            base.InitControls();

            if (_buttons.Length == 0) {
                AddMenuInfo("There are no items to show");
            }

            _buttons.ToList().ForEach(b => {
                var button = new Button(b);
                AddMenuItem(button);
            });

            AddMenuInfo("There are (" + _buttons.Length + ") in total.");

        }
    }

}
