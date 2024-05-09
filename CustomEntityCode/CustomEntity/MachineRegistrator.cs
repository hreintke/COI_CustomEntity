using Mafi;
using Mafi.Base;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core.Entities.Animations;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Factory.Machines;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomEntityMod
{
    public class CustomMachineRegistrator : IModData
    {
        public void RegisterData(ProtoRegistrator registrator)
        {
            Proto.Str ps = Proto.CreateStr(PrototypeIDs.Machine.CustomMachineID, "CustomMachine", "CustomMachine description");

            string[] lo = new string[] {

                    "A#>[2][6][6][6][6][6][6][6][2]>~Y",
                    "B#>[2][7][7][6][6][6][6][4][2]>@Z",
                    "C#>[2][7][7][6][7][7][6][4][2]   ",
                    "D#>[2][7][7][6][7][7][6][4][2]   ",
                    "I~>[2][7][7][6][6][6][6][6][2]   ",
                    "J@>[2][6][6][6][6][6][6][6][2]   ",
                    "         P@v                     "
            };


            EntityLayout el = registrator.LayoutParser.ParseLayoutOrThrow(lo);

            //          EnitityCosts sc = EntityCosts.None;
            //          Actual EntityCosts in the game are scaled based on DifficulySetting.

            EntityCostsTpl ecTpl = new EntityCostsTpl.Builder().CP(100).Glass(20).Workers(200).Priority(7);
            EntityCosts ec = ecTpl.MapToEntityCosts(registrator);

            Electricity elec = Electricity.FromKw(100);

            Computing cp = Computing.FromTFlops(3);

            ImmutableArray<AnimationParams> emptyAnimantion = ImmutableArray<AnimationParams>.Empty;


            MachineProto.Gfx lg = new MachineProto.Gfx("Assets/ExampleMod/BlastFurnace.prefab",
                registrator.GetCategoriesProtos(new Proto.ID[] { Ids.ToolbarCategories.Landmarks }));

            MachineProto bp =
                new MachineProto(
                    PrototypeIDs.Machine.CustomMachineID, ps, el, ec, elec, cp, null, Option<MachineProto>.None , false, emptyAnimantion,  lg);
            registrator.PrototypesDb.Add(bp);

            registrator.RecipeProtoBuilder
                .Start("GoldOil", PrototypeIDs.Recipes.CustomMachineGoldOil, bp)
                .AddInput(10, Ids.Products.Gold, "AB")
                .AddOutput(10, Ids.Products.CrudeOil, "*")
                .SetDurationSeconds(20)
                .BuildAndAdd();
        }
    }
}
