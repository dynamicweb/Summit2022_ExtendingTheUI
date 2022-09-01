using Dynamicweb.CoreUI.Data;
using Dynamicweb.Extensibility2.Mapping;
using ExpressDelivery.Api;
using ExpressDelivery.Models;

namespace ExpressDelivery.Queries
{
    public sealed class ExpressDeliveryByIdQuery : DataQueryIdentifiableModelBase<ExpressDeliveryPresetDataModel, long>
    {
        public long Id { get; set; }

        public override ExpressDeliveryPresetDataModel GetModel()
        {
            var delivery = ExpressDeliveryPresetService.GetExpressDeliveryById(Id);
            return MappingService.Map<ExpressDeliveryPresetDataModel>(delivery);
        }

        protected override void SetKey(long key) => Id = key;
    }
}
