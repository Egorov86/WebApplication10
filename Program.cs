using WebApplication10.Data;
using WebApplication10.Services;
using WebApplication10.Services.Implementations;

namespace WebApplication10
{
    public class Program
    {
        public static void Main(string[] args){
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<DatabaseContext>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            //builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            var app = builder.Build();

            app.MapControllerRoute("default", "{controller=Employee}/{action=Index}/{id?}");
            //app.MapControllerRoute("default", "{controller=Users}/{action=Index}/{id?}");

            app.UseStaticFiles();
            app.Run();
        }
    }
}
