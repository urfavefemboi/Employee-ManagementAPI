using EmployeeAPI.Interface.ModelInterface;
using EmployeeAPI.Model.DTOAdd;
using EmployeeAPI.Model.DTOUpdate;
using EmployeeAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Admin)]
    [ApiController]
    public class HopDongController : ControllerBase
    {
        private readonly IHopDong _hopdong;
        public HopDongController(IHopDong hopdong)
        {
            _hopdong = hopdong;
        }
        [HttpGet]
        [Route("GetAllHopDong")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllHopDong(bool status)
        {
            var getlist = await _hopdong.GetAllByStatus(status);
            if (getlist == null)
            {
                return NotFound();
            }
            return Ok(getlist);
        }
        [HttpGet]
        [Route("{id:int}", Name = "GetHopDongById")]
        public async Task<IActionResult> GetHopDongById([FromRoute] int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var get = await _hopdong.GetbyId(id);
            if (get == null)
            {
                return NotFound();
            }
            return Ok(get);
        }
        [HttpGet]
        [Route("GetHopDongByNhanVien")]
        public async Task<IActionResult> GetHopDongByNhanVien()
        {       
            var get = await _hopdong.GetHopDongByNhanVien();
            if (!get.Any())
            {
                return NotFound();
            }
            return Ok(get);
        }
        [HttpPost]
        [Route("CreateHopDong")]
        public async Task<IActionResult> CreateHopDong([FromBody] DTOAddHopDOng model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var get =await _hopdong.AddHopDong(model);
            if (get == false)
            {
                return BadRequest();
            }
            return Ok(get);
        }
        [HttpPut]
        [Route("{id:int}", Name = "UpdateHopDong")]
        public async Task<IActionResult> UpdateHopDong([FromRoute] int id, [FromBody] DTOUpdateHopdong model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var get =await _hopdong.UpdateHopDong(id, model);
            if (get == false)
            {
                return BadRequest();
            }
            return Ok(get);
        }
        [HttpDelete]
        [Route("{id:int}", Name = "DeleteHopDong")]
        public async Task<IActionResult> DeleteHopDong([FromRoute] int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var get =await _hopdong.DeleteHopDong(id);
            if (get == false)
            {
                return BadRequest();
            }
            return Ok(get);
        }
    }
}
