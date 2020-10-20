using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using web_api_x.Models;
namespace web_api_x.Controllers{
    [Route("api/[controller]")]
    public class EmpleadosController : Controller{
        private Conexion dbConexion;
        public EmpleadosController(){
            
            dbConexion=Conectar.Create();
        }
        [HttpGet]
        public ActionResult Get(){
            return Ok(dbConexion.Empleados.ToArray());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id){

            var empleados = await dbConexion.Empleados.FindAsync(id);
            if(empleados !=null){
                return Ok(empleados);


            }else{
                return NotFound();
            }

        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Empleados empleados){

            if(ModelState.IsValid){

                dbConexion.Empleados.Add(empleados);
                await dbConexion.SaveChangesAsync();
                return Ok(empleados);

            }else{
                
                return NotFound();

            }

        }
        [HttpPut]
        public async Task<ActionResult> put([FromBody] Empleados empleados){
            var v_empleados = dbConexion.Empleados.SingleOrDefault(a => a.id_empleado == empleados.id_empleado);
            if(v_empleados !=null && ModelState.IsValid){

                dbConexion.Entry(v_empleados).CurrentValues.SetValues(empleados);
                await dbConexion.SaveChangesAsync();
                return Ok();

            }else{

                return NotFound();
            }
        }

         [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id){
        var empleados = dbConexion.Empleados.SingleOrDefault(a => a.id_empleado == id);
        if(empleados !=null){

                dbConexion.Empleados.Remove(empleados);
                await dbConexion.SaveChangesAsync();
                return Ok();


        }else{

            return NotFound();

        }

        }
   

      
        
    }


}