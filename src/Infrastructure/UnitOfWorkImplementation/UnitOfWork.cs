using Application.Repositories;
using Application.UnitOfWork;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.UnitOfWorkImplementation;

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
    public ITimeSlotRepository TimeSlotRepository { get; } = timeSlotRepository;
    public ITransactionRepository TransactionRepository { get; } = transactionRepository;
    public IUnitOfMeasureRepository UnitOfMeasureRepository { get; } = unitOfMeasureRepository;

    public async Task<int> SaveChangesAsync()
    {
        return await dbContext.SaveChangesAsync();
    }
}
