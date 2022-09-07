using Dynamicweb.CoreUI;
using Dynamicweb.CoreUI.Screens;
using ExpressDelivery.Components;
using ExpressDelivery.Models;

namespace ExpressDelivery.Screens
{
    public sealed class ExpressDeliveryWidgetScreen : ScreenBase<ExpressDeliveryDecoratedOrderDataModel>
    {
        public override UiComponentBase? GetDefinition()
        {
            if (Model.Order is null || Model.DeliveryPreset is null)
                return null;

            var spanBetween = DateTime.Now.Subtract(Model.Order.CompletedDate ?? DateTime.Now);
            var elapsedFraction = (spanBetween.TotalHours / Model.DeliveryPreset.TimeLimitInHours);
            var text = elapsedFraction < 0.5 ? Model.DeliveryPreset.OverHalfWayText : elapsedFraction > 1 ? Model.DeliveryPreset.TooLateText : Model.DeliveryPreset.UnderHalfWayText;

            return new ExpressDeliveryWidget()
            {
                Header = $"Express delivery - {Model.DeliveryPreset.Name}",
                ShippingComment = text ?? "",
                ShippingLimit = Model.DeliveryPreset.TimeLimitInHours,
                RemainingTime = TimeSpan.FromHours(Model.DeliveryPreset.TimeLimitInHours) - spanBetween
            };
        }
    }
}
