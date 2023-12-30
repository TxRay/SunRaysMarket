using Application.DomainModels;
using Application.Repositories;
using Application.Structs;
using Application.Utilities.OrderCalculations;
using Infrastructure.Data;
using Infrastructure.Data.PersistenceModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Repositories;

internal class OrderRepository(ApplicationDbContext dbContext) : IOrderRepository
{
    private EntityEntry<Order>? OrderEntry { get; set; }

    public async Task<OrderDetailsModel?> GetOrderDetailsAsync(int orderId) =>
        await dbContext
            .Orders
            .Include(o => o.Customer)
            .ThenInclude(c => c!.User)
            .Include(o => o.Store)
            .Include(o => o.TimeSlot)
            .Where(o => o.Id == orderId)
            .Select(
                o =>
                    new OrderDetailsModel
                    {
                        Id = o.Id,
                        CustomerId = o.CustomerId,
                        CustomerName = o.Customer!.User!.FirstName,
                        OrderNumber = o.OrderNumber,
                        StoreId = o.StoreId,
                        DeliveryAddressId = o.DeliveryAddressId,
                        StoreName = o.Store!.LocationName,
                        TimeSlot = TimeSlotStruct.Create(
                            o.TimeSlot!.Date,
                            new TimeSlotRange
                            {
                                Start = new Time
                                {
                                    Minutes = o.TimeSlot!.TimeSlotDefinition!.StartTimeMinutes
                                },
                                End = new Time
                                {
                                    Minutes = o.TimeSlot.TimeSlotDefinition.EndTimeMinutes
                                }
                            }
                        ),
                        Subtotal = o.Subtotal,
                        Discount = o.Discount,
                        Tax = o.Tax,
                        Total = o.Total,
                        Status = o.Status
                    }
            )
            .FirstOrDefaultAsync();

    public async Task<OrderDetailsModel?> GetOrderDetailsAsync(long orderNumber) =>
        await dbContext
            .Orders
            .Include(o => o.Customer)
            .ThenInclude(c => c!.User)
            .Include(o => o.Store)
            .Include(o => o.TimeSlot)
            .Where(o => o.OrderNumber == orderNumber)
            .Select(
                o =>
                    new OrderDetailsModel
                    {
                        Id = o.Id,
                        CustomerId = o.CustomerId,
                        CustomerName = o.Customer!.User!.FirstName,
                        OrderNumber = o.OrderNumber,
                        DeliveryAddressId = o.DeliveryAddressId,
                        StoreId = o.StoreId,
                        StoreName = o.Store!.LocationName,
                        TimeSlot = TimeSlotStruct.Create(
                            o.TimeSlot!.Date,
                            new TimeSlotRange
                            {
                                Start = new Time
                                {
                                    Minutes = o.TimeSlot!.TimeSlotDefinition!.StartTimeMinutes
                                },
                                End = new Time
                                {
                                    Minutes = o.TimeSlot.TimeSlotDefinition.EndTimeMinutes
                                }
                            }
                        ),
                        Subtotal = o.Subtotal,
                        Discount = o.Discount,
                        Tax = o.Tax,
                        Total = o.Total,
                        Status = o.Status
                    }
            )
            .FirstOrDefaultAsync();

    public async Task<IEnumerable<OrderListModel>> GetOrdersAsync()
    => await dbContext.Orders
        .Include(o => o.Customer)
        .ThenInclude(c => c!.User)
        .Include(o => o.Store)
        .Include(o => o.TimeSlot)
        .Select(
                o =>
                    new OrderListModel
                    {
                        Id = o.Id,
                        CustomerId = o.CustomerId,
                        CustomerName = o.Customer!.User!.FirstName,
                        StoreId = o.StoreId,
                        DeliveryAddressId = o.DeliveryAddressId,
                        StoreName = o.Store!.LocationName,
                        TimeSlot = TimeSlotStruct.Create(
                            o.TimeSlot!.Date,
                            new TimeSlotRange
                            {
                                Start = new Time
                                {
                                    Minutes = o.TimeSlot!.TimeSlotDefinition!.StartTimeMinutes
                                },
                                End = new Time
                                {
                                    Minutes = o.TimeSlot.TimeSlotDefinition.EndTimeMinutes
                                }
                            }
                        ),
                        Total = o.Total,
                        Status = o.Status
                    }
            )
            .ToListAsync();

