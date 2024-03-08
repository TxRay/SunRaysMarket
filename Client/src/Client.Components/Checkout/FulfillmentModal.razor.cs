using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using SunRaysMarket.Client.Application.Models;
using SunRaysMarket.Client.Components.Modals;
using SunRaysMarket.Client.Components.Stores;
using SunRaysMarket.Shared.Core.DomainModels;
using SunRaysMarket.Shared.Core.Enums;
using SunRaysMarket.Shared.Services.Interfaces;

namespace SunRaysMarket.Client.Components;

public partial class FulfillmentModal : ModalContentBase<AddressModel>
{
    private const string StoreKey = "FulfillmentData";

    [Parameter]
    public EventCallback<FulfillmentModel> OnStateChanged { get; set; }

    [Parameter]
    public EventCallback OnClose { get; set; }

    private bool TabsComponentShouldLoad { get; set; }
    private string? InitialTabIndex { get; set; }
    private FulfillmentModel Model { get; set; } = new FulfillmentModel.EmptyModel();
    private IEnumerable<TimeSlotListModel> DeliveryTimeSlots { get; set; } = [];
    private IEnumerable<TimeSlotListModel> PickupTimeSlots { get; set; } = [];
    private IEnumerable<AddressModel> CustomerAddresses { get; set; } = [];
    private IEnumerable<StoreListModel> StoreLocationInfoList { get; set; } = [];
    private TimeSlotModel? DeliveryModel { get; set; }
    private TimeSlotModel? PickupModel { get; set; }
    private SelectAddressModel? CustomerAddressModel { get; set; }
    private SelectStoreModel? StoreSelectionModel { get; set; }
    private EditContext DeliveryContext { get; set; } = default!;
    private EditContext PickupContext { get; set; } = default!;
    private EditContext CustomerAddressContext { get; set; } = default!;
    private EditContext StoreSelectionContext { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        await RetrieveModelFromStore();

        InitialTabIndex = Model switch
        {
            FulfillmentModel.NonEmptyModel nonEmptyModel
                => Enum.GetName(typeof(OrderType), nonEmptyModel.OrderType),
            _ => null
        };

        Logger?.LogInformation("InitialTabIndex = {initTabIndex}", InitialTabIndex);

        TabsComponentShouldLoad = true;
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (!firstRender)
            return;
        StateHasChanged();
    }

    private async Task OnAddAddressBtnClicked()
    {
        var options = new ModalOptions { Width = "75%", Height = "75%" };

        await ChangeModalContent<AddressEntryModalContent, CreateAddressModel>(options);
    }

    private async Task StoreModel<TModel>()
        where TModel : FulfillmentModel
    {
        try
        {
            await Store.SetValueAsync(StoreKey, (TModel)Model);
        }
        catch (InvalidCastException e)
        {
            Logger?.LogDebug("{Message}", e.Message);
        }
    }

    private async Task RetrieveModelFromStore()
    {
        Model =
            await Store.TryGetValueAsync<FulfillmentModel.DeliveryModel>(StoreKey)
            ?? await Store.TryGetValueAsync<FulfillmentModel.PickupModel>(StoreKey)
            ?? Model;

        Logger?.LogInformation("Type of model = {Type}", Model.GetType());
    }

    private async Task OnTabLoaded(string label)
    {
        await RetrieveModelFromStore();

        switch (label)
        {
            case "Delivery":
                await OnTabLoaded_Delivery();
                break;
            case "Pickup":
                await OnTabLoaded_Pickup();
                break;
        }
    }

    private async Task<(
        IEnumerable<StoreListModel>,
        int?,
        SelectStoreModel
    )> RetrieveStoreLocationInfo()
    {
        var storeInfoList = await CheckoutService.GetStoreLocationsAsync();
        var customerPreferredStoreId = await StoreLocationService.GetPreferredStoreAsync();
        var defaultStoreSelectModel = customerPreferredStoreId is not null
            ? new SelectStoreModel { SelectedStoreId = customerPreferredStoreId.Value }
            : new SelectStoreModel();

        return (storeInfoList, customerPreferredStoreId, defaultStoreSelectModel);
    }

