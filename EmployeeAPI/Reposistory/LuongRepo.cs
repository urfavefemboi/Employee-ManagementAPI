using EmployeeAPI.Data;
using EmployeeAPI.Interface.Common;
using EmployeeAPI.Interface.ModelInterface;
using EmployeeAPI.Mapper;
using EmployeeAPI.Model.Domain;
using EmployeeAPI.Model.DTO;
using EmployeeAPI.Model.DTOAdd;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace EmployeeAPI.Reposistory
{
    public class LuongRepo : GenericReposistory<Luong>,ILuong
    {
        private readonly IUnitofWork _unit;
        private readonly ILogger<LuongRepo> _logger;
        public LuongRepo(EmployeeDBcontext context, IUnitofWork unit, ILogger<LuongRepo> logger) : base(context)
        {
            _unit = unit;
            _logger = logger;
        }
        public async Task <bool> Add(DTOAddLuong model)
        {
            var strat = _dbcontext.Database.CreateExecutionStrategy();
            return await strat.ExecuteAsync(async () =>
            {
                try
                {
                    var getadd = new Luong
                    {
                        MucLuong = model.MucLuong,
                    };
                    await Add(getadd);
                    if (await _unit.CommitAsync())
                    {
                        _logger.LogInformation("succes");
                        return true;
                    }
                    _logger.LogError("something wrong");
                    return false;
                }
                catch
                {
                    _logger.LogError("Error accure");
                    return false;
                }
            });              
        }

        public async Task <bool> Delete(int id)
        {
            var strat = _dbcontext.Database.CreateExecutionStrategy();
            return await strat.ExecuteAsync(async () =>
            {
                try
                {
                    var getdelete = await _dbcontext.Luong.FirstOrDefaultAsync(i => i.Id == id);
                    if(getdelete is null)
                    {
                        _logger.LogError("no match item");
                        return false;
                    }
                    Delete(getdelete);
                    if (await _unit.CommitAsync())
                    {
                        _logger.LogInformation("Succes");
                        return true;
                    }
                    _logger.LogError("something wrong");
                    return false;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "something wrong");
                    return false;
                }
            });
        }

        public async Task<IEnumerable<Luong>> GetallLuong()
        {
            var getmodel = await GetAll().ToListAsync();
            return getmodel;
        }

        public async Task<DTOLuong?> GetbyId(int id)
        {
            var getbyid = await _dbcontext.Luong.Select(x => new DTOLuong
            {
                MucLuong = x.MucLuong,
                Id = x.Id
            }).FirstOrDefaultAsync(i=>i.Id==id);
            if(getbyid is null)
            {
                return null;
            }
            return getbyid;
        }

        public async Task<Dictionary<int,List<DTONhanVien>>> GetNhanVienGrouByMucLuong()
        {
            var nhanvienlist = _dbcontext.NhanVien.Include(x => x.ChucVu)
                .Include(x => x.PhongBan).Select(x => x.toDTONhanVien());

             var getdatalist=await nhanvienlist.GroupBy(x => x.LuongId ?? 0)
                .ToDictionaryAsync(              
                    group => group.Key,
                    group => group.ToList()
                );           
            if (nhanvienlist is null)
            {
                return [];
            }          
            return getdatalist;
        }

        

        public async Task<bool> Update(int id, DTOAddLuong model)
        {           
            var strat = _dbcontext.Database.CreateExecutionStrategy();
            return await strat.ExecuteAsync(async () =>
            {
                try
                {
                    var getupdate = await _dbcontext.Luong.FirstOrDefaultAsync(i => i.Id == id);
                    if(getupdate is null)
                    {
                        _logger.LogError("no found item");
                        return false;
                    }
                    getupdate.MucLuong = model.MucLuong;
                    if (await _unit.CommitAsync())
                    {
                        _logger.LogInformation("Succes");
                        return true;
                    }
                    _logger.LogError("something wrong");
                    return false;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "something wrong");
                    return false;
                }
            });
        }
    }
}
