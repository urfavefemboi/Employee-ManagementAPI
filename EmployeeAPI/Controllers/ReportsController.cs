using EmployeeAPI.Interface.ModelInterface;
using EmployeeAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Admin)]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        [Route("GetNhanVienGroupByChucVu")]
        public async Task<IActionResult> GetAllNhanVienGroupByChucVu()
        {         
            var get = await _reportService.GetAllNhanVienGroupByChucVu();
            if (get.IsNullOrEmpty())
            {
                return NotFound();
            }
            return Ok(get);
        }
        [HttpGet]
        [Route("GetNhanVienNghiBySoNgay")]
        public async Task<IActionResult> GetNhanVienNghiBySoNgay(int ngay)
        {
            var get = await _reportService.GetNhanVienNghiBySoNgay(ngay);
            if (!get.Any())
            {
                return NotFound();
            }
            return Ok(get);
        }
        [HttpGet]
        [Route("GetluongBynhanvienId")]
        public async Task<IActionResult> GetLuongBynhanvienId(int nhanvienid,int month)
        {
            if (nhanvienid == 0)
            {
                return NotFound();
            }
            var get = await _reportService.GetluongbyNhanVienId(nhanvienid,month);
            if (get < 0)
            {
                return NotFound();
            }
            return Ok(get);
        }
        [HttpGet]
        [Route("GethourBynhanvienId/{nhanvienid}")]
        public async Task<IActionResult> GethourBynhanvienId([FromRoute] int nhanvienid,int month)
        {
            if (nhanvienid == 0)
            {
                return NotFound();
            }
            var get =await _reportService.GethourbyNhanVienId(nhanvienid,month);
            if (get <0)
            {
                return NotFound();
            }
            return Ok(get);
        }
        [HttpGet]
        [Route("GetNhanVienGroupByPhongBan")]
        public async Task<IActionResult> GetNhanVienGroupByPhongBan()
        {
            var get = await _reportService.GetNhanVIenByPhongBan();
            if (get == null)
            {
                return NotFound();
            }
            return Ok(get);
        }
        [HttpPost]
        [Route("GetAllLunchShiftByDayOver1hour")]
        public async Task<IActionResult> GetAllLunchShiftByDayOver1hour([FromQuery]DateTime date)
        {
           var get = await _reportService.GetAllLunchShiftByDayOver1hour(date);
            if (get.IsNullOrEmpty())
            {
                return NotFound();
            }
            return Ok(get);
        }
        [HttpGet]
        [Route("GetSoNgayNghiTrongThangByNHanVienId")]
        public async Task<IActionResult> GetSoNgayNghiTrongThangByNHanVienId([FromQuery] int id,int nam )
        {
            var get = await _reportService.GetSoNgayNghiTrongThangByNHanVienId(id,nam);
            if (get.IsNullOrEmpty())
            {
                return NotFound();
            }
            return Ok(get);
        }
    }
}
