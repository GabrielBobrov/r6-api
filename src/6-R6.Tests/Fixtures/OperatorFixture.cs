using Bogus;
using Bogus.DataSets;
using Moq;
using R6.Core.Enums;
using R6.Domain.Entities;
using R6.Services.DTO;
using System.Collections.Generic;

namespace R6.Tests.Fixtures
{
    public class OperatorFixture
    {
        public static Operator CreateValidOperator()
        {
            return new Operator(
                name: new Name().FirstName(),
                dificult: It.IsAny<DificultType>(),
                speed: It.IsAny<SpeedType>(),
                armor: It.IsAny<ArmorType>());
        }

        public static List<Operator> CreateListValidOperators(int limit = 5)
        {
            var list = new List<Operator>();

            for (int i = 0; i < limit; i++)
                list.Add(CreateValidOperator());

            return list;
        }

        public static OperatorDto CreateValidUserDTO(bool newId = false)
        {
            return new OperatorDto
            {
                Id = newId ? new Randomizer().Int(0, 1000) : 0,
                Name = new Name().FirstName(),
                Dificult = It.IsAny<DificultType>().ToString(),
                Speed = It.IsAny<SpeedType>().ToString(),
                Armor = It.IsAny<ArmorType>().ToString()
            };
        }

        public static OperatorDto CreateInvalidUserDTO()
        {
            return new OperatorDto
            {
                Id = 0,
                Name = "",
                Dificult = "",
                Speed = "",
                Armor = ""
            };
        }
    }
}