using EmployeeAPI.Interface;
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
    public class NhanVienController : ControllerBase
    {
        private readonly INhanVien _nhanvien;
        
        public NhanVienController(INhanVien nhanvien)
        {
            _nhanvien = nhanvien;
        }
        [HttpGet]
        [Route("GetAllNhanVien")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllNhanVien([FromQuery] QueryOptions query,CancellationToken canceltoken)
        {
            var getlist = await _nhanvien.GetAll(canceltoken);
            var data= getlist.Skip((query.PageNumbers - 1) * query.PageSize)
                    .Take(query.PageSize);
            
            if (getlist == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
        
      
        [HttpGet]
        [Route("{id:int}", Name = "GetNhanVienById")]
        public async Task<IActionResult> GetNhanVienById([FromRoute] int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var get = await _nhanvien.GetById(id);
            if (get == null)
            {
                return NotFound();
            }
            return Ok(get);
        }
        [HttpGet]
        [Route("GetHopDongbyNhanVienId")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetHopDongbyNhanVienId(int id)
        {
            var getlist = await _nhanvien.GetHopDongByNhanVienId(id);
            if (getlist == null)
            {
                return NotFound();
            }
            return Ok(getlist);
        }
        [HttpGet]
        [Route("GetChamCongbyNhanVienId")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetChamCongbyNhanVienId(int id)
        {
            var getlist = await _nhanvien.GetChamCongByNhanVienId(id);
            if (getlist == null)
            {
                return NotFound();
            }
            return Ok(getlist);
        }
        [HttpGet]
        [Route("GetNghiPhepbyNhanVienId")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetNghiPhepbyNhanVienId(int id)
        {
            var getlist = await _nhanvien.GetNghiPhepByNhanVienId(id);
            if (getlist == null)
            {
                return NotFound();
            }
            return Ok(getlist);
           
        }
        
        [HttpPost]
        [Route("CreateNhanVien")]
        public async Task <IActionResult> CreateNhanVien([FromForm] DTOAddNhanVien model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var get =await _nhanvien.Add(model);
            if (get == false)
            {
                return BadRequest();
            }
            
            return Ok(get);
        }
        [HttpPut]
        [Route("{id:int}", Name = "UpdateNhanVien")]
        public async Task <IActionResult> UpdateNhanVien([FromRoute] int id, [FromBody] DTOUpdateNhanVien model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var get =await _nhanvien.Update(id, model);
            if (get == false)
            {
                return BadRequest();
            }
            return Ok(get);
        }
        [HttpDelete]
        [Route("{id:int}", Name = "DeleteNhanVien")]
        public async Task <IActionResult> DeleteNhanVien([FromRoute] int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var get =await _nhanvien.Delete(id);
            if (get == false)
            {
                return BadRequest();
            }
            return Ok(get);
        }
    }
}
