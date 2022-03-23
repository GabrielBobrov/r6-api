using System.Collections.Generic;
using R6.Core.Enums;
using R6.Domain.Validators;

namespace R6.Domain.Entities
{
    public class Operator : Base {

        //Propriedades
        public string Name { get; private set; }
        public DificultType Dificult { get; private set; }
        public SpeedType Speed { get; private set; }
        public ArmorType Armor { get; private set; }
        public Gun Gun { get; private set; }
        //EF
        protected Operator(){}

        public Operator(string name, DificultType dificult, SpeedType speed, ArmorType armor)
        {
            Name = name;
            Dificult = dificult;
            Speed = speed;
            Armor = armor;
            _errors = new List<string>();

            Validate();
        }


        //Comportamentos
        public void SetName(string name){
            Name = name;
            Validate();
        }

        public void SetSpeed(SpeedType speed){
            Speed = speed;
            Validate();
        }

        public void SetDificult(DificultType dificult){
            Dificult = dificult;
            Validate();
        }

        //Autovalida
        public bool Validate()
            => base.Validate(new OperatorValidator(), this);
    }
}