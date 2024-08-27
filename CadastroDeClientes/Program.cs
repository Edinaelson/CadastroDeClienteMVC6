using CadastroDeClientes.Data;
using Microsoft.EntityFrameworkCore;

namespace CadastroDeClientes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
           //configurando agrupamento de arquivos staticos como css e javaScript

           builder.Services.AddWebOptimizer(pipeline =>
               {
                   //nome do arquivo, arquivo01 + arquivo02
                   pipeline.AddCssBundle("/css/bundle.css", "/lib/bootstrap/dist/css/bootstrap.css", "/css/site.css");
                   pipeline.AddJavaScriptBundle("/js/bundle.js", "/lib/bootstrap/dist/js/bootstrap.js", "/js/site.js");

                   pipeline.MinifyCssFiles();
                   pipeline.MinifyJsFiles();
               });
           

            //configurando conexao com o banco de dados
            builder.Services.AddDbContext<Contexto>
                (options => options.UseMySql(
                    "server=localhost;initial catalog=CLIENTES;uid=root;pwd=1234",
                    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.38-mysql")));

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            //usando arquivos estaticos
            app.UseWebOptimizer();
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