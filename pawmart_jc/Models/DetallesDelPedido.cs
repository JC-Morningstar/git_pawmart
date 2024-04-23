using System;
using System.Collections.Generic;

namespace pawmart_jc.Models
{
    public partial class DetallesDelPedido
    {
        public int Id { get; set; }
        public int? IdPedido { get; set; }
        public int? IdProducto { get; set; }
        public int? Cantidad { get; set; }
        public decimal? PrecioUnitario { get; set; }

        public virtual Pedido? IdPedidoNavigation { get; set; }
        public virtual Producto? IdProductoNavigation { get; set; }
    }
}
