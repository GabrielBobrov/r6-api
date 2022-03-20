using AutoMapper;
using Bogus;
using Bogus.DataSets;
using EscNet.Cryptography.Interfaces;
using FluentAssertions;
using R6.Core.Communication.Mediator.Interfaces;
using R6.Domain.Entities;
using R6.Infra.Interfaces;
using R6.Services.DTO;
using R6.Services.Interfaces;
using R6.Services.Services;
using R6.Tests.Configurations.AutoMapper;
using R6.Tests.Fixtures;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using System.Linq.Expressions;
using R6.Core.Enums;

namespace R6.Tests.Projects.Services
{
    public class OperatorServiceTest
    {
        // Subject Under Test (Quem será testado!)
        private readonly IOperatorService _sut;

        //Mocks
        private readonly IMapper _mapper;
        private readonly Mock<IOperatorRepository> _operatorRepositoryMock;
        private readonly Mock<IRijndaelCryptography> _rijndaelCryptographyMock;
        private readonly Mock<IMediatorHandler> _mediatorHandler;

        public OperatorServiceTest()
        {
            _mapper = AutoMapperConfiguration.GetConfiguration();
            _operatorRepositoryMock = new Mock<IOperatorRepository>();
            _rijndaelCryptographyMock = new Mock<IRijndaelCryptography>();
            _mediatorHandler = new Mock<IMediatorHandler>();

            _sut = new OperatorService(
                mapper: _mapper,
                operatorRepository: _operatorRepositoryMock.Object,
                rijndaelCryptography: _rijndaelCryptographyMock.Object,
                mediator: _mediatorHandler.Object);
        }

        #region Search

        [Fact(DisplayName = "Search By Speed")]
        [Trait("Category", "Services")]
        public async Task SearchByName_WhenAnyUserFound_ReturnsAListOfUserDTO()
        {
            // Arrange
            var operatorsFound = OperatorFixture.CreateListValidOperators();

            _operatorRepositoryMock.Setup(x => x.SearchAsync(
                 It.IsAny<Expression<Func<Operator, bool>>>(),
                 It.IsAny<bool>()))
             .ReturnsAsync(() => operatorsFound);

            // Act
            var result = await _sut.SearchBySpeedAsync(It.IsAny<SpeedType>());

            // Assert
            result.Value.Should()
                .BeEquivalentTo(_mapper.Map<List<OperatorDto>>(operatorsFound));
        }

        [Fact(DisplayName = "Search By Speed When None Operators Found")]
        [Trait("Category", "Services")]
        public async Task SearchBySpeed_WhenNoneUserFound_ReturnsEmptyList()
        {
            // Arrange
            _operatorRepositoryMock.Setup(x => x.SearchAsync(
                 It.IsAny<Expression<Func<Operator, bool>>>(),
                 It.IsAny<bool>()))
             .ReturnsAsync(() => null);

            // Act
            var result = await _sut.SearchBySpeedAsync(It.IsAny<SpeedType>());

            // Assert
            result.Value.Should()
                .BeEmpty();
        }

        #endregion
    }
}