namespace SunRaysMarket.Client.Components.Modals;

/// <summary>
/// </summary>
public class ModalOptions
{
    /// <summary>
    ///     The title to be displayed in the modals header.
    /// </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>
    ///     The width of the modal's content area. Any valid value of the css width property may be used.
    /// </summary>
    public string Width { get; init; } = "90%";

    /// <summary>
    ///     The height of the modal's content area. Any valid value of the css width property may be used.
    /// </summary>
    public string Height { get; init; } = "auto";

    /// <summary>
    ///     Parameters to be passed to the modal content component.
    /// </summary>
    public Dictionary<string, object> ModalParameters { get; init; } = new();
}
