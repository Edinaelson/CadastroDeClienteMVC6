using CadastroDeClientes.Data;
using Microsoft.EntityFrameworkCore;

namespace CadastroDeClientes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
           

            //configurando conexao com o banco de dados
            builder.Services.AddDbContext<Contexto>
                (options => options.UseMySql(
                    "server=localhost;initial catalog=CLIENTES;uid=root;pwd=1234",
                    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.38-mysql")));

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            

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
                pattern: "{controller=Clientes}/{action=Index}/{id?}");

            app.Run();
        }
    }
}