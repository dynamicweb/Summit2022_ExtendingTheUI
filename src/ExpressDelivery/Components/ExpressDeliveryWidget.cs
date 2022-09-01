using Dynamicweb.CoreUI;

namespace ExpressDelivery.Components
{
    public sealed class ExpressDeliveryWidget : UiComponentBase
    {
        public string Header { get; set; } = "";
        public int ShippingLimit { get; set; }
        public int ElapsedHours { get; set; }
        public string ShippingComment { get; set; } = "";
    }
}
