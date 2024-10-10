using EmployeeAPI.Data;
using EmployeeAPI.Mapper;
using EmployeeAPI.Model.DTO;
using EmployeeAPI.Model.DTOAdd;
using EmployeeAPI.Model.DTOUpdate;
using Microsoft.EntityFrameworkCore;
using EmployeeAPI.Interface.ModelInterface;
using Dapper;
using Microsoft.Extensions.Caching.Distributed;
using EmployeeAPI.Services;
using EmployeeAPI.Interface.Common;
using EmployeeAPI.Model.Domain;

namespace EmployeeAPI.Reposistory
{
    public class NghiPhepRepo : GenericReposistory<NghiPhep>,INghiPhep
    {        
        private readonly IDistributedCache _cache;
        private readonly ILogger<NghiPhepRepo> _log;
        private readonly IUnitofWork _unit;

        public NghiPhepRepo(EmployeeDBcontext context
            , IDistributedCache cache, ILogger<NghiPhepRepo> log, IUnitofWork unit) : base(context)
        {
            _cache = cache;
            _log = log;
            _unit = unit;
        }

        public async Task <bool> Add(DTOAddNghiPhep model)
        {
            var strat = _dbcontext.Database.CreateExecutionStrategy();
            return await strat.ExecuteAsync(async () =>
            {
               
                    try
                    {
                        var getmodel = model.toDTOAddNghiPhep();
                        if (Convert.ToDateTime(model.NgayNghi) > Convert.ToDateTime(model.NghiDen))
                        {
                            _log.LogError("Ngay nghi ko hop le");                           
                            return false;
                        }

                       await Add(getmodel);
                        if(await _unit.CommitAsync())
                        {
                           _log.LogInformation("succes");
                            return true;
                        }
                        else
                        {
                            _log.LogInformation("Something wrong");
                            return false;
                        }
                    }
                    catch(Exception ex)
                    {
                        _log.LogError(ex,"An error occurred while processing the request.");
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
                        var getdelete = await _dbcontext.NghiPhep.FirstOrDefaultAsync(i => i.Id == id);
                        if (getdelete is null)
                        {
                            _log.LogInformation("Cant find the right data try again");
                            return false;
                        }
                        Delete(getdelete);
                        if( await _unit.CommitAsync())
                        {
                        _log.LogInformation("succes");
                            return true;
                        }
                        else
                        {
                            _log.LogInformation("Something wrong");
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.LogError(ex, "An error occurred while processing the request.");
                        return false;
                    }
            });       
        }
        

        public async Task <bool> Update(int id, DTOUpdateNghiPhep model)
        {
            var strat = _dbcontext.Database.CreateExecutionStrategy();
            return await strat.ExecuteAsync(async () =>
            {           
                    try
                    {
                        var getupdate =await _dbcontext.NghiPhep.FirstOrDefaultAsync(i => i.Id == id);
                        if (Convert.ToDateTime(model.NghiDen) < Convert.ToDateTime(model.NgayNghi) || getupdate is null)
                        {
                            _log.LogError("Ngay nghi ko hop le ");
                           
                            return false;
                        }
                        getupdate.NghiDen = Convert.ToDateTime(model.NghiDen);
                        getupdate.LyDo = model.LyDo;
                        getupdate.NhanVienId = model.NhanVienId;
                        getupdate.NgayNghi = Convert.ToDateTime(model.NgayNghi);
                        if (await _unit.CommitAsync())
                        {
                            _log.LogInformation("succes");
                            return true;
                        }
                        else
                        {
                            _log.LogInformation("Something wrong");
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.LogInformation(ex, "An error occurred while processing the request");                      
                        return false;
                    }
                
            });
        }
        public async Task<Dictionary<int,List<DTONghiPhep>>> GetAllNghiPhep()
        {
            string cachekey = "ngaynghikey";
            var cachedata =await _cache.GetRecordsAsync<Dictionary<int, List<DTONghiPhep>>>(cachekey);
            if(cachedata != null)
            {
                _log.LogInformation("data nghi phep found in cache");
                return cachedata;
            }
            else
            {
                _log.LogInformation("no data found in cache fetch from database");
                var getall = await _dbcontext.NghiPhep.Select(x => x.toDTONghiPhep()).ToListAsync();
                if(getall != null)
                {
                    var getlistdata = getall.Where(x => x.NhanVienId != null)
                        .GroupBy(x => x.NhanVienId).ToDictionary(
                    x => (int)x.Key,
                    x => x.ToList()                    
                );
                    await _cache.SetRecordAsync(cachekey, getlistdata, TimeSpan.FromSeconds(40), TimeSpan.FromMinutes(4));
                    return getlistdata;
                }
                return [];
            }
        }

        public async Task<Dictionary<int,List<DTONghiPhep>>> GetbyId(int id)
        {
            var songaynghi =await GetTongSoNgayNghi(id);
            var getlistnghi = await _dbcontext.NghiPhep
                .Where(x => x.NhanVienId == id)
                .Select(x=>x.toDTONghiPhep()).ToListAsync();
            var getdata = getlistnghi.GroupBy(x => x.SoNgayNghi ?? 0)
                .ToDictionary(
                x => x.Key, 
                x => x.ToList());
           if(getdata is null)
            {
                return [];
            }          
            return getdata;
        }

        public async Task<IEnumerable<DTONghiPhep>> GetNgayNghiPhepCoLuong(int id, bool stats, int nam)
        {
                var getmodel = await _dbcontext.NghiPhep
                    .Where(i => i.NhanVienId == id && i.NghiDen.Year == nam && i.HuongLuong == stats)                                     
                    .Select(x => x.toDTONghiPhep()).ToListAsync();
                if (!getmodel.Any())
                {
                return [];
                }
                return getmodel;          
        }

        private async Task<int> GetTongSoNgayNghi(int id)
        {
            var tongSoNgayNghi = await _dbcontext.NghiPhep
        .Where(s => s.NhanVienId == id)
        .SumAsync(x => (int?)x.SoNgayNghi) ?? 0;
            return tongSoNgayNghi;
        }

        public async Task<bool> NghiHuongLuongApproved(int nhanvienid, DTOUpdateHuongLuong approved)
        {
            var getnghiphep =await _dbcontext.NghiPhep.FirstOrDefaultAsync(x => x.Id == nhanvienid);
            if(getnghiphep is null)
            {
                return false;
            }
            getnghiphep.HuongLuong = approved.Status;
            return await _unit.CommitAsync();
        }
    }
}
