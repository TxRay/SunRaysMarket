namespace SunRaysMarket.Server.Application.Services;

/// <summary>
/// Service for searching products.
/// </summary>
public interface IProductSearchService
{ 
    /// <summary>
    /// Get search results for a given search string.
    /// </summary>
    /// <param name="searchString">The search string to use.</param>
    /// <returns>An async enumerable of <see cref="ProductListModel"/> objects.</returns>
    IAsyncEnumerable<ProductListModel> GetSearchResults(string? searchString);
}