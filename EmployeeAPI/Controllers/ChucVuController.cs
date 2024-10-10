using EmployeeAPI.Interface.ModelInterface;
using EmployeeAPI.Model.DTOAdd;
using EmployeeAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize (Roles=Roles.Admin)]
    [ApiController]
    public class ChucVuController : ControllerBase
    {
        private readonly IChucVu _chucvu;
        public ChucVuController(IChucVu chucvu)
        {
            _chucvu = chucvu;
        }
        [HttpGet]
        [Route("GetAllChucVu")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllChucVu()
        {
            var getlist = await _chucvu.GetallChucVu();
            if (getlist == null)
            {
                return NotFound();
            }
            return Ok(getlist);
        }
        [HttpGet]
        [Route("{id:int}",Name ="GetChucVuById")]
        public async Task<IActionResult> GetChucVuById([FromRoute]int id)
        {
            if(id==0)
            {
                return NotFound();
            }
            var get = await _chucvu.GetbyId(id);
            if (get == null)
            {
                return NotFound();
            }
            return Ok(get);
        }
        [HttpGet]
        [Route("GetNhanVienByChucVuId/{id}")]
        public async Task<IActionResult> GetNhanVienByChucVuId([FromRoute] int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var get = await _chucvu.GetNhanVienByChucVuId(id);
            if (get == null)
            {
                return NotFound();
            }
            return Ok(get);
        }
      
        [HttpPost]
        [Route("CreateChucVu")]
        public async Task <IActionResult> CreateChucVu([FromBody] DTOAddChucVu model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var get = await _chucvu.Add(model);
            if (!get)
            {
                return BadRequest();
            }
            return Ok(get);
        }
        [HttpPut]
        [Route("{id:int}",Name ="UpdateChucVu")]
        public async Task<IActionResult> UpdateChucVu([FromRoute]int id,[FromBody] DTOAddChucVu model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var get =await _chucvu.Update(id,model);
            if (!get)
            {
                return BadRequest();
            }
            return Ok(get);
        }
        [HttpDelete]
        [Route("{id:int}", Name = "DeleteChucVu")]
        public async Task<IActionResult> DeleteChucVu([FromRoute] int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var get = await _chucvu.Delete(id);
            if (!get)
            {
                return BadRequest();
            }
            return Ok(get);
        }
    }
}
