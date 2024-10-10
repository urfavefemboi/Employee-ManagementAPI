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
    public class PhongBanRepo : GenericReposistory<PhongBan>,IPhongBan
    {
        private readonly ILogger<PhongBanRepo> _log;
        private readonly IUnitofWork _unit;
        public PhongBanRepo(EmployeeDBcontext context, ILogger<PhongBanRepo> log, IUnitofWork unit) : base(context)
        {
            _log = log;
            _unit = unit;
        }
        public  async Task <bool> Add(DTOAddPhongBan model)
        {
            var getadd = new PhongBan
            {
                TenPhongBan = model.TenPhongBan
            };
           await Add(getadd);
            return await Save();
        }

        public async Task <bool> Delete(int id)
        {
            var getdelete =await _dbcontext.PhongBan.FirstOrDefaultAsync(x => x.Id == id);
            if(getdelete is null)
            {
                return false;
            }
             Delete(getdelete);
            return await Save();
        }

        public async Task<IEnumerable<DTONhanVien>> GetAllNhanVienById(int id)
        {     
            var getlist = _dbcontext.NhanVien.Include(x=>x.PhongBan).Include(x=>x.ChucVu)
                .Where(i => i.PhongBanId == id)
                .Select(x => x.toDTONhanVien());
            if(getlist is null)
            {
                return [];
            }
            var getnhanvien =await getlist.ToListAsync();
            return getnhanvien;
        }

        public async Task<IEnumerable<DTOPhongBan>> GetAllPhongBan()
        {
            var getlist = await _dbcontext.PhongBan.Select(x => new DTOPhongBan
            {
                Id = x.Id,
                TenPhongBan = x.TenPhongBan
            }).ToListAsync();
            return getlist;
        }

        public async Task<DTOPhongBan> GetbyId(int id)
        {
            var get = await _dbcontext.PhongBan.Select(x => new DTOPhongBan
            {
                Id = x.Id,
                TenPhongBan = x.TenPhongBan
            }).FirstOrDefaultAsync(x=>x.Id==id);
            if(get is null)
            {
                return new DTOPhongBan();
            }
            return get;
        }

        public async Task<Dictionary<string, int>> GetSoLuongNhanVienTungPhongBan()
        {
            var filterdata = await _dbcontext.NhanVien
                .GroupBy(x => x.PhongBan.TenPhongBan ?? "Null")
                .ToDictionaryAsync(
                x=>x.Key,
                x=>x.Count());
            return filterdata;    
        }

        public async Task <bool> Save()
        {
            var result = await _dbcontext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> Update(int id, DTOAddPhongBan model)
        {
            var getupdate = _dbcontext.PhongBan.FirstOrDefault(i => i.Id == id);
            if(getupdate is null)
            {
                return false;
            }
            getupdate.TenPhongBan = model.TenPhongBan;
            return await Save();
        }
    }
}
