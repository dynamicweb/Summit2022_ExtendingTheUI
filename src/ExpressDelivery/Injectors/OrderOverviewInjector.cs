using Dynamicweb.CoreUI;
using Dynamicweb.CoreUI.Layout;
using Dynamicweb.CoreUI.Screens;
using Dynamicweb.Ecommerce.UI.Screens;
using ExpressDelivery.Api;
using ExpressDelivery.Queries;
using ExpressDelivery.Screens;

namespace ExpressDelivery.Injectors;

public sealed class OrderOverviewInjector : ScreenInjector<OrderOverviewScreen>
{
    public override void OnAfter(OrderOverviewScreen screen, UiComponentBase content)
    {
        if (content is not ScreenLayout layout || layout.Root is not Section section)
            return;

        var preset = ExpressDeliveryPresetService.GetExpressDeliveryPresetByOrderId(screen.Model.Id);
        if (preset is null)
            return;

        var group = new Group().WithComponent(new Dynamicweb.CoreUI.Displays.ShowScreen()
        {
            Load = Dynamicweb.CoreUI.Displays.ShowScreen.LoadMethod.Async,
            Query = new ExpressDeliveryPresetByOrderIdQuery() { OrderId = screen.Model.Id },
            Value = typeof(ExpressDeliveryWidgetScreen)
        });

        group.Width = Group.GroupWidth.Half;
        section.WithGroup(group);
    }
}
