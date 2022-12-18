using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DataDynamics.App.Database;

internal static class Program
{
    /// <summary>
    ///     해당 애플리케이션의 주 진입점입니다.
    /// </summary>
    [STAThread]
    private static void Main(string[] args)
    {
        string appName = ConfigurationManager.AppSettings["AppName"];
        Console.Out.WriteLine(appName);

        var context = new CustomerRepository();
        var primaryKey = Random.Shared.NextInt64();
        var customer = new Customer { Id = primaryKey, FullName = "Pendori" };
        context.Add(customer);
        context.SaveChanges();

        context.Remove(customer);
        context.SaveChanges();

        ///
        /// ORM으로 호출
        /// 
        foreach (var c in context.Customers)
        {
            Console.Out.WriteLine(c.FullName);
        }

        ///
        /// SQL을 직접 호출
        /// 
        var output1 = context.Customers.FromSql($"select * from customers limit 1").ToList();
        foreach (var c in output1)
        {
            Console.Out.WriteLine(c.FullName);
        }

        ///
        /// 매개변수 전달
        /// 
        var pk = 1;
        var output2 = context.Customers.FromSql($"select * from customers where id = {pk}").ToList();
        foreach (var c in output2)
        {
            Console.Out.WriteLine(c.FullName);
        }

        var customerSqlRepository = new CustomerSqlRepository();
        customerSqlRepository.Insert(1000, "Hello World");
        customerSqlRepository.FindAll();
    }
}