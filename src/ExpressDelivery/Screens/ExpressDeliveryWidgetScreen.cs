﻿using Dynamicweb.CoreUI;
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

            var elapsedHours = DateTime.Now.Subtract(Model.Order.CompletedDate ?? DateTime.Now).TotalHours;
            var elapsedFraction = (elapsedHours / Model.DeliveryPreset.TimeLimitInHours);
            var text = elapsedFraction < 0.5 ? Model.DeliveryPreset.UnderHalfWayText : elapsedFraction > 1 ? Model.DeliveryPreset.TooLateText : Model.DeliveryPreset.OverHalfWayText;

            return new ExpressDeliveryWidget()
            {
                Header = $"Express delivery - {Model.DeliveryPreset.Name}",
                ElapsedHours = (int)Math.Floor(elapsedHours),
                ShippingComment = text ?? "",
                ShippingLimit = Model.DeliveryPreset.TimeLimitInHours
            };
        }
    }
}
