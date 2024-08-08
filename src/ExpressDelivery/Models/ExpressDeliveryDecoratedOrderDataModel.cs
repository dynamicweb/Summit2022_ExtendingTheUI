using Dynamicweb.CoreUI.Data;
using Dynamicweb.Ecommerce.Orders;
using Dynamicweb.Ecommerce.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressDelivery.Models
{
    public sealed class ExpressDeliveryDecoratedOrderDataModel : DataViewModelBase
    {
        public OrderDataModel? Order { get; set; }
        public ExpressDeliveryPresetDataModel? DeliveryPreset { get; set; }
    }
}
