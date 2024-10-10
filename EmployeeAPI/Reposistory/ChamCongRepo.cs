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
    public class ChamCongRepo : GenericReposistory<ChamCong>,IChamCong
    {
       
        private readonly ILogger<ChamCongRepo> _log;
        private readonly IUnitofWork _unit;       
        public ChamCongRepo(EmployeeDBcontext context, ILogger<ChamCongRepo> log, IUnitofWork unit):base(context)
        {        
            _log = log;
            _unit = unit;
        }
        public async Task<bool> Create(DTOAddChamCong model)
        {
            var strat = _dbcontext.Database.CreateExecutionStrategy();
            return await strat.ExecuteAsync(async () =>
            {               
                    try
                    {
                        var getmodel = model.toDTOAddChamCong();   
                        if(getmodel.NgayChamCong<DateTime.Now)
                        {                           
                            _log.LogError("Ngay cham cong khong hop le");
                            return false;
                        }
                        await Add(getmodel);  
                    if(await _unit.CommitAsync())
                    _log.LogInformation("Add Cham cong success ");
                    return true;
                    }
                    catch(Exception ex) 
                    {                       
                      _log.LogError(ex,"Something wrong");
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
                    var getdelete = await _dbcontext.ChamCong.FirstOrDefaultAsync(i => i.Id == id);
                    if (getdelete is null)
                    {
                        _log.LogError("Ko tim thay du lieu");
                        return false;
                    }
                    Delete(getdelete);
                    if (await _unit.CommitAsync())
                        _log.LogInformation("xoa thanh cong");
                        return true;
                }
                catch (Exception ex)
                {
                    _log.LogError(ex, "Something wrong");
                    return false;
                }       
            });         
        }

        public async Task<Dictionary<DateOnly,List<DTOChamCong>>> GetAllLichChamCong()
        {
            var getall = await GetAll()
                .Select(x => x.toDTOChamCong()).ToListAsync();
            if(getall is null)
            {
                return [];
            }
            var result = getall
               .GroupBy(x => x.NgayChamCong)
               .ToDictionary(x => x.Key, x => x.ToList());
          
            return result;
     }

        public async Task<Dictionary<DateOnly,List<DTOChamCong>>> GetbyId(int id)
        {
            var getdata = await _dbcontext.ChamCong
                .Where(x => x.NhanVienId == id)
                .Select(x =>x.toDTOChamCong())
                .ToListAsync();
            if (getdata is null)
            {
                return [];
            }

            var result =  getdata
                .GroupBy(x => x.NgayChamCong)
                .ToDictionary(x => x.Key, x => x.ToList());
            return result;
        }


        public async Task <bool> Edit(int id, DTOUpdateChamCong model)
        {
            var strat = _dbcontext.Database.CreateExecutionStrategy();
            return await strat.ExecuteAsync(async () =>
            {
                using(var trans=await _dbcontext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var getupdate =await _dbcontext.ChamCong.FirstOrDefaultAsync(x => x.Id == id);
                        if(getupdate is null)
                        {
                            _log.LogError("Ko tim thay ban nghi");
                            return false;
                        }
                        getupdate.Clockin = (TimeSpan)model.Clockin;
                        getupdate.ClockOut = model.ClockOut;
                        if(await _unit.CommitAsync())                       
                            _log.LogInformation("luu thanh cong");
                            return true;                                         
                    }
                    catch(Exception ex)
                    {
                        _log.LogError(ex, "Something wrong");
                        return false;
                    }
                }    
            });     
        }
    }
}
