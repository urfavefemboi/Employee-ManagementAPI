using EmployeeAPI.Controllers;
using EmployeeAPI.Interface.ModelInterface;
using EmployeeAPI.Model.DTO;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Web_api_test_employee
{
    public class TestReportsController
    {
        private readonly ReportsController _report;
        private readonly Mock<IReportService> _mockservices;
       
        public TestReportsController()
        {        
            _mockservices = new Mock<IReportService>();
            _report = new ReportsController(_mockservices.Object);
         
        }

        [Fact]
        public async Task GetNhanVienGroupByChucVu_ReturnOkay()
        {
            //arrage          
            _mockservices.Setup(services => services.GetAllNhanVienGroupByChucVu())
               .ReturnsAsync(new Dictionary<string, List<DTONhanVien>>());
            //act
            var result = await _report.GetAllNhanVienGroupByChucVu() as OkObjectResult;
            //moq config

            //assert
            result.StatusCode.Should().Be(200);
            result.Should().NotBeNull();
        }
        [Fact]
        public async Task GetAllLunchShiftByDayOver1hour_ReturnOkay()
        {
            //arrage
            var date =Convert.ToDateTime("02/02/2003");
            _mockservices.Setup(services => services.GetAllLunchShiftByDayOver1hour(date))
               .ReturnsAsync(new List<DTOLunchShift>());
            //act
            var result = await _report.GetAllLunchShiftByDayOver1hour(date) as OkObjectResult;
            //moq config
            //assert
            result.StatusCode.Should().Be(200);
        }
        [Fact]
        public async Task GetNhanVIenByPhongBan_RetrunOk()
        {
            //arrage
           
            _mockservices.Setup(services => services.GetNhanVIenByPhongBan())
               .ReturnsAsync(new Dictionary<string, List<DTONhanVien>>());
            //act
            var result = await _report.GetNhanVienGroupByPhongBan() as OkObjectResult;
            //moq config
             
            //assert
            result.StatusCode.Should().Be(200);
         
        }
      
    }
}