    private async Task OnTabLoaded_Delivery()
    {
        (StoreLocationInfoList, var customerPreferredStoreId, var defaultStoreSelectModel) =
            await RetrieveStoreLocationInfo();

        await RetrieveModelFromStore();

        if (Model is not FulfillmentModel.DeliveryModel)
            Model = new FulfillmentModel.DeliveryModel(default, customerPreferredStoreId);

        var deliveryFulfillmentModel = (FulfillmentModel.DeliveryModel)Model;

        StoreSelectionModel = StoreLocationInfoList.Any(
            si => si.Id == deliveryFulfillmentModel.StoreId
        )
            ? new SelectStoreModel { SelectedStoreId = deliveryFulfillmentModel.StoreId!.Value }
            : defaultStoreSelectModel;

        DeliveryTimeSlots = await CheckoutService.GetCheckoutTimeSlotsAsync(
            StoreSelectionModel.SelectedStoreId,
            OrderType.Delivery
        );

        DeliveryModel = DeliveryTimeSlots.Any(dts => dts.Id == deliveryFulfillmentModel.TimeSlotId)
            ? new TimeSlotModel { SelectedTimeSlotId = deliveryFulfillmentModel.TimeSlotId }
            : new TimeSlotModel();

        /*
         *  After a new address is added using the address entry modal, it will be automatically set
         *  will be retrieved from the modal context's temporary data store and automatically set as
         *  the selected address.
         */
        if (ModalContext.TempData.TryGetValue("AddressId", out var aId) && aId is int newAddressId)
        {
            Model = deliveryFulfillmentModel with { DeliveryAddressId = newAddressId };
            ModalContext.TempData.Remove("AddressId");
            await StoreModel<FulfillmentModel.DeliveryModel>();
        }

        CustomerAddressModel =
            deliveryFulfillmentModel.DeliveryAddressId > 0
                ? new SelectAddressModel
                {
                    SelectedAddressId = deliveryFulfillmentModel.DeliveryAddressId.Value
                }
                : new SelectAddressModel();

        DeliveryContext = new EditContext(DeliveryModel);
        DeliveryContext.OnFieldChanged += DeliveryContext_OnFieldChanged;
        CustomerAddressContext = new EditContext(CustomerAddressModel);
        CustomerAddressContext.OnFieldChanged += CustomerAddressContext_OnFieldChanged;
        StoreSelectionContext = new EditContext(StoreSelectionModel);
        StoreSelectionContext.OnFieldChanged += StoreSelectionContext_OnFieldChanged;

        CustomerAddresses = await CustomerAddressService.GetAddressesAsync();
    }

    private async Task OnTabLoaded_Pickup()
    {
        (StoreLocationInfoList, var customerPreferredStoreId, var defaultStoreSelectModel) =
            await RetrieveStoreLocationInfo();

        await RetrieveModelFromStore();

        if (Model is not FulfillmentModel.PickupModel)
            Model = new FulfillmentModel.PickupModel(default, customerPreferredStoreId);

        var pickupFulfillmentModel = (FulfillmentModel.PickupModel)Model;

        StoreSelectionModel = StoreLocationInfoList.Any(
            si => si.Id == pickupFulfillmentModel.StoreId
        )
            ? new SelectStoreModel { SelectedStoreId = pickupFulfillmentModel.StoreId!.Value }
            : defaultStoreSelectModel;

        PickupTimeSlots = await CheckoutService.GetCheckoutTimeSlotsAsync(
            StoreSelectionModel.SelectedStoreId,
            OrderType.Pickup
        );

        PickupModel = PickupTimeSlots.Any(pts => pts.Id == pickupFulfillmentModel.TimeSlotId)
            ? new TimeSlotModel { SelectedTimeSlotId = pickupFulfillmentModel.TimeSlotId }
            : new TimeSlotModel();

        PickupContext = new EditContext(PickupModel);
        PickupContext.OnFieldChanged += PickupContext_OnFieldChanged;
        StoreSelectionContext = new EditContext(StoreSelectionModel);
        StoreSelectionContext.OnFieldChanged += StoreSelectionContext_OnFieldChanged;
    }

