using EmployeeAPI.Interface.ModelInterface;
using EmployeeAPI.Model.DTOAdd;
using EmployeeAPI.Model.DTOUpdate;
using EmployeeAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{

    [Route("api/[controller]")]
    [Authorize(Roles = Roles.User)]
    [ApiController]
    public class ChamCongController : ControllerBase
    {
        private readonly IChamCong _chamcong;
        
        public ChamCongController(IChamCong chamcong)
        {
            _chamcong = chamcong;
        }
       

        [HttpGet]
        [Route("GetAllChamCong")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllChamCong()
        {
            var getlist = await _chamcong.GetAllLichChamCong();
            if (getlist == null)
            {
                return NotFound();
            }     
        
            
            return Ok(getlist);
        }
        [HttpGet]
        [Route("{id:int}", Name = "GetChamCongById")]
        public async Task<IActionResult> GetChamCongById([FromRoute] int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var get =await _chamcong.GetbyId(id);
            if ( get is null)
            {
                return NotFound();
            }
             
            return Ok(get);
        }

        [HttpPost]
        [Route("CreateChamCong")]
        public async Task<IActionResult> CreateChamCong([FromBody] DTOAddChamCong model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var get =await _chamcong.Create(model);
            
            if (get == false)
            {
                return BadRequest();
            }
            return Ok(get);
        }
       
        [HttpPut]
        [Route("{id:int}", Name = "UpdateChamCong")]
        public async Task<IActionResult> UpdateChamCong([FromRoute] int id, [FromBody] DTOUpdateChamCong model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var get = await _chamcong.Edit(id, model);
            if (get == false)
            {
                return BadRequest();
            }
            return Ok(get);
        }
        [HttpDelete]
        [Route("{id:int}", Name = "DeleteChamCong")]
        public async Task<IActionResult> DeleteChamCong([FromRoute] int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var get =await _chamcong.Delete(id);
            if (get == false)
            {
                return BadRequest();
            }
            return Ok(get);
        }
    }
}
