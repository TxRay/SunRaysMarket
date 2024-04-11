using Microsoft.AspNetCore.Components;

namespace SunRaysMarket.Client.Web.Components.Modals;

public partial class Modal : ComponentBase
{
    //[Inject] private IServiceProvider ServiceProvider { get; set; } = null!;

    [Inject]
    private ILogger<Modal>? Logger { get; set; }

    [Inject]
    protected IModalController ModalController { get; set; } = null!;

    protected ModalContext ModalContext { get; set; } = null!;

    protected override void OnInitialized()
    {
        //ModalController = ServiceProvider.GetService<IModalController>();
        ModalContext = ModalController.GetActiveContext();
        ModalController.OnChange += OnStateChanged;
    }

    protected override void OnParametersSet() { }

    private Task OnCloseBtnClicked()
    {
        return ModalController!.Close();
    }

    private void OnStateChanged()
    {
        Logger?.LogInformation("The method 'Modal.OnStateChanged' was called.");
        ModalContext = ModalController!.GetActiveContext();
        StateHasChanged();
    }
}
