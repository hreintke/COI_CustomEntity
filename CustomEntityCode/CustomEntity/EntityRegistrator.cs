using Mafi.Base;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mafi.Base.Assets.Base.Machines.PowerPlant;
using static Mafi.Base.Assets.Base.Machines;
using static Mafi.Base.Assets;
using static Mafi.Unity.Assets.Unity;
using Mafi;
using static Mafi.Core.Entities.Static.Layout.LayoutEntityProto;
using Mafi.Core.PropertiesDb;
using UnityEngine;

namespace CustomEntityMod
{
    public class CustomEntityRegistrator : IModData
    {
        public void RegisterData(ProtoRegistrator registrator)
        {
            Proto.Str ps = Proto.CreateStr(PrototypeIDs.LocalEntities.CustomEntityID, "CustomEntity", "CustomEntity description");
            EntityLayout el = registrator.LayoutParser.ParseLayoutOrThrow("[8]");

            //          EnitityCosts sc = EntityCosts.None;
            //          Actual EntityCosts in the game are scaled based on DifficulySetting.

            EntityCostsTpl ecTpl = new EntityCostsTpl.Builder().CP(100).Glass(20).Workers(200).Priority(7);
            EntityCosts ec = ecTpl.MapToEntityCosts(registrator);

            LayoutEntityProto.Gfx lg =
  //              new LayoutEntityProto.Gfx("Assets/Base/Machines/Waste/Flare.prefab",
                new LayoutEntityProto.Gfx("Assets/Prefabs/PlaneColumn.prefab",

                customIconPath: "Assets/Unity/Generated/Icons/LayoutEntity/Flare.png",
                categories: new ImmutableArray<ToolbarCategoryProto>?(registrator.GetCategoriesProtos(Ids.ToolbarCategories.Landmarks)))
                ;

            CustomEntityPrototype bp =
                new CustomEntityPrototype(
                    PrototypeIDs.LocalEntities.CustomEntityID, ps, el, ec, lg);
            registrator.PrototypesDb.Add(bp);
        }
    }
}