    private void OnTabUnloaded(string label)
    {
        switch (label)
        {
            case "Delivery":
                CustomerAddressContext.OnFieldChanged -= CustomerAddressContext_OnFieldChanged;
                DeliveryContext.OnFieldChanged -= DeliveryContext_OnFieldChanged;
                break;
            case "Pickup":
                PickupContext.OnFieldChanged -= PickupContext_OnFieldChanged;
                break;
        }

        StoreSelectionContext.OnFieldChanged -= StoreSelectionContext_OnFieldChanged;
    }

    private async void PickupContext_OnFieldChanged(object? sender, FieldChangedEventArgs e)
    {
        if (Model is not FulfillmentModel.PickupModel pickupFulfillmentModel)
            return;

        PickupModel = PickupContext.Model as TimeSlotModel;

        Model = pickupFulfillmentModel with { TimeSlotId = PickupModel!.SelectedTimeSlotId };
        await StoreModel<FulfillmentModel.PickupModel>();
    }

    private async void DeliveryContext_OnFieldChanged(object? sender, FieldChangedEventArgs e)
    {
        if (Model is not FulfillmentModel.DeliveryModel deliveryFulfillmentModel)
            return;

        DeliveryModel = DeliveryContext.Model as TimeSlotModel;

        Model = deliveryFulfillmentModel with { TimeSlotId = DeliveryModel!.SelectedTimeSlotId };

        await StoreModel<FulfillmentModel.DeliveryModel>();
    }

    private async void CustomerAddressContext_OnFieldChanged(
        object? sender,
        FieldChangedEventArgs e
    )
    {
        if (Model is not FulfillmentModel.DeliveryModel deliveryFulfillmentModel)
            return;

        CustomerAddressModel = CustomerAddressContext.Model as SelectAddressModel;
        Model = deliveryFulfillmentModel with
        {
            DeliveryAddressId = CustomerAddressModel?.SelectedAddressId
        };
        await StoreModel<FulfillmentModel.DeliveryModel>();
    }

    private async void StoreSelectionContext_OnFieldChanged(object? sender, FieldChangedEventArgs e)
    {
        switch (Model)
        {
            case FulfillmentModel.DeliveryModel deliveryModel:
                Model = deliveryModel with { StoreId = StoreSelectionModel!.SelectedStoreId };
                await StoreModel<FulfillmentModel.DeliveryModel>();
                break;
            case FulfillmentModel.PickupModel pickupModel:
                Model = pickupModel with { StoreId = StoreSelectionModel!.SelectedStoreId };
                await StoreModel<FulfillmentModel.PickupModel>();
                break;
            default:
                Logger.LogError(
                    "The fulfillment model should be of type '{delivery}' or '{pickup}'",
                    nameof(FulfillmentModel.DeliveryModel),
                    nameof(FulfillmentModel.PickupModel)
                );
                break;
        }

        StateHasChanged();
    }

    /*private async void HandleStateChange()
    {
        if (OnStateChanged.HasDelegate)
            await OnStateChanged.InvokeAsync(ModalContext.State);

        StateHasChanged();
    }*/

    private record TimeSlotModel
    {
        public int SelectedTimeSlotId { get; set; }
    }

    private record SelectAddressModel
    {
        public int SelectedAddressId { get; set; }
    }

    private record SelectStoreModel
    {
        public int SelectedStoreId { get; set; }
    }

#nullable disable
    [Inject]
    private ILogger<FulfillmentModal> Logger { get; set; }

    [Inject]
    private ICheckoutService CheckoutService { get; set; }

    [Inject]
    private IStoreLocationService StoreLocationService { get; set; }

    [Inject]
    private IStore Store { get; set; }

    [Inject]
    private ICustomerAddressService CustomerAddressService { get; set; }
}
