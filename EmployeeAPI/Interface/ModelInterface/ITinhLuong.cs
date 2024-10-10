namespace EmployeeAPI.Interface.ModelInterface
{
    public interface ITinhLuong
    {
      Task<int> GetluongbyNhanVienId(int id, int thang);
       Task<double> GethourbyNhanVienId(int id,int thang);
    }
}
