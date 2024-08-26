using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CadastroDeClientes.Data;
using CadastroDeClientes.Models;

namespace CadastroDeClientes.Controllers
{
    public class ClientesController : Controller
    {
        private readonly Contexto _context;

        public ClientesController(Contexto context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
              return _context.Cliente != null ? 
                          View(await _context.Cliente.ToListAsync()) :
                          Problem("Entity set 'Contexto.Cliente'  is null.");
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cliente == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Cnpj,Segmento,Cep,Cidade,Rua,Bairro,Uf,Ibge")] Cliente cliente, IFormFile Imagem)
        {
            if (ModelState.IsValid)
            {

                if (Imagem != null && Imagem.Length > 0)
                {
                    
                    // Verifica se o arquivo é PNG
                    var extensaoArquivo = Path.GetExtension(Imagem.FileName).ToLower();
                    if (extensaoArquivo != ".png")
                    {
                        ModelState.AddModelError("ImagemCaminho", "Apenas arquivos PNG são permitidos.");
                        return View(cliente);
                    }
                    
                    // Definir o caminho onde a imagem será salva
                    var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens/clientes");
                    var nomeArquivo = Path.GetFileName(Imagem.FileName);
                    var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);
                
                    // Cria a pasta caso não exista
                    if (!Directory.Exists(caminhoPasta))
                    {
                        Directory.CreateDirectory(caminhoPasta);
                    }
                    
                    // Salvar a imagem no diretório especificado
                    using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                    {
                        await Imagem.CopyToAsync(stream);
                    }

                    // Salvar o caminho da imagem no banco de dados
                    cliente.ImagemCaminho = $"/imagens/clientes/{nomeArquivo}";
                }
                
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cliente == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Cnpj,Segmento,Cep,Cidade,Rua,Bairro,Uf,Ibge")] Cliente cliente, IFormFile imagem)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (imagem != null && imagem.Length > 0)
                    {
                        // Verifica se o arquivo é PNG
                        var extensaoArquivo = Path.GetExtension(imagem.FileName).ToLower();
                        if (extensaoArquivo != ".png")
                        {
                            ModelState.AddModelError("ImagemCaminho", "Apenas arquivos PNG são permitidos.");
                            return View(cliente);
                        }

                        // Define o caminho onde a imagem será salva
                        var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens/clientes");
                        var nomeArquivo = Path.GetFileName(imagem.FileName);
                        var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);

                        // Cria a pasta caso não exista
                        if (!Directory.Exists(caminhoPasta))
                        {
                            Directory.CreateDirectory(caminhoPasta);
                        }

                        // Salva a imagem no diretório especificado
                        using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                        {
                            await imagem.CopyToAsync(stream);
                        }

                        // Atualiza o caminho da imagem no cliente
                        cliente.ImagemCaminho = $"/imagens/clientes/{nomeArquivo}";
                    }
                    else
                    {
                        // Caso nenhuma imagem seja enviada, mantenha o caminho atual
                        var clienteAtual = await _context.Cliente.FindAsync(id);
                        cliente.ImagemCaminho = clienteAtual.ImagemCaminho;
                    }
                    
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cliente == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cliente == null)
            {
                return Problem("Entity set 'Contexto.Cliente'  is null.");
            }
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente != null)
            {
                _context.Cliente.Remove(cliente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
          return (_context.Cliente?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