    public async Task<IEnumerable<OrderListModel>> GetOrdersAsync(int customerId) =>
        await dbContext
            .Orders
            .Include(o => o.Customer)
            .ThenInclude(c => c!.User)
            .Include(o => o.Store)
            .Include(o => o.TimeSlot)
            .Where(o => o.Customer!.Id == customerId)
            .Select(
                o =>
                    new OrderListModel
                    {
                        Id = o.Id,
                        CustomerId = o.CustomerId,
                        CustomerName = o.Customer!.User!.FirstName,
                        StoreId = o.StoreId,
                        DeliveryAddressId = o.DeliveryAddressId,
                        StoreName = o.Store!.LocationName,
                        TimeSlot = TimeSlotStruct.Create(
                            o.TimeSlot!.Date,
                            new TimeSlotRange
                            {
                                Start = new Time
                                {
                                    Minutes = o.TimeSlot!.TimeSlotDefinition!.StartTimeMinutes
                                },
                                End = new Time
                                {
                                    Minutes = o.TimeSlot.TimeSlotDefinition.EndTimeMinutes
                                }
                            }
                        ),
                        Total = o.Total,
                        Status = o.Status
                    }
            )
            .ToListAsync();

    public async Task<IEnumerable<OrderLineListModel>> GetOrderLinesAsync(int orderId) =>
        await dbContext
            .OrderLine
            .Where(ol => ol.OrderId == orderId)
            .Select(
                ol =>
                    new OrderLineListModel
                    {
                        OrderId = ol.OrderId,
                        ItemId = ol.ItemId,
                        Quantity = ol.Quantity,
                        Price = ol.Price,
                        Discount = ol.Discount,
                        TotalPrice = ol.TotalPrice,
                    }
            )
            .ToListAsync();

    public async Task<bool> OrderExistsAsync(int orderId) =>
        await dbContext.Orders.AnyAsync(o => o.Id == orderId);

    public async Task<bool> CreateOrderAsync(CreateOrderModel model)
    {
        var newOrder = new Order
        {
            CustomerId = model.CustomerId,
            StoreId = model.StoreId,
            TimeSlotId = model.TimeSlotId,
            DeliveryAddressId = model.DeliveryAddressId,
            OrderType = model.OrderType,
            Status = model.Status
        };

        OrderEntry = await dbContext.Orders.AddAsync(newOrder);

        return OrderEntry.State == EntityState.Added;
    }

    public async Task AddOrderLineAsync(CreateOrderLineModel model)
    {
        var newOrderLine = new OrderLine
        {
            OrderId = model.OrderId,
            ItemId = model.ItemId,
            OrderSubstitutionId = model.OrderSubstitutionId,
            Quantity = model.Quantity,
            Price = model.Price,
            Discount = model.Discount,
            TotalPrice = model.TotalPrice,
        };

        await dbContext.OrderLine.AddAsync(newOrderLine);
    }

    public Task AddOrderLinesAsync(IEnumerable<CreateOrderLineModel> models)
    {
        var newOrderLines = models.Select(
            model =>
                new OrderLine
                {
                    OrderId = model.OrderId,
                    ItemId = model.ItemId,
                    OrderSubstitutionId = model.OrderSubstitutionId,
                    Quantity = model.Quantity,
                    Price = model.Price,
                    Discount = model.Discount,
                    TotalPrice = model.TotalPrice,
                }
        );

        return dbContext.OrderLine.AddRangeAsync(newOrderLines);
    }

    public async Task UpdateOrderAmountAsync(
        int orderId,
        Action<IOrderPriceSummary, IEnumerable<IOrderItemAmounts>> calculateAmounts
    )
    {
        var orderLines = dbContext.OrderLine.Where(ol => ol.OrderId == orderId);
        var order = await dbContext.Orders.FindAsync(orderId);

        if (order is null)
            return;

        calculateAmounts.Invoke(order, orderLines);
    }

    public async Task DeleteOrderAsync(int orderId)
    {
        var order = await dbContext.Orders.FindAsync(orderId);

        if (order is not null)
        {
            dbContext.Orders.Remove(order);
        }
    }

    public async Task DeleteOrderLineAsync(int orderLineId)
    {
        var orderLine = await dbContext.OrderLine.FindAsync(orderLineId);

        if (orderLine is not null)
        {
            dbContext.OrderLine.Remove(orderLine);
        }
    }

    public int? GetPersistedOrderId() =>
        OrderEntry?.State == EntityState.Unchanged ? OrderEntry.Entity.Id : null;

    public OrderDetailsModel? GetPersistedOrderDetails() =>
        OrderEntry is not null && OrderEntry?.State == EntityState.Unchanged
            ? new OrderDetailsModel
            {
                Id = OrderEntry.Entity.Id,
                CustomerId = OrderEntry.Entity.CustomerId,
                OrderNumber = OrderEntry.Entity.OrderNumber,
                StoreId = OrderEntry.Entity.StoreId,
                DeliveryAddressId = OrderEntry.Entity.DeliveryAddressId,
                TimeSlot = new TimeSlotStruct(),
                Subtotal = OrderEntry.Entity.Subtotal,
                Discount = OrderEntry.Entity.Discount,
                Tax = OrderEntry.Entity.Tax,
                Total = OrderEntry.Entity.Total,
                Status = OrderEntry.Entity.Status
            }
            : null;
}
