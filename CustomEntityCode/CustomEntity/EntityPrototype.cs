using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Factory.Machines;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mafi.Core.Prototypes.Proto;

namespace CustomEntityMod
{
    public partial class PrototypeIDs
    {
        public partial class LocalEntities
        {
            public static readonly CustomEntityPrototype.ID CustomEntityID = new CustomEntityPrototype.ID("CustomEntity");
        }
    }

    public class CustomEntityPrototype : LayoutEntityProto, IProto
    {
        public CustomEntityPrototype(ID id, Str strings, EntityLayout layout, EntityCosts costs, Gfx graphics)
             : base(id, strings, layout, costs, graphics)
        {
        }

        public override Type EntityType => typeof(CustomEntity);

        public int actionDuration;
        
    }
}
