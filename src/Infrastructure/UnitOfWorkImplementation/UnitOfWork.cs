using Application.Repositories;
using Application.UnitOfWork;
using Infrastructure.Data;

namespace Infrastructure.UnitOfWorkImplementation;

internal class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public ICartRepository CartRepository { get; }
    public ICustomerRepository CustomerRepository { get; }
    public IDepartmentRepository DepartmentRepository { get; }

    public IImageRepository ImageRepository { get; }

    public IOrderRepository OrderRepository { get; }
    public IProductRepository ProductRepository { get; }
    public IProductTypeRepository ProductTypeRepository { get; }
    public ITimeSlotRepository TimeSlotRepository { get; }
    public ITransactionRepository TransactionRepository { get; }
    public IUnitOfMeasureRepository UnitOfMeasureRepository { get; }
    public IUserRepository UserRepository { get; }

    public UnitOfWork(
        ApplicationDbContext dbContext,
        ICartRepository cartRepository,
        ICustomerRepository customerRepository,
        IDepartmentRepository departmentRepository,
        IImageRepository imageRepository,
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IProductTypeRepository productTypeRepository,
        ITimeSlotRepository timeSlotRepository,
        ITransactionRepository transactionRepository,
        IUnitOfMeasureRepository unitOfMeasureRepository,
        IUserRepository userRepository
    )
    {
        _dbContext = dbContext;
        CartRepository = cartRepository;
        CustomerRepository = customerRepository;
        DepartmentRepository = departmentRepository;
        ImageRepository = imageRepository;
        OrderRepository = orderRepository;
        ProductRepository = productRepository;
        ProductTypeRepository = productTypeRepository;
        TimeSlotRepository = timeSlotRepository;
        TransactionRepository = transactionRepository;
        UnitOfMeasureRepository = unitOfMeasureRepository;
        UserRepository = userRepository;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}
