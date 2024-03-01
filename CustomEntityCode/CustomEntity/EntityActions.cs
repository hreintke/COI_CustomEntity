using Mafi;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core.Entities;
using Mafi.Core.Input;
using Mafi.Core.Prototypes;
using Mafi.Core.Terrain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomEntityMod
{

    [GlobalDependency(RegistrationMode.AsSelf)]
    public class EntityActions
    {
        private readonly ProtosDb _protosDb;
        private readonly UnlockedProtosDb _unlockedProtosDb;

        public EntityActions(
            ProtosDb protosDb,
            UnlockedProtosDb unlockedProtosDb
        )
        {
            // This unlocks the custom entity at startup
            // Next verions will show the use of research
            unlockedProtosDb.Unlock(ImmutableArray.Create(protosDb.Get(PrototypeIDs.LocalEntities.CustomEntityID).Value));
            unlockedProtosDb.Unlock(ImmutableArray.Create(protosDb.Get(PrototypeIDs.Machine.CustomMachineID).Value));
            unlockedProtosDb.Unlock(ImmutableArray.Create(protosDb.Get(PrototypeIDs.Recipes.CustomMachineGoldOil).Value));
        }
    }
}
