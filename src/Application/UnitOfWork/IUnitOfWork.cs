using Application.Repositories;

namespace Application.UnitOfWork;

public interface IUnitOfWork
{
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
    public Task<int> SaveChangesAsync();
}
