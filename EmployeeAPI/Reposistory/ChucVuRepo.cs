using Dapper;
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
    public class ChucVuRepo : GenericReposistory<ChucVu>,IChucVu
    {
        private readonly IUnitofWork _unit;
        private readonly ILogger<ChucVu> _log;
        public ChucVuRepo(EmployeeDBcontext context,IUnitofWork unit, ILogger<ChucVu> log) : base(context)
        {
            _unit = unit;
            _log = log;
        }
        public async Task<bool> Add(DTOAddChucVu model)
        {
            var strat = _dbcontext.Database.CreateExecutionStrategy();
            return await strat.ExecuteAsync(async () =>
            {             
                    try
                    {
                        var getadd = new ChucVu
                        {
                            TenChucVu = model.TenChucVu
                        };
                      await Add(getadd);
                        if (await _unit.CommitAsync())
                        {
                        _log.LogInformation("succes");
                            return true;
                        }
                        else
                        {
                        _log.LogError("Loi xay ra");
                            return false;
                        }
                    }
                    catch
                    {
                    _log.LogError("Something Wrong");
                    // Optionally log the exception
                    return false;
                    }
                
            });
        }

        public async Task<bool> Delete(int id)
        {
            var getdelete = await _dbcontext.ChucVu.FirstOrDefaultAsync(x => x.Id == id);
            if (getdelete is null)
            {
                _log.LogError("Ko tim thay chuc vu");
                return false;
            }
            Delete(getdelete);
            return await _unit.CommitAsync();
        }

        public async Task<IEnumerable<DTOChucVu>> GetallChucVu()
        {
            var getall = await _dbcontext.ChucVu.Select(x=>new DTOChucVu
            {
                Id=x.Id,
                TenChucVu=x.TenChucVu
            }).ToListAsync();
            if(getall is null)
            {
                return [];
            }
            return getall;
        }

        public async Task<DTOChucVu> GetbyId(int id)
        {
            var getbyid= await _dbcontext.ChucVu.Select(x=>new DTOChucVu
            {
                Id=x.Id,
                TenChucVu=x.TenChucVu
            }).FirstOrDefaultAsync(x=>x.Id==id);
            if(getbyid is null)
            {
                return null;
            }
            return getbyid;
        }

        public async Task<IEnumerable<DTONhanVien>> GetNhanVienByChucVuId(int id)
        {
            var getlist =  _dbcontext.NhanVien.Include(x=>x.PhongBan).Include(x=>x.ChucVu).Where(i => i.ChucVuId == id);
            if(getlist is null)
            {
                return [];
            }
            var getnhanvien = await getlist.Select(x => x.toDTONhanVien()).ToListAsync();
            return getnhanvien;
        }

      

        public async Task <bool> Update(int id, DTOAddChucVu model)
        {

            var getupdate = await _dbcontext.ChucVu.FirstOrDefaultAsync(x => x.Id == id);
            if(getupdate is null)
            {
                return false;
            }
            getupdate.TenChucVu = model.TenChucVu;
            return await _unit.CommitAsync();
        }
    }
}
