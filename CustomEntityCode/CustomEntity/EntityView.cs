﻿using Mafi.Unity.InputControl.Inspectors;
using Mafi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mafi.Unity.UiFramework.Components;
using Mafi.Unity.UserInterface;
using Mafi.Core;
using Mafi.Core.Input;
using System.Runtime.Remoting.Lifetime;
using Mafi.Unity.UiFramework;
using Mafi.Unity.InputControl;
using Mafi.Core.Products;
using Mafi.Core.Syncers;
using Mafi.Base.Prototypes.Machines.ComputingEntities;
using Mafi.Unity;

namespace CustomEntityMod
{
    public class CustomEntityWindowView : StaticEntityInspectorBase<CustomEntity>
    {
        private readonly CustomEntityInspector _inspector;
        private Btn CustomEntityButton;
        private Txt sliderLabel;
        private ProtosFilterEditor<ProductProto> m_filterView;

        public CustomEntityWindowView(CustomEntityInspector inspector) : base(inspector)
        {
            _inspector = inspector;
//            this.SetOnCloseButtonClickAction((Action)(() => { LogWrite.Info("View onClose"); inspector.Deactivate(); })); // { inspector.Deactivate(); }));
        }

        protected override CustomEntity Entity => _inspector.SelectedEntity;

        protected override void AddCustomItems(StackContainer itemContainer)
        {
            UpdaterBuilder updaterBuilder = UpdaterBuilder.Start();
            base.AddCustomItems(itemContainer);
            AddSectionTitle(itemContainer, "These are the actions");
            CustomEntityButton = AddButton(itemContainer, () => { Entity.buttonAction(); }, "Action Button");
            CustomEntityButton.SetEnabled(true);
            sliderLabel = Builder
                .NewTxt("")
                .SetTextStyle(Builder.Style.Global.TextControls)
                .SetText("Default text");
            sliderLabel.AppendTo(itemContainer);

            StatusPanel statusInfo = AddStatusInfoPanel();
            updaterBuilder.Observe<CustomEntity.State>((Func<CustomEntity.State>)(() => this.Entity.CurrentState)).Do((Action<CustomEntity.State>)(state =>
            {
                switch (state)
                {
                    case CustomEntity.State.None:
                        statusInfo.SetStatus(Tr.EntityStatus__Idle, StatusPanel.State.Warning);
                        break;
                    case CustomEntity.State.Working:
                        statusInfo.SetStatusWorking();
                        break;
                    case CustomEntity.State.Paused:
                        statusInfo.SetStatusPaused();
                        break;
//                    case Mainframe .State.Broken:
//                        statusInfo.SetStatus(Tr.EntityStatus__Broken, StatusPanel.State.Critical);
//                        break;
                    case CustomEntity.State.NotEnoughWorkers:
                        statusInfo.SetStatusNoWorkers();
                        break;
 //                   case Mainframe.State.NotEnoughElectricity:
 //                       statusInfo.SetStatus(Tr.EntityStatus__LowPower, StatusPanel.State.Critical);
 //                       break;
                }
            }));
            //  m_filterView = new ProtosFilterEditor<ProductProto>(this.Builder, (IWindowWithInnerWindowsSupport) this, this.ItemsContainer, new Action<ProductProto>((p) => { }), new Action<ProductProto>((p) => { }),null,null, usePrimaryBtnStyle: false);
            updaterBuilder.Observe<int>((Func<int>)(() => this.Entity.pushCount)).Do((Action<int>)(pc => { sliderLabel.SetText(Entity.getLabelTxt()); }));
            this.AddUpdater(updaterBuilder.Build());

        }
    }
}
