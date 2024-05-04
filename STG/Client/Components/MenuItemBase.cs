using Client.Abstractions.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Routing;

namespace Client.Components
{
    public class MenuItemBase : NavLink
    {
        [Parameter]
        public IMenuItem? Model { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            /* Basic component layout inheritance in Blazor
               https://stackoverflow.com/questions/59990832/basic-component-layout-inheritance-blazor
               
               // this would be used in the *.razor file
               base.BuildRenderTree(__builder);
            */

            //...your own logic...

            // this is the equivalent in the code behind file.
            base.BuildRenderTree(builder);
        }
    }
}
