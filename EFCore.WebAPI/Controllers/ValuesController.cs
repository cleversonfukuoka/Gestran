using EFCore.Dominio;
using EFCore.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public readonly ApplicationDBContext _context;
        public ValuesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<List<Fornecedor>>> Get()
        {

            return await _context.Fornecedores.Include(x=>x.Enderecos).ToListAsync();                
        }

        // GET: api/<ValuesController>/nome,cidade,cnpj
        [HttpGet("{fornecedores}")]
        public async Task<ActionResult<List<Fornecedor>>> GetFiltro(string Nome, string Cidade, string CNPJ)
        {

            return await _context.Fornecedores
                .Where(w=>w.Nome.Equals(Nome) || w.CNPJ.Equals(CNPJ))
                .Include(x => x.Enderecos.Where(z=>z.Cidade.Equals(Cidade))).
                ToListAsync();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fornecedor>> Get(int id)
        {
            
            var enderecos = await _context.Enderecos.Where(x=>x.Fornecedor.Id.Equals(id)).ToListAsync(); 
            var fornecedor = await _context.Fornecedores.FindAsync(id);

            if(fornecedor == null)
            {
                return NotFound();
            }
            fornecedor.Enderecos = enderecos;
            return fornecedor;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult<Fornecedor>> Post([FromBody] Fornecedor fornecedor)
        {
            _context.Fornecedores.Add(fornecedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = fornecedor.Id}, fornecedor);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Fornecedor fornecedor)
        {
            if (id != fornecedor.Id)
            {
                return BadRequest();
            }

            var enderecos = new List<Endereco>();
            enderecos = (List<Endereco>)fornecedor.Enderecos;

            _context.Entry(fornecedor).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                _context.Entry(enderecos).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            { 
                if (!fornecedor.Id.Equals(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var fornecedor = await _context.Fornecedores.FindAsync(id);
            if (fornecedor == null)
            {
                return NotFound();
            }
            _context.Fornecedores.Remove(fornecedor);
            await _context.SaveChangesAsync();

            return NoContent(); 
        }

        private static Fornecedor FornecedorToDTO(Fornecedor fornecedor) => new Fornecedor
        {
            Id = fornecedor.Id,
            Nome = fornecedor.Nome,
            CNPJ = fornecedor.CNPJ,
            Telefone = fornecedor.Telefone,
            Email = fornecedor.Email,
            Enderecos = fornecedor.Enderecos
        };
    }
}
