using CustomEntityMod;
using Mafi.Base;
using Mafi.Core.Factory.Machines;
using Mafi.Core.Factory.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomEntityMod
{
    public partial class PrototypeIDs
    {
        public partial class Machine
        {
            public static readonly MachineProto.ID CustomMachineID = new MachineProto.ID("CustomMachine");
        }
        public partial class Recipes
        {
            public static readonly RecipeProto.ID CustomMachineGoldOil = Ids.Recipes.CreateId("GoldToOil");
        }
    }
}
