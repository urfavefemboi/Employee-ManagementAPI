using EmployeeAPI.Interface.ModelInterface;
using EmployeeAPI.Model.Domain;
using EmployeeAPI.Model.DTOAdd;
using EmployeeAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
   [Authorize(Roles = Roles.Admin)]
    [ApiController]
    public class LuongController : ControllerBase
    {
        private readonly ILuong _luong;
        public LuongController(ILuong luong)
        {
            _luong = luong;
        }
        [HttpGet]
        [Route("GetAllLuong")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllLuong()
        {
            var getlist = await _luong.GetallLuong();
            if(getlist.IsNullOrEmpty())
            {
                return NotFound();
            }
            return Ok(getlist);
        }
        [HttpGet]
        [Route("{id:int}", Name = "GetLuongById")]
        public async Task<IActionResult> GetLuongById([FromRoute] int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var get = await _luong.GetbyId(id);
            if (get == null)
            {
                return NotFound();
            }
            return Ok(get);
        }
        [HttpGet]
        [Route("GetNhanVienGroupByLuong")]
        public async Task<IActionResult> GetNhanVienGroupByLuongId()
        {
            
            var get = await _luong.GetNhanVienGrouByMucLuong();
            if (get.IsNullOrEmpty())
            {
                return NotFound();
            }
            return Ok(get);
        }
        [HttpPost]
        [Route("CreateLuong")]
        public async Task<IActionResult> CreateLuong([FromBody] DTOAddLuong model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var get =await _luong.Add(model);
            if (!get)
            {
                return BadRequest();
            }
            return Ok(get);
        }
        [HttpPut]
        [Route("{id:int}", Name = "UpdateLuong")]
        public async Task<IActionResult> UpdateLuong([FromRoute] int id, [FromBody] DTOAddLuong model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var get =await _luong.Update(id, model);
            if (!get)
            {
                return BadRequest();
            }
            return Ok(get);
        }
        [HttpDelete]
        [Route("{id:int}", Name = "DeleteLuong")]
        public async Task<IActionResult> DeleteLuong([FromRoute] int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var get =await _luong.Delete(id);
            if (!get)
            {
                return BadRequest();
            }
            return Ok(get);
        }
    }
}
