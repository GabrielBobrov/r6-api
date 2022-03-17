using System.Collections.Generic;
using R6.Core.Enums;
using R6.Domain.Validators;

namespace R6.Domain.Entities
{
    public class Operator : Base {

        //Propriedades
        public string Name { get; private set; }
        public int Dificult { get; private set; }
        public int Speed { get; private set; }
        public ArmorType Armor { get; private set; }


        //EF
        protected Operator(){}

        public Operator(string name, int dificult, int speed, ArmorType armor)
        {
            Name = name;
            Dificult = dificult;
            Speed = speed;
            Armor = armor;
            _errors = new List<string>();

            Validate();
        }


        //Comportamentos
        public void SetDificult(string name){
            Name = name;
            Validate();
        }

        public void SetSpeed(int speed){
            Speed = speed;
            Validate();
        }

        public void SetEmail(int email){
            Dificult = email;
            Validate();
        }

        //Autovalida
        public bool Validate()
            => base.Validate(new OperatorValidator(), this);
    }
}