using SunRaysMarket.Server.Application.Repositories;

namespace SunRaysMarket.Server.Application.UnitOfWork;

public interface IUnitOfWork
{
    IAddressRepository AddressRepository { get; }
    public ICartRepository CartRepository { get; }
    public ICustomerRepository CustomerRepository { get; }
    public IDepartmentRepository DepartmentRepository { get; }

    public IImageRepository ImageRepository { get; }
    public IOrderRepository OrderRepository { get; }
    public IProductRepository ProductRepository { get; }
    public IProductTypeRepository ProductTypeRepository { get; }
    public IStoreRepository StoreRepository { get; }
    public ITimeSlotRepository TimeSlotRepository { get; }
    public ITransactionRepository TransactionRepository { get; }

    public IUnitOfMeasureRepository UnitOfMeasureRepository { get; }

    public Task<int> SaveChangesAsync();
}
