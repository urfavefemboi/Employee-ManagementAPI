using EmployeeAPI.Interface.ModelInterface;
using EmployeeAPI.Model.DTOAdd;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhongBanController : ControllerBase
    {
        private readonly IPhongBan _phongban;
        public PhongBanController(IPhongBan phongban)
        {
            _phongban = phongban;
        }
        [HttpGet]
        [Route("GetAllPhongBan")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllPhongBan()
        {
            var getlist = await _phongban.GetAllPhongBan();
            if (getlist == null)
            {
                return NotFound();
            }
            return Ok(getlist);
        }
        [HttpGet]
        [Route("{id:int}", Name = "GetPhongBanById")]
        public async Task<IActionResult> GetPhongBanById([FromRoute] int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var get = await _phongban.GetbyId(id);
            if (get == null)
            {
                return NotFound();
            }
            return Ok(get);
        }
        [HttpGet]
        [Route("GetNhanVienbyPhongBanId")]
        public async Task<IActionResult> GetNhanVienbyPhongBanId( int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var get = await _phongban.GetAllNhanVienById(id);
            if (get == null)
            {
                return NotFound();
            }
            return Ok(get);
        }

        [HttpGet]
        [Route("GetSoLuongNhanVienbyPhongBan")]
        public async Task<IActionResult> GetSoLuongNhanVienbyPhongBan()
        {
            var get = await _phongban.GetSoLuongNhanVienTungPhongBan();
            if (get == null)
            {
                return NotFound();
            }
            return Ok(get);
        }
        [HttpPost]
        [Route("CreatePhongBan")]
        public async Task<IActionResult> CreatePhongBan([FromBody] DTOAddPhongBan model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var get =await _phongban.Add(model);
            if (get == false)
            {
                return BadRequest();
            }
            return Ok(get);
        }
        [HttpPut]
        [Route("{id:int}", Name = "UpdatePhongBan")]
        public async Task <IActionResult> UpdatePhongBan([FromRoute] int id, [FromBody] DTOAddPhongBan model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var get =await _phongban.Update(id, model);
            if (get == false)
            {
                return BadRequest();
            }
            return Ok(get);
        }
        [HttpDelete]
        [Route("{id:int}", Name = "DeletePhongBan")]
        public async Task <IActionResult> DeletePhongBan([FromRoute] int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var get =await _phongban.Delete(id);
            if (get == false)
            {
                return BadRequest();
            }
            return Ok(get);
        }
    }
}

