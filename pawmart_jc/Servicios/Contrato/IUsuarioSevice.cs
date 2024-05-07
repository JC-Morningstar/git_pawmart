
    using Microsoft.EntityFrameworkCore;
    using pawmart_jc.Models;
    namespace pawmart_jc.Servicios.Contrato
{
        public interface IUsuarioService
        {
           Task<Cliente> GetCliente(string CorreoElectronico, string Contraseña);
            Task<Cliente> SaveCliente(Cliente modelo);

        }
    }

