using Application.DomainModels;

namespace Application.Services;

public interface ICartService
{
    Task FetchCartAsync(int cartId);
    CartDetailsModel GetCartDetails();
}
