using EmployeeAPI.Interface.ModelInterface;
using EmployeeAPI.Model.DTOAdd;
using EmployeeAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.User)]
    [ApiController]
    public class LunchShiftController : ControllerBase
    {
        private readonly IlunchShift _lunch;
        public LunchShiftController(IlunchShift lunch)
        {
            _lunch = lunch;
        }
        [HttpGet]
        [Route("GetAllLunchShift")]
        public async Task<IActionResult> GetAllLunchShift() 
        {
            var getdata = await _lunch.GetAllGroupByDate();
            if(getdata is null)
            {
                return NotFound();
            }
            return Ok(getdata);
        }
        [HttpGet]
        [Route("GetAllLunchShiftByDate")]
        public async Task<IActionResult> GetAllLunchShiftByDate(DateTime date)
        {
            var getdata = await _lunch.GetAllShiftByDay(date);
            if (getdata is null)
            {
                return NotFound();
            }
            return Ok(getdata);
        }
        [HttpGet]
        [Route("GetLunchShiftByNhanVienId/{id}")]
        public async Task<IActionResult> GetLunchShiftByNhanVienId([FromRoute]int id)
        {
            var getdata = await _lunch.GetbyNhanVienId(id);
            
            if (getdata is null)
            {
                return NotFound();
            }
            return Ok(getdata);
        }

        [HttpPost]
        [Route("CreateLunchShift")]
        public async Task<IActionResult> CreateLunchShift([FromForm] DTOAddLunchShift model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var get = await _lunch.Add(model);
            if (get == false)
            {
                return BadRequest();
            }
            return Ok(get);
        }
        [HttpDelete]
        [Route("DeleteLunchShift/{id}")]
        public async Task<IActionResult> DeleteLunchShift([FromRoute] int id)
        {
            var get = await _lunch.Remove(id);
            if (get == false)
            {
                return BadRequest();
            }
            return Ok(get);
        }
    }
}
