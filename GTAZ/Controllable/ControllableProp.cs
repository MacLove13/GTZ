
using GTAZ.Assembly;

namespace GTAZ.Controllable {

    public abstract class ControllableProp : EntityAssembly {

        protected ControllableProp(string groupId, float weightCapacity) : base(groupId, weightCapacity) {}

        protected sealed override void ApplyChanges() {}

    }

}
