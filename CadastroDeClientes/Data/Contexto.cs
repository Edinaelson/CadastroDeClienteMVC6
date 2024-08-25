using CadastroDeClientes.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroDeClientes.Data
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options)
            : base(options){ }

        public DbSet<Cliente> Cliente { get; set;}
    }
}
