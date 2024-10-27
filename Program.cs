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

            // � ������������ 50/50 ������, ����� �� ��������� middleware ������������ ������
            bool shouldHandleErrors = random.NextDouble() < 0.5;

            // ������� ��������� ���������� middleware � ������ ��� ���������
            MiddlewareBase selectedMiddlewareInstance;

            if (shouldHandleErrors)
            {
                // ���� ���������� ������, ������� ��������� � ���������� try-catch
                if (selectedMiddlewareType == typeof(ErrorHandlingMiddleware))
                {
                    selectedMiddlewareInstance = (MiddlewareBase)Activator.CreateInstance(selectedMiddlewareType, new object[] { null });
                    selectedMiddlewareInstance.HasTryCatch = true;
                }
                else
                {
                    // ���� ��� �� ErrorHandlingMiddleware, ������� ��� � try-catch
                    selectedMiddlewareInstance = new ErrorHandlingMiddleware(null)
                    {
                        HasTryCatch = true
                    };
                }
                Log.Information($"������ middleware ��� ���������� ������: {selectedMiddlewareType.Name}");
            }
            else
            {
                // ���� ������ ������� response, ������� ��������� ���������� middleware
                selectedMiddlewareInstance = (MiddlewareBase)Activator.CreateInstance(selectedMiddlewareType, new object[] { null });
                Log.Information($"������ middleware ��� ��������� ������: {selectedMiddlewareType.Name}");
            }

            // ����������� ���������� middleware ������
            app.UseMiddleware(selectedMiddlewareType);

            // ����������� ��������� middleware
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


            Log.Information($"������ middleware � ID: {selectedMiddlewareInstance.id}");
            
            app.Run();
        }
    }
}
