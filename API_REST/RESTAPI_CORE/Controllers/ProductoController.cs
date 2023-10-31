using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using RESTAPI_CORE.Models;
using System.Data;
using System.Data.SqlClient;

namespace RESTAPI_CORE.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly string cadenaSQL;
        public ProductoController(IConfiguration config) {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }


        //LISTAR PRODUCTOS
       
        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista() { 
            
            List<Producto> lista = new List<Producto>();

            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("spConsultarProducto", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {

                        while (rd.Read())
                        {

                            lista.Add(new Producto
                            {
                                IdProducto = Convert.ToInt32(rd["IdProducto"]),
                                Nombre = rd["Nombre"].ToString(),
                                Marca = rd["Marca"].ToString(),
                                CostoUnitario = Convert.ToInt32(rd["CostoUnitario"]),
                                Cantidad = Convert.ToInt32(rd["Cantidad"]),
                                PrecioVenta = Convert.ToInt32(rd["PrecioVenta"])
                                //FechaCompra = Convert.ToDateTime(rd["FechaCompra"].ToString())
                            });
                        }

                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" , lista = lista });
            }
            catch(Exception error) {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message,  response = lista });

            }
        }

        [HttpGet]
        [Route("Obtener/{idProducto:int}")]
        public IActionResult Obtener(int idProducto)
        {

            List<Producto> lista = new List<Producto>();
            Producto oproducto = new Producto();

            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("spConsultarProducto", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {

                        while (rd.Read())
                        {

                            lista.Add(new Producto
                            {
                                IdProducto = Convert.ToInt32(rd["IdProducto"]),
                                Nombre = rd["Nombre"].ToString(),
                                Marca = rd["Marca"].ToString(),
                                CostoUnitario = Convert.ToInt32(rd["CostoUnitario"].ToString()),
                                Cantidad = Convert.ToInt32(rd["Cantidad"]),
                                PrecioVenta = Convert.ToInt32(rd["PrecioVenta"]),
                                FechaCompra = Convert.ToDateTime(rd["FechaCompra"].ToString())
                            });
                        }

                    }
                }

                oproducto = lista.Where(item => item.IdProducto == idProducto).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", objeto = oproducto });
            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = oproducto });

            }
        }

        //GUARDAR PRODUCTOS
        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Producto objeto)
        {
            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("spRegistrarProducto", conexion);
                    cmd.Parameters.AddWithValue("Nombre", objeto.Nombre);
                    cmd.Parameters.AddWithValue("Marca", objeto.Marca);
                    cmd.Parameters.AddWithValue("CostoUnitario", objeto.CostoUnitario);
                    cmd.Parameters.AddWithValue("Cantidad", objeto.Cantidad);
                    cmd.Parameters.AddWithValue("PrecioVenta", objeto.PrecioVenta);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "agregado" });
            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });

            }
        }

        //MODIFICAR PRODUCTOS
        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Producto objeto)
        {
            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("spEditarProducto", conexion);
                    cmd.Parameters.AddWithValue("IdProducto", objeto.IdProducto == 0 ? DBNull.Value : objeto.IdProducto);
                    cmd.Parameters.AddWithValue("Nombre", objeto.Nombre is null ? DBNull.Value : objeto.Nombre);
                    cmd.Parameters.AddWithValue("Marca", objeto.Marca is null ? DBNull.Value : objeto.Marca);
                    cmd.Parameters.AddWithValue("CostoUnitario", objeto.CostoUnitario == 0 ? DBNull.Value : objeto.CostoUnitario);
                    cmd.Parameters.AddWithValue("Cantidad", objeto.Cantidad == 0 ? DBNull.Value : objeto.Cantidad);
                    cmd.Parameters.AddWithValue("PrecioVenta", objeto.PrecioVenta == 0 ? DBNull.Value : objeto.PrecioVenta);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Modificado" });
            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });

            }
        }

        //ELIMINAR PRODUCTOS
        [HttpDelete]
        [Route("Eliminar/{IdProducto:int}")]
        public IActionResult Eliminar(int IdProducto)
        {
            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("spEliminarProducto", conexion);
                    cmd.Parameters.AddWithValue("IdProducto", IdProducto);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Borrado con éxito" });
            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });

            }
        }


    }
}
