using System;
using System.Collections.Generic;

namespace pawmart_jc.Models
{
    public partial class Pedido
    {
        public Pedido()
        {
            DetallesDelPedidos = new HashSet<DetallesDelPedido>();
        }

        public int Id { get; set; }
        public int? IdCliente { get; set; }
        public DateTime? FechaHoraPedido { get; set; }

        public virtual Cliente? IdClienteNavigation { get; set; }
        public virtual ICollection<DetallesDelPedido> DetallesDelPedidos { get; set; }
    }
}
