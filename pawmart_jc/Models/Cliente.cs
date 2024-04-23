using System;
using System.Collections.Generic;

namespace pawmart_jc.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? CorreoElectronico { get; set; }
        public string? Contraseña { get; set; }
        public string? DireccionEnvio { get; set; }
        public string? OtrosDatosContacto { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
