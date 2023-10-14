using lab2.Models;
using Microsoft.EntityFrameworkCore;

namespace lab2;

public static class Program
{
    private static void Main()
    {
        using var context = new AcmeDataContext();
        SelectOneRelation(context);
        SelectOneRelationWithFilter(context, "Company1");
        SelectManyRelationWithAggregation(context);
        SelectOneToManyRelationWithJoin(context);
        SelectOneToManyRelationWithJoinAndFilter(context, 3, new DateTime(2022, 5, 10));
        InsertOneRelation(context);
        InsertManyRelation(context);
        DeleteOneRelation(context, 1);
        DeleteManyRelation(context, 5);
        UpdateWithCondition(context, "Company10");
    }

    private static void SelectOneRelation(AcmeDataContext context)
    {
        var customers = context.Customers.ToList();

        foreach (var customer in customers)
        {
            Console.WriteLine($"ID: {customer.CustomerId}, Название компании: {customer.CompanyName}");
        }
    }

    private static void SelectOneRelationWithFilter(AcmeDataContext context, string filterString)
    {
        var filteredCustomers = context.Customers
            .Where(customer => customer.CompanyName.StartsWith(filterString))
            .ToList();

        foreach (var customer in filteredCustomers)
        {
            Console.WriteLine($"ID: {customer.CustomerId}, Название компании: {customer.CompanyName}");
        }
    }
    
    private static void SelectManyRelationWithAggregation(AcmeDataContext context)
    {
        var orderAverages = context.OrderDetails
            .Join(context.Furnitures,
                orderDetail => orderDetail.FurnitureId,
                furniture => furniture.FurnitureId,
                (orderDetail, furniture) => new { orderDetail, furniture })
            .GroupBy(joinResult => joinResult.orderDetail.Order.CustomerId)
            .Select(group => new
            {
                CustomerID = group.Key,
                AveragePrice = group.Average(joinResult => joinResult.furniture.Price)
            })
            .ToList();

        foreach (var result in orderAverages)
        {
            Console.WriteLine($"CustomerID: {result.CustomerID}, AveragePrice: {result.AveragePrice}");
        }
    }
    
    private static void SelectOneToManyRelationWithJoin(AcmeDataContext context)
    {
        var customerOrders = context.Customers
            .Join(
                context.Orders,
                customer => customer.CustomerId,
                order => order.CustomerId,
                (customer, order) => new
                {
                    CustomerName = customer.CompanyName,
                    OrderDate = order.OrderDate
                })
            .ToList();

        foreach (var entry in customerOrders)
        {
            Console.WriteLine($"Customer Name: {entry.CustomerName}, Order Date: {entry.OrderDate}");
        }
    }
    
    private static void SelectOneToManyRelationWithJoinAndFilter(AcmeDataContext context, int targetCustomerId, DateTime targetDate)
    {
        var customerOrders = context.Customers
            .Where(customer => customer.CustomerId == targetCustomerId)
            .SelectMany(customer => customer.Orders)
            .Where(order => order.OrderDate >= targetDate)
            .ToList();

        foreach (var order in customerOrders)
        {
            Console.WriteLine($"Order ID: {order.OrderId}, Order Date: {order.OrderDate}");
        }
    }
    
    private static void InsertOneRelation(AcmeDataContext context)
    {
        var newCustomer = new Customer
        {
            CompanyName = "Новая Компания",
            RepresentativeLastName = "Фамилия",
            RepresentativeFirstName = "Имя",
            RepresentativeMiddleName = "Отчество",
            PhoneNumber = "123-456-7890",
            Address = "Адрес"
        };

        context.Customers.Add(newCustomer);
        context.SaveChanges(); 
    }
    
    private static void InsertManyRelation(AcmeDataContext context)
    {
        var newOrder = new Order
        {
            OrderDate = DateTime.Now,
            CustomerId = 10,
            SpecialDiscount = (decimal)0.1, 
            IsCompleted = false,
            ResponsibleEmployeeId = 20 
        };

        var orderDetail1 = new OrderDetail
        {
            FurnitureId = 30, 
            Quantity = 2 
        };

        var orderDetail2 = new OrderDetail
        {
            FurnitureId = 50, 
            Quantity = 1 
        };

        newOrder.OrderDetails.Add(orderDetail1);
        newOrder.OrderDetails.Add(orderDetail2);

        context.Orders.Add(newOrder);

        context.SaveChanges();
    }
    
    private static void DeleteOneRelation(AcmeDataContext context, int customerId)
    {
        var customerToDelete = context.Customers
            .Include(c => c.Orders)
            .ThenInclude(o => o.OrderDetails) // Включите связанные записи OrderDetails
            .SingleOrDefault(c => c.CustomerId == customerId);

        if (customerToDelete == null) return;
        context.OrderDetails.RemoveRange(
            customerToDelete.Orders.SelectMany(o => o.OrderDetails));

        context.Orders.RemoveRange(customerToDelete.Orders);

        context.Customers.Remove(customerToDelete);

        context.SaveChanges();
    }
    
    private static void DeleteManyRelation(AcmeDataContext context, int orderId)
    {
        var orderToDelete = context.Orders
            .Include(o => o.OrderDetails)
            .SingleOrDefault(o => o.OrderId == orderId);

        if (orderToDelete == null) return;
        context.OrderDetails.RemoveRange(orderToDelete.OrderDetails);
        
        context.Orders.Remove(orderToDelete);

        context.SaveChanges();
    }
    
    private static void UpdateWithCondition(AcmeDataContext context, string targetCompanyName)
    {
        var customersToUpdate = context.Customers
            .Where(customer => customer.CompanyName == targetCompanyName)
            .ToList();

        customersToUpdate.ForEach(customer =>
        {
            customer.RepresentativeLastName = "Новая Фамилия"; 
            customer.RepresentativeFirstName = "Новое Имя";
            customer.RepresentativeMiddleName = "Новое Отчество";
            customer.PhoneNumber = "Новый Телефон";
            customer.Address = "Новый Адрес";
        });

        context.SaveChanges();
    }
}