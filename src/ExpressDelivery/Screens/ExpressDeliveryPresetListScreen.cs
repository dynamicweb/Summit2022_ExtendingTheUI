using Dynamicweb.CoreUI.Actions;
using Dynamicweb.CoreUI.Actions.Implementations;
using Dynamicweb.CoreUI.Icons;
using Dynamicweb.CoreUI.Lists;
using Dynamicweb.CoreUI.Lists.ViewMappings;
using Dynamicweb.CoreUI.Screens;
using ExpressDelivery.Models;
using ExpressDelivery.Queries;

namespace ExpressDelivery.Screens
{
    public sealed class ExpressDeliveryPresetListScreen : ListScreenBase<ExpressDeliveryPresetDataModel>
    {
        protected override string GetScreenName() => "Express Delivery presets";
        protected override ListScreenConfiguration GetDefaultScreenConfiguration() => new(typeof(ExpressDeliveryPresetListScreen));

        protected override IEnumerable<ListViewMapping> GetViewMappings() => new[]
        {
            new RowViewMapping
            {
                Columns = new[]
                {
                    CreateMapping(m => m.Name),
                    CreateMapping(m => m.TimeLimitInHours),
                    CreateMapping(m => m.UnderHalfWayText),
                    CreateMapping(m => m.OverHalfWayText),
                    CreateMapping(m => m.TooLateText),
                }
            }
        };

        protected override ActionBase GetListItemPrimaryAction(ExpressDeliveryPresetDataModel model)
             => new NavigateScreenAction<ExpressDeliveryPresetEditScreen>(new ExpressDeliveryByIdQuery { Id = model.Id });

        protected override ActionNode GetItemCreateAction()
            => new ActionNode { Name = "New preset", Icon = Icon.PlusSquare, NodeAction = new NavigateScreenAction<ExpressDeliveryPresetEditScreen>() };
    }
}
