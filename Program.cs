using JustTest.Controllers;
using JustTest.Middlewaresa;
using Serilog;
namespace JustTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

            var app = builder.Build();

            var middlewareList = new List<Type>
            {
                typeof(ErrorHandlingMiddleware),
                typeof(Middleware2),
                typeof(Middleware3),
                typeof(Middleware4),
                typeof(Middleware5)
            };

            var random = new Random();

            var selectedMiddlewareType = middlewareList[random.Next(middlewareList.Count)];

            // С вероятностью 50/50 решаем, будет ли выбранный middleware обработчиком ошибок
            bool shouldHandleErrors = random.NextDouble() < 0.5;

            // Создаем экземпляр выбранного middleware с учетом его поведения
            MiddlewareBase selectedMiddlewareInstance;

            if (shouldHandleErrors)
            {
                // Если обработчик ошибок, создаем экземпляр с включением try-catch
                if (selectedMiddlewareType == typeof(ErrorHandlingMiddleware))
                {
                    selectedMiddlewareInstance = (MiddlewareBase)Activator.CreateInstance(selectedMiddlewareType, new object[] { null });
                    selectedMiddlewareInstance.HasTryCatch = true;
                }
                else
                {
                    // Если это не ErrorHandlingMiddleware, создаем его с try-catch
                    selectedMiddlewareInstance = new ErrorHandlingMiddleware(null)
                    {
                        HasTryCatch = true
                    };
                }
                Log.Information($"Выбран middleware как обработчик ошибок: {selectedMiddlewareType.Name}");
            }
            else
            {
                // Если просто обычный response, создаем экземпляр выбранного middleware
                selectedMiddlewareInstance = (MiddlewareBase)Activator.CreateInstance(selectedMiddlewareType, new object[] { null });
                Log.Information($"Выбран middleware без обработки ошибок: {selectedMiddlewareType.Name}");
            }

            // Регистрация выбранного middleware первым
            app.UseMiddleware(selectedMiddlewareType);

            // Регистрация остальных middleware
            foreach (var middlewareType in middlewareList.Where(t => t != selectedMiddlewareType))
            {
                app.UseMiddleware(middlewareType);
            }


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


            Log.Information($"Выбран middleware с ID: {selectedMiddlewareInstance.id}");
            
            app.Run();
        }
    }
}
