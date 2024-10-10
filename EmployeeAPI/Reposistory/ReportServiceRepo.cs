using EmployeeAPI.Data;
using EmployeeAPI.Interface.ModelInterface;
using EmployeeAPI.Mapper;
using EmployeeAPI.Model.DTO;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Reposistory
{
    public class ReportServiceRepo:IReportService
    {
        private readonly EmployeeDBcontext _context;
       
        private readonly DbContextOptions<EmployeeDBcontext> _contextOptions;
        public ReportServiceRepo(EmployeeDBcontext context,
            DbContextOptions<EmployeeDBcontext> contextOptions
           )
        {
            _context = context;
            _contextOptions = contextOptions;
           
        }
        private async Task<int> GetTongSoNgayNghi(int id,EmployeeDBcontext contextopions)
        {
            var tongSoNgayNghi = await contextopions.NghiPhep
        .Where(s => s.NhanVienId == id)
        .SumAsync(x => (int?)x.SoNgayNghi) ?? 0;
            return tongSoNgayNghi;
        }
       
        public async Task<IEnumerable<DTONhanVien>> GetNhanVienNghiBySoNgay(int ngay)
        {
            var getlist =await _context.NhanVien.Include(x=>x.PhongBan).Include(x=>x.ChucVu)
                .AsNoTracking()
                .Select(x => x.toDTONhanVien()).ToListAsync();

            if (getlist is null)
            {
                return [];
            }

            var tasks = getlist.Select(async item =>
            {
                using(var newcontext=new EmployeeDBcontext(_contextOptions))
                {
                    int totalDays = await GetTongSoNgayNghi((int)item.Id,newcontext);
                    return new { Item = item, TotalDays = totalDays };
                }             
            });
        
            var result = await Task.WhenAll(tasks);
            var FilterResult=result
                .Where(result=>result.TotalDays > ngay)
                .Select(result=>result.Item).ToList();
            return FilterResult;
    }

        public async Task<IEnumerable<DTONhanVien>> FindNhanVienByName(string name)
        {
            var getlist = _context.NhanVien
                .AsNoTrackingWithIdentityResolution()
                .Include(x => x.PhongBan)
                .Include(x => x.ChucVu)
                .Where(x => x.TenNV == name)
                .Select(x => x.toDTONhanVien());
          
            if (getlist is null)
            {
                return [];
            }

            var getdata = await getlist.ToListAsync();

            return getdata;
        }
        public async Task<Dictionary<string, List<DTONhanVien>>> GetAllNhanVienGroupByChucVu()
        {
            var getlistdata =await _context.NhanVien
                .AsNoTracking()
                .Include(i => i.ChucVu)
                .Include(x => x.PhongBan)
                .Select(x => x.toDTONhanVien())
                .ToListAsync();
            var result = getlistdata.GroupBy(x => x.TenChucVu ?? "Null");       
            if (getlistdata != null && getlistdata.Any())
            {
                var groupedResult = result
                    .ToDictionary(
                        g => g.Key,
                        g => g.ToList()
                    );

                return groupedResult;
            }
            return [];
        }
 
        public async Task<int> GetluongbyNhanVienId(int id,int thang)
        {
            var mucluong =await _context.ChamCong.Where(x => x.NhanVienId == id)
                .Select(x => x.NhanVien.Luong.MucLuong)
                .FirstOrDefaultAsync();
            if (mucluong is null)
            {
                return 0;
            }
            var totalHours = await GethourbyNhanVienId(id,thang); ;         
            double totalPaymen = (totalHours) * (25000 * int.Parse(mucluong));
            if (totalHours > 130)
            {
                var extrahour = (int)Math.Floor(totalHours - 130);
                totalPaymen += (extrahour * 30000);
                return (int)totalPaymen;
            }
            return (int)totalPaymen;
        }
        public async Task<double> GethourbyNhanVienId(int id,int thang)
        {
            var nhanvien = await _context.ChamCong
               .Where(x => x.NhanVienId == id && x.NgayChamCong.Month==thang)
               .ToListAsync();
            if(nhanvien is null)
            {
                return 0;
            }
            var totalHours = nhanvien
                .Select(c => (c.ClockOut - c.Clockin)?.TotalHours ?? 0)
                .Sum();
            return totalHours;

        }

        public async Task<Dictionary<string, List<DTONhanVien>>> GetNhanVIenByPhongBan()
        {
            var getlist = _context.NhanVien.Include(x => x.PhongBan).Include(x => x.ChucVu)
           .GroupBy(x => x.PhongBan.TenPhongBan ?? "NUll").AsSplitQuery();
            
            var result = await getlist
            .ToDictionaryAsync(
                 x => x.Key,
                 x => x.Select(x => x.toDTONhanVien()).ToList()
            );
          
            if (result is null)
            {
                return [];
            }
            return result;
        }

        public async Task<IEnumerable<DTOLunchShift>> GetAllLunchShiftByDayOver1hour(DateTime date)
        {
            var getfilterdatadate = await _context.LunchShift
                .Where(x => x.Date == date)
                .ToListAsync();
       
            var overtime = getfilterdatadate
                .Where(x => (x.EndTime - x.StartTime).TotalHours > 1)
                .OrderBy(x=>x.NhanvienId)
                .Select(x=>x.toDTOLunchShift());
            if(getfilterdatadate is null || overtime is null)
            {
                return [];
            }
            var result= overtime.ToList();
            return result;
        }

        public async Task<Dictionary<int, int>> GetSoNgayNghiTrongThangByNHanVienId(int id, int nam)
        {
            var datanghiphep = _context.NghiPhep
                .Where(x => x.NghiDen.Year == nam && x.NhanVienId == id)
                .GroupBy(x => x.NghiDen.Month).AsQueryable();

            var resultfilter =await datanghiphep.
                ToDictionaryAsync(
                x => x.Key,
                x => x.Select(x => x.SoNgayNghi ?? 0).Sum());
            return resultfilter;
        }

        
    }
}
