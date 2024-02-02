using SunRaysMarket.Server.Application.Repositories;
using SunRaysMarket.Server.Application.UnitOfWork;
using SunRaysMarket.Server.Infrastructure.Data;

namespace SunRaysMarket.Server.Infrastructure.UnitOfWorkImplementation;

internal class UnitOfWork(
    ApplicationDbContext dbContext,
    IAddressRepository addressRepository,
    ICartRepository cartRepository,
    ICustomerRepository customerRepository,
    IDepartmentRepository departmentRepository,
    IImageRepository imageRepository,
    IOrderRepository orderRepository,
    IProductRepository productRepository,
    IProductTypeRepository productTypeRepository,
    IStoreRepository storeRepository,
    ITimeSlotRepository timeSlotRepository,
    ITransactionRepository transactionRepository,
    IUnitOfMeasureRepository unitOfMeasureRepository
) : IUnitOfWork
{
    public IAddressRepository AddressRepository { get; } = addressRepository;
    public ICartRepository CartRepository { get; } = cartRepository;
    public ICustomerRepository CustomerRepository { get; } = customerRepository;
    public IDepartmentRepository DepartmentRepository { get; } = departmentRepository;

    public IImageRepository ImageRepository { get; } = imageRepository;

    public IOrderRepository OrderRepository { get; } = orderRepository;
    public IProductRepository ProductRepository { get; } = productRepository;
    public IProductTypeRepository ProductTypeRepository { get; } = productTypeRepository;
    public IStoreRepository StoreRepository { get; } = storeRepository;
    public ITimeSlotRepository TimeSlotRepository { get; } = timeSlotRepository;
    public ITransactionRepository TransactionRepository { get; } = transactionRepository;
    public IUnitOfMeasureRepository UnitOfMeasureRepository { get; } = unitOfMeasureRepository;

    public async Task<int> SaveChangesAsync()
    {
        return await dbContext.SaveChangesAsync();
    }
}
