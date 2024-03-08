using Microsoft.Extensions.Caching.Distributed;
using SunRaysMarket.Server.Application.Repositories;
using SunRaysMarket.Server.Infrastructure.Cache;

namespace SunRaysMarket.Server.Infrastructure.Repositories;

internal class CartRepository(ApplicationDbContext dbContext, IDistributedCache cache)
    : ICartRepository
{
    private Cart? CartPersistenceModel { get; set; }
    private CartItem? CartItemPersistenceModel { get; set; }

    public async Task<CartDetailsModel?> GetCartDetailsAsync(int cartId, bool persist = false)
    {
        var cartPersistenceModel = await dbContext
            .Carts
            .Include(c => c.Customer)
            .ThenInclude(cm => cm!.User)
            .Where(c => c.Id == cartId)
            .FirstOrDefaultAsync();

        if (cartPersistenceModel is null)
            return null;

        if (persist)
            CartPersistenceModel = cartPersistenceModel;

        return cartPersistenceModel.Customer is null
            ? new CartDetailsModel { Id = cartPersistenceModel.Id }
            : new CartDetailsModel
            {
                Id = cartPersistenceModel.Id,
                CustomerId = cartPersistenceModel.CustomerId,
                FirstName = cartPersistenceModel.Customer.User!.FirstName,
                LastName = cartPersistenceModel.Customer.User!.LastName,
                Email = cartPersistenceModel.Customer.User!.Email
            };
    }

    public async Task<IEnumerable<CartItemListModel>> GetCartItemsAsync(int cartId)
    {
        return await QueryCartItems(cartId).ToArrayAsync();
    }

    public IAsyncEnumerable<CartItemListModel> GetCartItemsAsyncEnumerable(int cartId)
    {
        return QueryCartItems(cartId).AsAsyncEnumerable();
    }

    public async Task<IEnumerable<CartItemControlModel>> GetAllCartItemInfoAsync(int cartId)
    {
        const string key = "GetCartInfoList";

        if (
            cache.TryGetValue<IEnumerable<CartItemControlModel>>(
                key,
                out var cachedCartItemInfoList
            )
        )
            return cachedCartItemInfoList!;

        var fetchedCartItemInfoList = await dbContext
            .CartItems
            .Where(ci => ci.CartId == cartId)
            .Select(
                ci =>
                    new CartItemControlModel
                    {
                        Id = ci.Id,
                        ProductId = ci.ProductId,
                        Quantity = ci.Quantity
                    }
            )
            .ToArrayAsync();

        await cache.SetValueAsync(
            key,
            fetchedCartItemInfoList,
            new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.Add(TimeSpan.FromSeconds(60))
            }
        );

        return fetchedCartItemInfoList;
    }

    public async Task<CartItemControlModel?> GetCartItemControlInfoAsync(int cartId, int productId)
    {
        return await dbContext
            .CartItems
            .Where(ci => ci.CartId == cartId && ci.ProductId == productId)
            .Select(
                ci =>
                    new CartItemControlModel
                    {
                        Id = ci.Id,
                        ProductId = ci.ProductId,
                        Quantity = ci.Quantity
                    }
            )
            .FirstOrDefaultAsync();
    }

    public Task<bool> CartExistsAsync(int cartId)
    {
        throw new NotImplementedException();
    }

    public async Task CreateCartAsync(int? customerId, bool persist = false)
    {
        var cart = new Cart { CustomerId = customerId };

        if (persist)
            CartPersistenceModel = cart;

        await dbContext.Carts.AddAsync(cart);
    }

    public Task<bool> DeleteCartAsync(int cartId)
    {
        var cart = dbContext.Carts.Find(cartId);

        if (cart is null)
            return Task.FromResult(false);

        dbContext.Carts.Remove(cart);
        return Task.FromResult(true);
    }

    public async Task AddProductToCartAsync(
        int cartId,
        int productId,
        int quantity,
        bool persist = false
    )
    {
        var cartItem = new CartItem
        {
            CartId = cartId,
            ProductId = productId,
            Quantity = quantity
        };

        if (persist)
            CartItemPersistenceModel = cartItem;

        await dbContext.CartItems.AddAsync(cartItem);
    }

    public async Task RemoveItemFromCartAsync(int itemId)
    {
        var cartItem = await dbContext.CartItems.FindAsync(itemId);

        if (cartItem is not null)
            dbContext.CartItems.Remove(cartItem);
    }

    public async Task RemoveItemFromCartAsync(int cartId, int productId)
    {
        var cartItem = await dbContext
            .CartItems
            .Where(ci => ci.CartId == cartId && ci.ProductId == productId)
            .FirstOrDefaultAsync();

        if (cartItem is not null)
            dbContext.CartItems.Remove(cartItem);
    }

    public async Task UpdateProductQuantityAsync(int itemId, int quantity, bool persist = false)
    {
        var cartItem = await dbContext.CartItems.FindAsync(itemId);

        if (cartItem is not null)
        {
            cartItem.Quantity = quantity;

            if (persist)
                CartItemPersistenceModel = cartItem;
        }
    }

    public int GetPersistedCartId()
    {
        return CartPersistenceModel?.Id ?? throw new NullReferenceException("No cart is persisted");
    }

    public CartDetailsModel GetPersistedCartDetails()
    {
        if (CartPersistenceModel is null)
            throw new NullReferenceException("No cart is persisted");

        return CartPersistenceModel.Customer is null
            ? new CartDetailsModel { Id = CartPersistenceModel.Id }
            : new CartDetailsModel
            {
                Id = CartPersistenceModel.Id,
                CustomerId = CartPersistenceModel.CustomerId,
                FirstName = CartPersistenceModel.Customer.User!.FirstName,
                LastName = CartPersistenceModel.Customer.User!.LastName,
                Email = CartPersistenceModel.Customer.User!.Email
            };
    }

    public int GetPersistedCartItemId()
    {
        return CartItemPersistenceModel?.Id
            ?? throw new NullReferenceException("No cart item is persisted");
    }

    public void ClearPersistedCart()
    {
        CartPersistenceModel = null;
    }

    public int? GetPersistedCartItemQuantity()
    {
        return CartItemPersistenceModel?.Quantity
            ?? throw new NullReferenceException("No cart item is persisted");
    }

    private IQueryable<CartItemListModel> QueryCartItems(int cartId)
    {
        return dbContext
            .Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product)
            .Where(c => c.Id == cartId)
            .SelectMany(c => c.CartItems)
            .Select(
                ci =>
                    new CartItemListModel
                    {
                        Id = ci.Id,
                        CartId = ci.CartId,
                        ProductId = ci.ProductId,
                        ProductName = ci.Product.Name,
                        ProductSlug = ci.Product.Slug,
                        ProductPhotoUrl = ci.Product.PhotoUrl,
                        Quantity = ci.Quantity,
                        RegularPrice = ci.Product.Price,
                        DiscountDecimal = ci.Product.DiscountPercent
                    }
            );
    }
}
