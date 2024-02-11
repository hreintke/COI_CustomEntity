using Mafi.Base;
using Mafi.Core.Mods;
using Mafi.Core.Research;
using Mafi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomEntityMod;

namespace CustomEntityMod
{
    public partial class PrototypeIDs
    {
        public partial class Research
        {
            public static readonly ResearchNodeProto.ID UnlockCustomEntity = Ids.Research.CreateId("UnlockCustomEntity");
        }
    }

    public class EntityResearch : IModData
    {
        public void RegisterData(ProtoRegistrator registrator)
        {
            registrator.ResearchNodeProtoBuilder
                .Start("Custom Entities", PrototypeIDs.Research.UnlockCustomEntity)
                .Description("The unlocking of the Custom Entity")
                .AddLayoutEntityToUnlock(PrototypeIDs.LocalEntities.CustomEntityID)
                .SetGridPosition(registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.Blueprints).GridPosition + new Vector2i(0,4))
                .SetCosts(new ResearchCostsTpl(5))
                .AddParents(registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.CaptainsOffice))
                .BuildAndAdd();
        }
    }
}
