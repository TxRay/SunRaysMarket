using Application.EndpointViewModels;

namespace Application.Options;

public class AddItemToCartOptions
{
    public int? CartId { get; set; }
    public AddItemToCartCommand? Command { get; set; }
}