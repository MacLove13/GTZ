using System.Drawing;
using GTAZ.Shopping;
using mlgthatsme.GUI;

namespace GTAZ.Menus {

    public class StoreMenu : BaseMenu {

        public class StoreItemMenu : BaseMenu {
            
        }

        private readonly Store _store;

        public StoreMenu(Store store) {

            _store = store;

            TitleText = _store.GetName();
            TitleColor = Color.White;
            TitleOverlay = new SpriteDefinition("www_merryweathersecurity_com", "texturesheet_map");

        }

        public override void InitControls() {

            base.InitControls();



        }

        public Store GetStore() {
            return _store;
        }

    }

}
