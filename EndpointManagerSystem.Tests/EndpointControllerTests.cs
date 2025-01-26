using EndpointManager.Controllers;
using EndpointManager.Interfaces;
using EndpointManager.Models;
using Moq;

namespace EndpointManagerSystem.Tests
{
    public class EndpointControllerTests
    {
        [Theory]
        [MemberData(nameof(GetEndpointDTO))]

        public void InsertEndpoint_ValidInput_ShouldCreateEndpoint(Endpoint endpoint, EndpointDTO endpointDTO)
        {
            // Arrange
            var mockEndpointView = new Mock<IEndpointView>(MockBehavior.Strict);
            var mockEndpointRepository = new Mock<IEndpointRepository>();
            mockEndpointView.Setup(x => x.RequestEndpoint()).Returns(endpointDTO);
            mockEndpointRepository.Setup(x => x.Find(endpointDTO.SerialNumber)).Returns((Endpoint?) null);
            var controller = new EndpointController(mockEndpointRepository.Object, mockEndpointView.Object);

            // Act
            controller.InsertEndpoint();

            // Assert
            mockEndpointRepository.Verify(repo => repo.Insert(It.Is<Endpoint>(received => AreEqualEndpoint(received, endpoint))));                    
        }

        [Fact]
        public void InsertEndpoint_ValidSerialNumber_InvalidSwitchState_ShouldThrowException()
        {
            // Arrange
            var endpointDTO = new EndpointDTO()
            {
                SerialNumber = "1234",
                ModelId = "NSX1P2W",
                MeterNumber = 15,
                MeterFirmwareVersion = "v1.0",
                SwitchState = "InvalidSwitchState"
            };
            var mockEndpointView = new Mock<IEndpointView>(MockBehavior.Strict);
            var mockEndpointRepository = new Mock<IEndpointRepository>(MockBehavior.Strict);
            mockEndpointView.Setup(x => x.RequestEndpoint()).Returns(endpointDTO);
            mockEndpointRepository.Setup(x => x.Find(endpointDTO.SerialNumber)).Returns((Endpoint?) null);
            var controller = new EndpointController(mockEndpointRepository.Object, mockEndpointView.Object);

            // Act
            void act() => controller.InsertEndpoint();

            // Assert
            var ex = Assert.Throws<Exception>(act);
            Assert.Equal("Switch State not valid.", ex.Message);                        
        }

        [Fact]
        public void InsertEndpoint_ValidSerialNumber_InvalidMeterModel_ShouldThrowException()
        {
            // Arrange
            var endpointDTO = new EndpointDTO()
            {
                SerialNumber = "1234",
                ModelId = "InvalidModelString",
                MeterNumber = 15,
                MeterFirmwareVersion = "v1.0",
                SwitchState = "armed"
            };
            var mockEndpointView = new Mock<IEndpointView>(MockBehavior.Strict);
            var mockEndpointRepository = new Mock<IEndpointRepository>(MockBehavior.Strict);
            mockEndpointView.Setup(x => x.RequestEndpoint()).Returns(endpointDTO);
            mockEndpointRepository.Setup(x => x.Find(endpointDTO.SerialNumber)).Returns((Endpoint?) null);
            var controller = new EndpointController(mockEndpointRepository.Object, mockEndpointView.Object);

            // Act
            void act() => controller.InsertEndpoint();

            // Assert
            var ex = Assert.Throws<Exception>(act);
            Assert.Equal("Meter Model not valid.", ex.Message);                        
        }

        [Theory]
        [MemberData(nameof(GetEndpointDTO))]
        public void InsertEndpoint_DuplicatedSerialNumber_ShouldThrowException(Endpoint endpoint, EndpointDTO endpointDTO)
        {
            // Arrange
            var mockEndpointView = new Mock<IEndpointView>(MockBehavior.Strict);
            var mockEndpointRepository = new Mock<IEndpointRepository>(MockBehavior.Strict);
            mockEndpointView.Setup(x => x.RequestEndpoint()).Returns(endpointDTO);
            mockEndpointRepository.Setup(x => x.Find(endpointDTO.SerialNumber)).Returns(endpoint);
            var controller = new EndpointController(mockEndpointRepository.Object, mockEndpointView.Object);

            // Act
            void act() => controller.InsertEndpoint();

            // Assert
            var ex = Assert.Throws<Exception>(act);
            Assert.Equal("Endpoint with this serial number already exists.", ex.Message);                        
        }

