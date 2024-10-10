using Dapper;
using EmployeeAPI.Data;
using EmployeeAPI.Interface;
using EmployeeAPI.Interface.Common;
using EmployeeAPI.Mapper;
using EmployeeAPI.Model.Domain;
using EmployeeAPI.Model.DTO;
using EmployeeAPI.Model.DTOAdd;
using EmployeeAPI.Model.DTOUpdate;
using EmployeeAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Data;

namespace EmployeeAPI.Reposistory
{
    public class NhanVienRepo : GenericReposistory<NhanVien>,INhanVien
    {
        private readonly ILogger<NhanVienRepo> _logger;   
        private readonly IDistributedCache _cache;
        private readonly IFileUpload _upload;
        private readonly IUnitofWork _unit;
        public NhanVienRepo(
            EmployeeDBcontext context,
            IDistributedCache cache,
            ILogger<NhanVienRepo> logger,
            IFileUpload upload,
            IUnitofWork unit) : base(context)
        {
            _cache = cache;
            _logger = logger;
            _upload = upload;
            _unit = unit;
        }


        public async Task <bool> Add(DTOAddNhanVien model)
        {
            var strategy = _dbcontext.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {             
                    try
                    {
                       await _upload.Upload(model.Anh);
                        var getmodel = model.toDTOAddNhanVien();
                    await Add(getmodel);
                        if (await _unit.CommitAsync())
                        {
                            _logger.LogInformation("Success");                           
                            return true;
                        }
                        else 
                        {
                            _logger.LogInformation("Something wrong");                          
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {                      
                        // Optionally log the exception
                        _logger.LogInformation(ex,"Error");
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
                        var getdelete = await _dbcontext.NhanVien.FirstOrDefaultAsync(i => i.Id == id);
                        if (getdelete is null)
                        {
                            _logger.LogInformation("Ko tim thay nhan vien");                           
                            return false;
                        }
                        Delete(getdelete);
                        if (await _unit.CommitAsync())
                        {
                            _logger.LogInformation("Xoa thanh cong");                          
                            return true;
                        }
                        else
                        {
                            _logger.LogInformation("Loi xay ra");                           
                            return false;
                        }
                    }
                    catch(Exception ex)
                    {
                        _logger.LogInformation(ex,"Something wrong");
                        return false;
                    }
            });     
        }

       
        public async Task<IEnumerable<DTONhanVien>> GetAll(CancellationToken canceltoken)
        {
            const string cacheKey = "SecondKey";
        // Try to get the data from the cache
        var cachedData = await _cache.GetRecordsAsync<IEnumerable<DTONhanVien>>(cacheKey);
            if (cachedData != null)
            {
                _logger.LogInformation("NhanVien found in cache memory");
                return cachedData;
            }
            else
            {
                _logger.LogInformation("NhanVien not found in cache, fetching from database");

                // Fetch data from the database
                var getlist =  _dbcontext.NhanVien
                    .Include(x => x.PhongBan)
                    .Include(x => x.ChucVu)
                    .AsNoTracking()
                    .Select(x => x.toDTONhanVien());                  
                  var result=await getlist.ToListAsync(canceltoken);
               await _cache.SetRecordAsync(cacheKey, getlist, TimeSpan.FromSeconds(20), TimeSpan.FromMinutes(1));
                // Cache the data
                return result;
            }                                      
        }
        public async Task<DTONhanVien> GetByNhanVienId(int id)
        {
          string cachekey = $"GetById-{id}";
          var cachedata=await _cache.GetRecordsAsync<DTONhanVien>(cachekey);
            if(cachedata != null)
            {
                _logger.LogInformation(" data found in cache");
                return cachedata;
            }
            else
            {
                _logger.LogInformation("no data found in cachce, Fetch from db");
                var getbyid = await _dbcontext.NhanVien
                    .Include(x => x.ChucVu)
                    .Include(x => x.PhongBan)
                    .FirstOrDefaultAsync(x => x.Id == id);
                
                if (getbyid is null)
                {
                    return new DTONhanVien();
                }
                var getnhanvien = getbyid.toDTONhanVien();
               await _cache.SetRecordAsync<DTONhanVien>(cachekey, getnhanvien,TimeSpan.FromSeconds(20),TimeSpan.FromMinutes(1));
                return getnhanvien;
            }         
        }
        public async Task<IEnumerable<DTOChamCong>> GetChamCongByNhanVienId(int id)
        {
            var filter = _dbcontext.ChamCong.Where(x => x.NhanVienId == id).Select(x=>x.toDTOChamCong());

                if (filter is null || !filter.Any())
                {
                    return [];
                }

                var getchamcong =await filter.ToListAsync();
                return getchamcong;
            
        }
        public async Task<IEnumerable<DTOHopdong>> GetHopDongByNhanVienId(int id)
        {
            var filter = _dbcontext.HopDong.Where(x => x.NhanVienId == id).Select(x => x.toDTOHopDong());
                if (filter is null || !filter.Any())
                {
                    return [];
                }
                var getchamcong =await filter.ToListAsync();
                return getchamcong;
            
        }

        public async Task <bool> Update(int id, DTOUpdateNhanVien model)
        {
            var strategy = _dbcontext.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {               
                    try
                    {
                        var getupdate =await _dbcontext.NhanVien.FirstOrDefaultAsync(x => x.Id == id);
                        if (getupdate is not null)
                        {
                            getupdate.SDT = model.SDT;
                            getupdate.DiaChi = model.DiaChi;
                            getupdate.ChucVuId = model.ChucVuId;
                            getupdate.CMND = model.CMND;
                            getupdate.LuongId = model.LuongId;
                            getupdate.PhongBanId = model.PhongBanId;
                            getupdate.NgaySinh = Convert.ToDateTime(model.NgaySinh);
                            getupdate.TenNV = model.TenNV;
                            getupdate.GioiTinh = model.GioiTinh;

                            if(await _unit.CommitAsync())
                            {
                            _logger.LogInformation("Edit succes");
                              return true;
                            }
                        _logger.LogError("No succes");
                            return false;
                        }
                        _logger.LogInformation("ko tim thay nhan vien");
                        return false;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation(ex,"Something wrong");
                        return false;
                    }
            });
        }

        public async Task<IEnumerable<DTONghiPhep>> GetNghiPhepByNhanVienId(int id)
        {
            var getlist = await _dbcontext.NhanVien
                .Include(x => x.NghiPhep)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (getlist is null)
            {
                return [];
            }
            var getnghiphep = getlist.NghiPhep?.Select(x => x.toDTONghiPhep()).ToList();
            if (getnghiphep is null)
            {
                return [];
            }
            return getnghiphep;
        }
    }
}
