using Mafi.Base.Prototypes.Buildings;
using Mafi.Core.Entities.Static;
using Mafi.Core.Entities;
using Mafi.Core;
using Mafi.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Prototypes;
using Mafi;
using Mafi.Base;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core.Mods;
using Mafi.Core.Population;
using Mafi.Core.Entities.Priorities;
using Mafi.Core.Factory.ElectricPower;
using Mafi.Core.Maintenance;
using Mafi.Core.Ports;
using Mafi.Core.Ports.Io;

namespace CustomEntityMod
{

    [GenerateSerializer(false, null, 0)]
    public class CustomEntity : LayoutEntity, IEntityWithWorkers, IEntityWithSimUpdate
    {
        private CustomEntityPrototype _proto;

        public enum State
        {
            None,
            Working,
            Paused,
            NotEnoughWorkers,
        }

        public State CurrentState { get; private set; }

        void IEntityWithSimUpdate.SimUpdate()
        {
            CurrentState = updateState();
        }

        private State updateState()
        {
            if (!base.IsEnabled)
            {
                return State.Paused;
            }
            if (Entity.IsMissingWorkers(this))
            {
                return State.NotEnoughWorkers;
            }
            return State.Working;
        }

        private int _pushCount = 0;
        public int pushCount
        { 
            get { return _pushCount; }
        }

        public CustomEntity(EntityId id, CustomEntityPrototype proto, TileTransform transform, EntityContext context) : base(id, proto, transform, context)

        {
            _proto = proto;
        }

        public new CustomEntityPrototype Prototype
        {
            get
            {
                return _proto;
            }
            protected set
            {
                _proto = value;
                base.Prototype = value;
            }
        }

        public override bool CanBePaused => true;

        int IEntityWithWorkers.WorkersNeeded => Prototype.Costs.Workers;
        [DoNotSave(0, null)]
        bool IEntityWithWorkers.HasWorkersCached { get; set; }

        public string getLabelTxt()
        {
            return $"The button has been pushed {_pushCount} times, simUpdateCount" ; 
        }

        public void buttonAction()
        {
            _pushCount++;
        }
        
        private static readonly Action<object, BlobWriter> s_serializeDataDelayedAction;

        private static readonly Action<object, BlobReader> s_deserializeDataDelayedAction;

        public static void Serialize(CustomEntity value, BlobWriter writer)
        {
            if (writer.TryStartClassSerialization(value))
            {
                writer.EnqueueDataSerialization(value, s_serializeDataDelayedAction);
            }
        }

        protected override void SerializeData(BlobWriter writer)
        {
            base.SerializeData(writer);
            writer.WriteGeneric(_proto);
            writer.WriteInt(_pushCount);
        }

        public static CustomEntity Deserialize(BlobReader reader)
        {
            if (reader.TryStartClassDeserialization(out CustomEntity obj, (Func<BlobReader, Type, CustomEntity>)null))
            {
                reader.EnqueueDataDeserialization(obj, s_deserializeDataDelayedAction);
            }
            return obj;
        }

        protected override void DeserializeData(BlobReader reader)
        {
            base.DeserializeData(reader);
            reader.SetField(this, "_proto", reader.ReadGenericAs<CustomEntityPrototype>());
            reader.SetField(this, "_pushCount", reader.ReadInt());
        }

        static CustomEntity()
        {
            s_serializeDataDelayedAction = delegate (object obj, BlobWriter writer)
            {
                ((CustomEntity)obj).SerializeData(writer);
            };
            s_deserializeDataDelayedAction = delegate (object obj, BlobReader reader)
            {
                ((CustomEntity)obj).DeserializeData(reader);
            };
        }
    }
}
