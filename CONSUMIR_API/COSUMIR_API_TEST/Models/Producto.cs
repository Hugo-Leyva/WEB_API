namespace COSUMIR_API_TEST.Models
{
    public class Producto
    {
        public int? IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public int? CostoUnitario { get; set; }
        public int? Cantidad { get; set; }
        public int PrecioVenta { get; set; }
        public DateTime? FechaCompra { get; set; }
    }
}
