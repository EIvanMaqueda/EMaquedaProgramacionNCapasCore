using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    public class AseguradoraController : Controller
    {
        [HttpGet]
        [Route("api/Aseguradora/GetAll")]
        public ActionResult GetAll()
        {
            
            ML.Result result = BL.Aseguradora.GetAllAseguradora();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }

        }

        [HttpPost]
        [Route("api/Aseguradora/Add")]
        public ActionResult Add([FromBody] ML.Aseguradora aseguradora)
        {

            ML.Result result = BL.Aseguradora.AddAseguradora(aseguradora);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }

        }


        [HttpGet]
        [Route("api/Aseguradora/GetByid/{IdAseguradora}")]
        public ActionResult GetById(int IdAseguradora)
        {
           
            ML.Result result = BL.Aseguradora.GetByIdAseguradora(IdAseguradora);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }

        }

        [HttpGet]
        [Route("api/Aseguradora/Delete/{IdAseguradora}")]
        public ActionResult Delete(int IdAseguradora)
        {
            ML.Result result = BL.Aseguradora.DeleteAseguradora(IdAseguradora);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }

        }

        [HttpPost]
        [Route("api/Aseguradora/Update")]
        public ActionResult Update([FromBody] ML.Aseguradora aseguradora)
        {

            ML.Result result = BL.Aseguradora.UpdateAseguradora(aseguradora);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

    }
}
