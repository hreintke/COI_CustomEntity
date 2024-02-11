using Mafi.Core.Input;
using Mafi.Core;
using Mafi.Unity.InputControl.Inspectors;
using Mafi.Unity.UserInterface;
using Mafi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomEntityMod
{
    [GlobalDependency(RegistrationMode.AsEverything)]
    public class CustomEntityInspector : EntityInspector<CustomEntity, CustomEntityWindowView>
    {
        private UiBuilder _uiBuilder;
        private CustomEntityWindowView _windowView;

        public CustomEntityInspector(InspectorContext inspectorContext, UiBuilder uiBuilder) : base(inspectorContext)
        {
            _uiBuilder = uiBuilder;
            _windowView = new CustomEntityWindowView(this);
        }
        protected override CustomEntityWindowView GetView() => this._windowView;

    }

}
