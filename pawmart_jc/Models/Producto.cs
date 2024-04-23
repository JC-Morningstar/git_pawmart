using System;
using System.Collections.Generic;

namespace pawmart_jc.Models
{
    public partial class Producto
    {
        public Producto()
        {
            DetallesDelPedidos = new HashSet<DetallesDelPedido>();
        }

        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal? Precio { get; set; }
        public int? Existencia { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? TipoMascota { get; set; }
        public byte[]? Imagen { get; set; }
        public string? OtrasCaracteristicas { get; set; }

        public virtual ICollection<DetallesDelPedido> DetallesDelPedidos { get; set; }
    }
}
