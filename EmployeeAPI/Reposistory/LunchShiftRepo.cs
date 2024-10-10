using EmployeeAPI.Data;
using EmployeeAPI.Interface.Common;
using EmployeeAPI.Interface.ModelInterface;
using EmployeeAPI.Mapper;
using EmployeeAPI.Model.Domain;
using EmployeeAPI.Model.DTO;
using EmployeeAPI.Model.DTOAdd;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Reposistory
{
    public class LunchShiftRepo : GenericReposistory<LunchShift>,IlunchShift
    {
        private readonly IUnitofWork _unit;
        private readonly ILogger<LunchShiftRepo> _log;
        public LunchShiftRepo(EmployeeDBcontext context, ILogger<LunchShiftRepo> log, IUnitofWork unit) : base(context)
        {
            _log = log;
            _unit = unit;
        }
        public async Task<bool> Add(DTOAddLunchShift model)
        {       
            var strat = _dbcontext.Database.CreateExecutionStrategy();
            return await strat.ExecuteAsync(async () =>
            {
                try
                {
                    var getmodel = model.toDTOAddLunchShift();
                    var check = await _dbcontext.LunchShift.AnyAsync(x => x.NhanvienId == getmodel.NhanvienId && x.Date == getmodel.Date);
                    if (model.Date < DateTime.Now)
                    {
                        _log.LogError("ngay ko hop le");
                        return false;
                    }
                    var add = await _dbcontext.LunchShift.AddAsync(getmodel);
                    if (getmodel is null || check is true)
                    {
                        _log.LogError("Ton tai lich lunch ");
                        return false;
                    }
                    if (await _unit.CommitAsync())
                        _log.LogInformation("thanh cong");
                        return true;
                    
                }
                catch (Exception ex)
                {
                    _log.LogError(ex, "error");
                    return false;
                }
            });
        }

        public async Task<Dictionary<DateOnly,List<DTOLunchShift>>> GetAllGroupByDate()
        {
            var defaultDate =  DateOnly.MinValue;
            var getdata = await _dbcontext.LunchShift.Select(x => x.toDTOLunchShift()).ToListAsync();
            var getgroupby =  getdata.GroupBy(x => x.Date ?? defaultDate).ToDictionary(
                g => g.Key,
                g => g.ToList());
            if(getgroupby is null)
            {
                return [];
            }
            return getgroupby;
        }

        public async Task<IEnumerable<DTOLunchShift>> GetAllShiftByDay(DateTime date)
        {
            var getdata = _dbcontext.LunchShift.Where(x =>x.Date == date);
            if(getdata is null)
            {
                return [];
            }
            var result = await getdata.Select(x => x.toDTOLunchShift()).ToListAsync();
            return result;
        }

        public async Task<Dictionary<DateOnly,List<DTOLunchShift>>> GetbyNhanVienId(int id)
        {
            var defaultDate = DateOnly.MinValue;
            var getdata = _dbcontext.LunchShift.Where(x => x.NhanvienId == id);
            if (getdata is null)
            {
                return [];
            }
            var getlunchresult =await getdata
                .Select(x => x.toDTOLunchShift())
                .ToListAsync();

            var data = getlunchresult.GroupBy(x => x.Date ?? defaultDate).ToDictionary(
                x => x.Key,
                x => x.ToList());

            if(data is null)
            {
                return [];
            }
            return data;
        }

        public async Task<bool> Remove(int lunchshiftId)
        {
            var strat = _dbcontext.Database.CreateExecutionStrategy();
            return await strat.ExecuteAsync(async () =>
            {              
                {
                    try
                    {
                        var getdata = await _dbcontext.LunchShift.FirstOrDefaultAsync(x => x.Id == lunchshiftId);
                        if (getdata is null)
                        {
                            _log.LogError("no data found");
                            return false;
                        }
                        Delete(getdata);
                        if( await _unit.CommitAsync())
                        {
                            _log.LogInformation("Succes");
                            return true;
                        }
                        else
                        {
                            _log.LogError("ko thanh cong");
                            return false;
                        }
                    }
                    catch(Exception ex)
                    {
                        _log.LogError(ex,"something wrong");
                        return false;
                    }
                }
            });   
        }

    }
}
