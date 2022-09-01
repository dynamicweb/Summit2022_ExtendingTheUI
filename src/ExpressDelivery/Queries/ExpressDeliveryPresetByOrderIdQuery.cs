using Dynamicweb.CoreUI.Data;
using Dynamicweb.Ecommerce.UI.Models;
using Dynamicweb.Extensibility2.Mapping;
using ExpressDelivery.Api;
using ExpressDelivery.Models;

namespace ExpressDelivery.Queries
{
    public sealed class ExpressDeliveryPresetByOrderIdQuery : DataQueryModelBase<ExpressDeliveryDecoratedOrderDataModel>
    {
        public string? OrderId { get; set; }
        public override ExpressDeliveryDecoratedOrderDataModel GetModel()
        {
            if (OrderId is null)
                return new();

            var preset = ExpressDeliveryPresetService.GetExpressDeliveryPresetByOrderId(OrderId);
            if (preset is null)
                return new();

            var presetModel = MappingService.Map<ExpressDeliveryPresetDataModel>(preset);
            var order = Dynamicweb.Ecommerce.Services.Orders.GetById(OrderId);
            var orderModel = MappingService.Map<OrderDataModel>(order);

            return new()
            {
                Order = orderModel,
                DeliveryPreset = presetModel
            };
        }
    }
}