        [Theory]
        [MemberData(nameof(GetEndpointNewSwitchState))]
        public void EditEndpoint_EndpointFound_SwitchStateValid_DifferentSwitchState_ShouldEditEndpoint(Endpoint endpoint, string newSwitchState, int newSwitchStateId)
        {
            // Arrange
            var mockEndpointView = new Mock<IEndpointView>(MockBehavior.Strict);
            var mockEndpointRepository = new Mock<IEndpointRepository>();
            mockEndpointView.Setup(x => x.RequestSerialNumber()).Returns(endpoint.SerialNumber);
            mockEndpointRepository.Setup(x => x.Find(endpoint.SerialNumber)).Returns(endpoint);
            mockEndpointView.Setup(x => x.RequestSwitchState()).Returns(newSwitchState);
            var controller = new EndpointController(mockEndpointRepository.Object, mockEndpointView.Object);

            // Act
            controller.EditEndpoint();

            // Assert
            mockEndpointRepository.Verify(repo => repo.Edit(It.Is<string>(receivedSerialNumber => endpoint.SerialNumber.Equals(receivedSerialNumber)),
                                                            It.Is<int>(receivedSwitchState => newSwitchStateId == receivedSwitchState)));
        }

        [Theory]
        [MemberData(nameof(GetEndpointDTO))]
        public void EditEndpoint_EndpointFound_SwitchStateValid_SameSwitchState_ShouldThrowException(Endpoint endpoint, EndpointDTO endpointDTO)
        {
            // Arrange
            var mockEndpointView = new Mock<IEndpointView>(MockBehavior.Strict);
            var mockEndpointRepository = new Mock<IEndpointRepository>(MockBehavior.Strict);
            mockEndpointView.Setup(x => x.RequestSerialNumber()).Returns(endpoint.SerialNumber);
            mockEndpointRepository.Setup(x => x.Find(endpoint.SerialNumber)).Returns(endpoint);
            mockEndpointView.Setup(x => x.RequestSwitchState()).Returns(endpointDTO.SwitchState);
            var controller = new EndpointController(mockEndpointRepository.Object, mockEndpointView.Object);

            // Act
            void act() => controller.EditEndpoint();

            // Assert
            var ex = Assert.Throws<Exception>(act);
            Assert.Equal("Same Switch State value already registered.", ex.Message);
        }

        [Theory]
        [MemberData(nameof(GetEndpoint))]
        public void EditEndpoint_EndpointFound_SwitchStateInvalid_ShouldThrowException(Endpoint endpoint)
        {
            // Arrange
            var mockEndpointView = new Mock<IEndpointView>(MockBehavior.Strict);
            var mockEndpointRepository = new Mock<IEndpointRepository>(MockBehavior.Strict);
            mockEndpointView.Setup(x => x.RequestSerialNumber()).Returns(endpoint.SerialNumber);
            mockEndpointRepository.Setup(x => x.Find(endpoint.SerialNumber)).Returns(endpoint);
            mockEndpointView.Setup(x => x.RequestSwitchState()).Returns("InvalidSwitchState");
            var controller = new EndpointController(mockEndpointRepository.Object, mockEndpointView.Object);

            // Act
            void act() => controller.EditEndpoint();

            // Assert
            var ex = Assert.Throws<Exception>(act);
            Assert.Equal("Switch State not valid.", ex.Message);
        }

        [Fact]
        public void EditEndpoint_EndpointNotFound_ShouldThrowException()
        {
            // Arrange
            var mockEndpointView = new Mock<IEndpointView>(MockBehavior.Strict);
            var mockEndpointRepository = new Mock<IEndpointRepository>(MockBehavior.Strict);
            var serialNumber = "A1";
            mockEndpointView.Setup(x => x.RequestSerialNumber()).Returns(serialNumber);
            mockEndpointRepository.Setup(x => x.Find(serialNumber)).Returns((Endpoint?) null);
            var controller = new EndpointController(mockEndpointRepository.Object, mockEndpointView.Object);

            // Act
            void act() => controller.EditEndpoint();

            // Assert
            var ex = Assert.Throws<Exception>(act);
            Assert.Equal("Endpoint not found.", ex.Message);
        }

