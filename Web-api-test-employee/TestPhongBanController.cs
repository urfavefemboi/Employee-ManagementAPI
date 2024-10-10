using EmployeeAPI.Controllers;
using EmployeeAPI.Interface.ModelInterface;
using EmployeeAPI.Model.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Web_api_test_employee
{
    public class TestPhongBanController
    {
        private readonly PhongBanController _phongban;
        private readonly Mock<IPhongBan> _mockService;
        public TestPhongBanController()
        {
            _mockService = new Mock<IPhongBan>();
            _phongban = new PhongBanController(_mockService.Object);
        }
        [Fact]
        public async Task GetPhongBanbyId_ReturnOkay()
        {
            //Arrange
            var phongbanid = 4;
            var expectedResult = new DTOPhongBan { Id = phongbanid, TenPhongBan = "it" };
           
            //Config mock service
            _mockService.Setup(services => services.GetbyId(phongbanid)).ReturnsAsync(expectedResult);
            //Act
            var result = await _phongban.GetPhongBanById(phongbanid) as OkObjectResult;
  
            //Assert
            Assert.Equal(200, result.StatusCode);   
        }
        [Fact]
        public async Task GetPhongBanById_ReturnsNullWhenNotFound()
        {
            // Arrange
            // Assuming this ID does not exist
            var userId = 99;
            //mock config
            _mockService.Setup(services => services.GetbyId(userId)).ReturnsAsync((DTOPhongBan)null);
            // Act
            var result = await _phongban.GetPhongBanById(userId) as NotFoundResult;
            // Assert
            // Check that result is null
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }
        [Fact]
        public async Task GetAllPhongBan_ReturnsAll()
        {  
            // Act
            var result =await _phongban.GetAllPhongBan() as OkObjectResult;

            _mockService.Setup(services => services
            .GetAllPhongBan()).ReturnsAsync(new List<DTOPhongBan>());
            // Assert
            // Check that result is null
           
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result);
        }
        

    }
}