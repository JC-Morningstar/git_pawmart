using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using pawmart_jc.Models;
using pawmart_jc.Servicios.Contrato;

namespace pawmart_jc.Servicios.Implementacion
{
    public class UsuarioService : IUsuarioService
    {
        private readonly Pawmart_BDContext _dbContext;

        public UsuarioService(Pawmart_BDContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Cliente> GetCliente(string CorreoElectronico, string Contraseña)
        {
            // Verifica que los parámetros no sean nulos
            if (CorreoElectronico == null || Contraseña == null)
            {
                return null; // O maneja el error de alguna otra forma
            }

            // Busca al cliente por correo electrónico
            Cliente cliente_encontrado = await _dbContext.Clientes.FirstOrDefaultAsync(u => u.CorreoElectronico == CorreoElectronico);

            return cliente_encontrado;
        }

        public async Task<Cliente> SaveCliente(Cliente modelo)
        {
            _dbContext.Clientes.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return modelo;
        }
    }
}
