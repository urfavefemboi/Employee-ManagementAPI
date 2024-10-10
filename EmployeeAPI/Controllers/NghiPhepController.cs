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
    public class NghiPhepController : ControllerBase
    {
        private readonly INghiPhep _nghiphep;
        public NghiPhepController(INghiPhep nghiphep)
        {
            _nghiphep = nghiphep;
        }

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAll()
        {
            var getlist = await _nghiphep.GetAllNghiPhep();
            if (getlist == null)
            {
                return NotFound();
            }
            return Ok(getlist);
        }
       
        [HttpGet]
        [Route("{id:int}", Name = "GetById")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var get = await _nghiphep.GetbyId(id);
            if (get == null)
            {
                return NotFound();
            }
            return Ok(get);
        }
        [HttpGet]
        [Route("GetNgayNghiPhepCoLuong/{nhanvienid}")]
        public async Task<IActionResult> GetNgayNghiPhepCoLuong([FromRoute]int nhanvienid,bool status,int nam)
        {
            if (nhanvienid == 0||nam==0)
            {
                return NotFound();
            }
            var get = await _nghiphep.GetNgayNghiPhepCoLuong(nhanvienid,status ,nam);
            if (get == null||get==null)
            {
                return NotFound();
            }
            
            return Ok(get);
        }
      
        [HttpPost]
        [Route("CreateNghiPhep")]
        public async Task<IActionResult> CreateNghiPhep([FromBody] DTOAddNghiPhep model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var get =await _nghiphep.Add(model);
            if (get == false)
            {
                return BadRequest();
            }
            return Ok(get);
        }
        [HttpPut]
        [Route("{id:int}", Name = "UpdateNghiPhep")]
        public async Task<IActionResult> UpdateNghiPhep([FromRoute] int id, [FromBody] DTOUpdateNghiPhep model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var get =await _nghiphep.Update(id, model);
            if (get == false)
            {
                return BadRequest();
            }
            return Ok(get);
        }
        [HttpPut]
        [Route("NghiHuongLuongApproved/{id}")]
        public async Task <IActionResult> NghiHuongLuongApproved([FromRoute]int id, DTOUpdateHuongLuong approvedl)
        {
            if (approvedl == null)
            {
                return BadRequest();
            }
            var get =await _nghiphep.NghiHuongLuongApproved(id, approvedl);
            if (get == false)
            {
                return BadRequest();
            }
            return Ok(get);
        }
        [HttpDelete]
        [Route("{id:int}", Name = "DeleteNghiPhep")]
        public async Task <IActionResult> DeleteNghiPhep([FromRoute] int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var get =await _nghiphep.Delete(id);
            if (get == false)
            {
                return BadRequest();
            }
            return Ok(get);
        }
    }
}
