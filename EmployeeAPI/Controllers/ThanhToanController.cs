using EmployeeAPI.Interface.ModelInterface;
using EmployeeAPI.Model.DTOAdd;
using EmployeeAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Admin)]
    [ApiController]
    public class ThanhToanController : ControllerBase
    {
        private readonly ITHanhToan _thanhtoan;
        public ThanhToanController(ITHanhToan thanhtoan)
        {
            _thanhtoan = thanhtoan;
        }
        [HttpGet]
        [Route("GetAllThanhToan")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllThanhToan()
        {
            var getlist = await _thanhtoan.GetAllThanhToan();
            if (getlist == null)
            {
                return NotFound();
            }
            return Ok(getlist);
        }
        [HttpGet]
        [Route("{id:int}", Name = "GetThanhToanByNhanVienId")]
        public async Task<IActionResult> GetThanhToanByNhanVienId([FromRoute] int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var get = await _thanhtoan.GetbyNhanVienId(id);
            if (get == null)
            {
                return NotFound();
            }
            return Ok(get);
        }
        [HttpPost]
        [Route("CreateThanhToan")]
        public async Task<IActionResult> CreateThanhToan([FromBody] DTOAddThanhToan model)
        {
            if (model == null)
            {
                return BadRequest("model sai");
            }
            var get =await _thanhtoan.Add(model);
            if (get == false)
            {
                return BadRequest();
            }
            return Ok(get);
        }
        [HttpPut]
        [Route("UpdateTHanhToan")]
        public async Task <IActionResult> UpdateTHanhToan([FromBody] DTOAddThanhToan model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var get =await _thanhtoan.Update(model);
            if (get == false)
            {
                return BadRequest();
            }
            return Ok(get);
        }
        [HttpDelete]
        [Route("{id:int}", Name = "DeleteThanhToan")]
        public async Task<IActionResult> DeleteThanhToan([FromRoute] int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var get =await _thanhtoan.Delete(id);
            if (get == false)
            {
                return BadRequest();
            }
            return Ok(get);
        }
    }
}
