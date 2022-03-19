using System.ComponentModel.DataAnnotations;
using R6.Core.Enums;

namespace R6.API.ViewModels
{
    public class CreateOperatorViewModel
    {
        [Required(ErrorMessage = "O Nome não pode ser vazio.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "A Dificuldade não pode ser vazia.")]
        [EnumDataType(typeof(DificultType), ErrorMessage ="Informe uma Dificuldade válida")]
        public string? Dificult { get; set; }

        [Required(ErrorMessage = "A Armaduta não pode ser vazia.")]
        [EnumDataType(typeof(ArmorType), ErrorMessage ="Informe uma armadura válida")]

        public string? Armor { get; set; }

        [Required(ErrorMessage = "A Velocidade não pode ser vazia.")]
        [EnumDataType(typeof(SpeedType), ErrorMessage ="Informe uma velocidade válida")]

        public string? Speed { get; set; }
    }
}