        [Theory]
        [MemberData(nameof(GetEndpoint))]
        public void DeleteEndpoint_EndpointFound_Confirmed_ShouldReturnSerialNumber(Endpoint endpoint)
        {
            // Arrange
            var serialNumber = endpoint.SerialNumber;
            var mockEndpointView = new Mock<IEndpointView>(MockBehavior.Strict);
            var mockEndpointRepository = new Mock<IEndpointRepository>(MockBehavior.Strict);
            mockEndpointView.Setup(x => x.RequestSerialNumber()).Returns(serialNumber);
            mockEndpointRepository.Setup(x => x.Find(serialNumber)).Returns(endpoint);
            mockEndpointView.Setup(x => x.RequestConfirmation()).Returns(true);
            mockEndpointRepository.Setup(x => x.Delete(serialNumber));
            var controller = new EndpointController(mockEndpointRepository.Object, mockEndpointView.Object);

            // Act
            var result = controller.DeleteEndpoint();

            // Assert
            Assert.Equal(serialNumber, result);
        }

        [Theory]
        [MemberData(nameof(GetEndpoint))]
        public void DeleteEndpoint_EndpointFound_NotConfirmed_ShouldReturnNull(Endpoint endpoint)
        {
            // Arrange
            var serialNumber = endpoint.SerialNumber;
            var mockEndpointView = new Mock<IEndpointView>(MockBehavior.Strict);
            var mockEndpointRepository = new Mock<IEndpointRepository>(MockBehavior.Strict);
            mockEndpointView.Setup(x => x.RequestSerialNumber()).Returns(serialNumber);
            mockEndpointRepository.Setup(x => x.Find(serialNumber)).Returns(endpoint);
            mockEndpointView.Setup(x => x.RequestConfirmation()).Returns(false);
            var controller = new EndpointController(mockEndpointRepository.Object, mockEndpointView.Object);

            // Act
            var result = controller.DeleteEndpoint();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void DeleteEndpoint_EndpointNotFound_ShouldThrowException()
        {
            // Arrange
            var mockEndpointView = new Mock<IEndpointView>(MockBehavior.Strict);
            var mockEndpointRepository = new Mock<IEndpointRepository>(MockBehavior.Strict);
            var serialNumber = "A1";
            mockEndpointView.Setup(x => x.RequestSerialNumber()).Returns(serialNumber);
            mockEndpointRepository.Setup(x => x.Find(serialNumber)).Returns((Endpoint?) null);
            var controller = new EndpointController(mockEndpointRepository.Object, mockEndpointView.Object);

            // Act
            void act() => controller.DeleteEndpoint();

            // Assert
            var ex = Assert.Throws<Exception>(act);
            Assert.Equal("Endpoint not found.", ex.Message);
        }

        [Theory]
        [MemberData(nameof(GetEndpointsDTOs))]
        public void ListAllEndpoints_ShouldListDTOs(List<Endpoint> endpoints, List<EndpointDTO> expectedDTOs)
        {
            // Arrange
            var mockEndpointView = new Mock<IEndpointView>();
            var mockEndpointRepository = new Mock<IEndpointRepository>(MockBehavior.Strict);            
            mockEndpointRepository.Setup(x => x.ListAll()).Returns(endpoints);
            var controller = new EndpointController(mockEndpointRepository.Object, mockEndpointView.Object);

            // Act
            controller.ListAllEndpoints();

            // Assert
            mockEndpointView.Verify(view => view.DisplayEndpoints(It.Is<List<EndpointDTO>>(receivedDTO => AreEqualDTOs(receivedDTO, expectedDTOs))));
        }

        [Theory]
        [MemberData(nameof(GetEndpointDTO))]
        public void FindEndpointBySerialNumber_EndpointFound_ShouldCreateDTO(Endpoint endpoint, EndpointDTO expectedDTO)
        {
            // Arrange
            var serialNumber = endpoint.SerialNumber;
            var mockEndpointView = new Mock<IEndpointView>();
            var mockEndpointRepository = new Mock<IEndpointRepository>(MockBehavior.Strict);
            mockEndpointView.Setup(x => x.RequestSerialNumber()).Returns(serialNumber);
            mockEndpointRepository.Setup(x => x.Find(serialNumber)).Returns(endpoint);
            var controller = new EndpointController(mockEndpointRepository.Object, mockEndpointView.Object);

            // Act
            controller.FindEndpointBySerialNumber();

            // Assert
            mockEndpointView.Verify(view => view.DisplayEndpoint(It.Is<EndpointDTO>(receivedDTO => AreEqualDTO(receivedDTO, expectedDTO))));
        }
        
        [Fact]
        public void FindEndpointBySerialNumber_EndpointNotFound_ShouldThrowException()
        {
            // Arrange
            var mockEndpointView = new Mock<IEndpointView>(MockBehavior.Strict);
            var mockEndpointRepository = new Mock<IEndpointRepository>(MockBehavior.Strict);
            var serialNumber = "A1";
            mockEndpointView.Setup(x => x.RequestSerialNumber()).Returns(serialNumber);
            mockEndpointRepository.Setup(x => x.Find(serialNumber)).Returns((Endpoint?) null);
            var controller = new EndpointController(mockEndpointRepository.Object, mockEndpointView.Object);

            // Act
            void act() => controller.FindEndpointBySerialNumber();

            // Assert
            var ex = Assert.Throws<Exception>(act);
            Assert.Equal("Endpoint not found.", ex.Message);
        }

        private bool AreEqualEndpoint(Endpoint received, Endpoint expected)
        {
            Assert.Equal(received.SerialNumber, expected.SerialNumber);
            Assert.Equal(received.ModelId, expected.ModelId);
            Assert.Equal(received.MeterNumber, expected.MeterNumber);
            Assert.Equal(received.MeterFirmwareVersion, expected.MeterFirmwareVersion);
            Assert.Equal(received.SwitchState, expected.SwitchState);

            return true;
        }
        
        private bool AreEqualDTOs(List<EndpointDTO> receivedList, List<EndpointDTO> expectedList)
        {
            if (receivedList.Count != expectedList.Count)
                return false;
            
            for(int i = 0; i < receivedList.Count; ++i)
                AreEqualDTO(receivedList[i], expectedList[i]);

            return true;
        }

        private bool AreEqualDTO(EndpointDTO receivedDTO, EndpointDTO expectedDTO)
        {
            Assert.Equal(receivedDTO.SerialNumber, expectedDTO.SerialNumber);
            Assert.Equal(receivedDTO.ModelId, expectedDTO.ModelId);
            Assert.Equal(receivedDTO.MeterNumber, expectedDTO.MeterNumber);
            Assert.Equal(receivedDTO.MeterFirmwareVersion, expectedDTO.MeterFirmwareVersion);
            Assert.Equal(receivedDTO.SwitchState, expectedDTO.SwitchState);

            return true;
        }

        public static IEnumerable<object[]> GetEndpoint()
        {
            var endpoints = TestsData.ReturnEndpoints();
            foreach(var endpoint in endpoints)
                yield return new object[] { endpoint };
        }

        public static IEnumerable<object[]> GetEndpointsDTOs()
        {
            yield return new object[] // Empty repository
            {
                new List<Endpoint>(),
                new List<EndpointDTO>()
            };

            var endpoints = TestsData.ReturnEndpoints();
            var DTOs = TestsData.ReturnDTOs();
            for(int i = 0; i < endpoints.Count; ++i)  // Only one register
                yield return new object[]
                {
                    new List<Endpoint>()
                    {
                        endpoints[i]
                    },
                    new List<EndpointDTO>()
                    {
                        DTOs[i]
                    }
                };

            yield return new object[] // Several registers
            {
                TestsData.ReturnEndpoints(),
                TestsData.ReturnDTOs()
            };
        }

        public static IEnumerable<object[]> GetEndpointDTO()
        {
            var endpoints = TestsData.ReturnEndpoints();
            var DTOs = TestsData.ReturnDTOs();
            for(int i = 0; i < endpoints.Count; ++i)
                yield return new object[] { endpoints[i], DTOs[i] };
        }
    
        public static IEnumerable<object[]> GetEndpointNewSwitchState()
        {
            var endpoints = TestsData.ReturnEndpoints();
            var newSwitchStates = TestsData.ReturnNewSwitchStates();
            for(int i = 0; i < endpoints.Count; ++i)
                yield return new object[] { endpoints[i], newSwitchStates[i].Item1, newSwitchStates[i].Item2 };
        }
    }
}