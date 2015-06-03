using GTA;

namespace GTAZ.Items {

    public sealed class ItemCoffee : ItemFood{

        // Model: 1348707560
        public ItemCoffee() : base(4, "Coffee", 2) {}

        protected override void Eat(Ped target) {

        }

        public override bool IsDrink() {
            return true;
        }

    }

}
