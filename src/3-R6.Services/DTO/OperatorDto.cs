using R6.Core.Enums;

namespace R6.Services.DTO
{
    public class OperatorDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Dificult { get; set; }
        public string Armor { get; set; }
        public string Speed { get; set; }

        public OperatorDto(long id, string dificult, string armor, string speed, string name)
        {
            Id = id;
            Dificult = dificult;
            Armor = armor;
            Speed = speed;
            Name = name;
        }
        public OperatorDto()
        {
        }
    }
}