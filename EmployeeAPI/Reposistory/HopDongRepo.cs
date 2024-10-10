using EmployeeAPI.Data;
using EmployeeAPI.Interface.Common;
using EmployeeAPI.Interface.ModelInterface;
using EmployeeAPI.Mapper;
using EmployeeAPI.Model.Domain;
using EmployeeAPI.Model.DTO;
using EmployeeAPI.Model.DTOAdd;
using EmployeeAPI.Model.DTOUpdate;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Reposistory
{
    public class HopDongRepo : GenericReposistory<HopDong>,IHopDong
    {
        private readonly ILogger<HopDongRepo> _log;
        private readonly IUnitofWork _unit;
        public HopDongRepo(EmployeeDBcontext context, ILogger<HopDongRepo> log, IUnitofWork unit) : base(context)
        {
          
            _log = log;
            _unit = unit;
        }
        public async Task <bool> AddHopDong(DTOAddHopDOng model)
        {
            var start = _dbcontext.Database.CreateExecutionStrategy();
            return await start.ExecuteAsync(async () =>
            {              
                    try
                    {
                        var getmodel = model.toDTOAddHopDong();                       
                        if (getmodel.NgayKyHD > getmodel.HanHD)
                        {
                        _log.LogWarning("Wrong date");
                            return false;
                        }                       
                        if(await _unit.CommitAsync())
                        {
                        _log.LogInformation("succes");
                            return true;
                        }
                        else
                        {
                        _log.LogError("Something wrong");
                        return false;
                        }
                    }
                    catch
                    {
                    _log.LogError("Error");
                    return false;
                    }
                
            });           
        }

        public async Task <bool> DeleteHopDong(int id)
        {
            
            var start = _dbcontext.Database.CreateExecutionStrategy();
            return await start.ExecuteAsync(async () =>
            {
                try
                {
                    var getdelete = await _dbcontext.HopDong.FirstOrDefaultAsync(i => i.Id == id);
                    if (getdelete is null)
                    {
                        _log.LogError("No found data");
                        return false;
                    }
                    Delete(getdelete);
                    if (await _unit.CommitAsync())
                    {
                        _log.LogInformation("succes");
                        return true;
                    }
                    else
                    {
                        _log.LogError("Something wrong");
                        return false;
                    }
                }
                catch
                {
                    _log.LogError("Error");
                    return false;
                }
            });
        }

        public async Task<IEnumerable<DTOHopdong>> GetAllByStatus(bool status)
        {
            var getlist = await GetAll().Where(x=>x.HieuLuc==status)
                .Select(x => x.toDTOHopDong()).ToListAsync();
            if(getlist is null)
            {
                return [];
            }
            return getlist;
        }

        public async Task<DTOHopdong> GetbyId(int id)
        {
           var get= await _dbcontext.HopDong
                .Select(x=>x.toDTOHopDong())
                .FirstOrDefaultAsync(x=>x.Id==id);
            if(get is null)
            {
                return null;
            }
            return get;
        }

        public async Task<Dictionary<int, List<DTOHopdong>>> GetHopDongByNhanVien()
        {
            var getlist =await _dbcontext.HopDong
                .AsSplitQuery()
                .Select(x => x.toDTOHopDong())
                .ToListAsync();
                if(getlist.Any()&&getlist!=null )
            {
                var result = getlist.GroupBy(x => x.NhanVienId?? 0).ToDictionary(
               g => g.Key,
               g => g.ToList()
           );
                return result;
            }
            return [];
        }

        

        public async Task <bool> UpdateHopDong(int id, DTOUpdateHopdong model)
        {           
            var start = _dbcontext.Database.CreateExecutionStrategy();
            return await start.ExecuteAsync(async () =>
            {
                try
                {
                    var getupdate = await _dbcontext.HopDong.FirstOrDefaultAsync(x => x.Id == id);
                    if (Convert.ToDateTime(model.NgayKyHD) > Convert.ToDateTime(model.HanHD) || getupdate is null)
                    {
                        _log.LogWarning("Date ko hop le");
                        return false;
                    }
                    else
                    {
                        getupdate.HieuLuc = model.HieuLuc;
                    }
                    Update(getupdate);
                    if (await _unit.CommitAsync())
                    {
                        _log.LogInformation("succes");
                        return true;
                    }
                    else
                    {
                        _log.LogError("Something wrong");
                        return false;
                    }
                }
                catch
                {
                    _log.LogError("Error");
                    return false;
                }
            });
        }
    }
}
