
using System.Collections.Generic;
using R6.Core.Enums;
using R6.Domain.Validators;

namespace R6.Domain.Entities
{
    public class Gun : Base {

        //Propriedades
        public string Name { get; private set; }
        public PrimaryGunType? PrimaryGunType { get; private set; }
        public SecondaryGunType? SecondaryGunType { get; private set; }
        public long operatorId { get; private set; }
        //EF Relations
        public Operator Operator { get; private set; }


        //EF
        protected Gun(){}

        public Gun(string name, PrimaryGunType primaryGunType, SecondaryGunType secondaryGunType)
        {
            Name = name;
            PrimaryGunType = primaryGunType;
            SecondaryGunType = secondaryGunType;
            _errors = new List<string>();

            Validate();
        }


        //Comportamentos
        public void SetName(string name){
            Name = name;
            Validate();
        }

        public void SetSecondaryGunType(SecondaryGunType secondaryGunType){
            SecondaryGunType = secondaryGunType;
            Validate();
        }

        public void SetPrimaryGunType(PrimaryGunType primaryGunType){
            PrimaryGunType = primaryGunType;
            Validate();
        }

        //Autovalida
        public bool Validate()
            => base.Validate(new GunValidator(), this);
    }
}