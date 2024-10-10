using EmployeeAPI.Data;
using EmployeeAPI.Interface.Common;
using EmployeeAPI.Interface.ModelInterface;
using EmployeeAPI.Model.Domain;
using EmployeeAPI.Model.DTO;
using EmployeeAPI.Model.DTOAdd;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Reposistory
{
    public class ThanhToanRepo : GenericReposistory<ThanhToan>,ITHanhToan
    {
       
        private readonly IReportService _services ;
        private readonly IUnitofWork _unit;
        private readonly ILogger<ThanhToanRepo> _log;
        public ThanhToanRepo(EmployeeDBcontext context, IReportService services, IUnitofWork unit, ILogger<ThanhToanRepo> log):base(context)
        {           
            _services = services;
            _unit = unit;
            _log = log;
        }
        public async Task<bool> Add(DTOAddThanhToan model)
        {
            var strat = _dbcontext.Database.CreateExecutionStrategy();
            return await strat.ExecuteAsync(async ()=>
            {
                    try
                    {
                        var getluong =await _services.GetluongbyNhanVienId((int)model.NhanVienId, (int)model.ThangLinhLuong);
                        var getid = (_dbcontext.ThanhToan.Max(x => (int?)x.Id) ?? 0) + 1;
                        var getmodel = new ThanhToan
                        {
                            Id = getid,
                            LinhLuong = getluong,
                            NhanVienId = model.NhanVienId
                       
                        };
                    await Add(getmodel);
                        if (await _unit.CommitAsync())
                            _log.LogInformation("Succes");
                            return true;                    
                    }
                    catch(Exception ex) 
                    {
                            _log.LogError(ex, "somethingwrong");
                        return false;
                    }         
            });
        }

        public async Task<bool> Delete(int id)
        {
            var strat = _dbcontext.Database.CreateExecutionStrategy();
            return await strat.ExecuteAsync(async () =>
            {             
                    try
                    {
                        var getdeletee =await _dbcontext.ThanhToan.FirstOrDefaultAsync(x => x.NhanVienId == id);
                        if (getdeletee is null)
                        {
                        _log.LogError("KO tim thay ban nghi");
                            return false;
                        }
                    Delete(getdeletee);
                        if( await _unit.CommitAsync())
                        
                        _log.LogInformation("Succes");
                        return true;
                       
                    }
                    catch(Exception ex)
                    {
                    _log.LogError(ex, "something wrong");
                        return false;
                    }
                
            }); 
        }
        public async Task<IEnumerable<DTOThanhToan>> GetAllThanhToan()
        {
            var getall = await _dbcontext.ThanhToan.Select(x => new DTOThanhToan
            {
                LinhLuong = x.LinhLuong,
                NhanVienId = x.NhanVienId,
                Id = x.Id
            }).ToListAsync();
            return getall;
        }

        public async Task<IEnumerable<DTOThanhToan>> GetbyNhanVienId(int id)
        {
            var getbyid = await _dbcontext.ThanhToan.Where(x => x.NhanVienId == id)
            .Select(x => new DTOThanhToan
            {
                LinhLuong = x.LinhLuong,
                Id=x.Id,
                NhanVienId = x.NhanVienId
            }).ToListAsync();
            return getbyid;
        }

       

        public async Task<bool> Update(DTOAddThanhToan model)
        {
            var start = _dbcontext.Database.CreateExecutionStrategy();
            return await start.ExecuteAsync(async () =>
            {        
                    try
                    {
                        var getupdate =await _dbcontext.ThanhToan.FirstOrDefaultAsync(x => x.NhanVienId == model.NhanVienId);
                        if(getupdate is null)
                        {
                            _log.LogError("ko tim thay ban nghi");
                            return false;
                        }
                        var getluong =await _services.GetluongbyNhanVienId(model.NhanVienId,model.ThangLinhLuong);
                        getupdate.LinhLuong = getluong;

                    if (await _unit.CommitAsync())
                        _log.LogInformation("Sucess");
                        return true;  
                    }
                    catch(Exception ex)
                    {
                    _log.LogError(ex, "something wrong");
                        return false;
                    }         
            });
            
        }
    }
}
