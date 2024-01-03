namespace WebClient.State;

public class ProductModalState : IModalState
{
    public int? ProductId { get;  private set; }
    public bool ShowModal { get; private set; }

    public void SetState(int? productId, bool? isOpen)
    {
        ShowModal = isOpen ?? ShowModal;
        ProductId = productId ?? ProductId;
        Console.WriteLine($"IsOpen = {ShowModal}");
        NotifyStateChanged();
    }

    public event Action? OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke();
}