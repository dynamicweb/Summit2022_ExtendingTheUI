using Dynamicweb.CoreUI.Data;
using ExpressDelivery.Api;
using ExpressDelivery.Models;

namespace ExpressDelivery.Queries
{
    public sealed class ExpressDeliveriesQuery : DataQueryListBase<ExpressDeliveryPresetDataModel, ExpressDeliveryPreset>
    {
        protected override IEnumerable<Api.ExpressDeliveryPreset> GetListItems()
            => ExpressDeliveryPresetService.GetExpressDeliveries();
    }
}
