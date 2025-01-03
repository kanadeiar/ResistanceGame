using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ResistanceGame.Data;
using ResistanceGame.Model;

namespace ResistanceGame.Web.Models;

public class RegistrationWebModel
{
    [DisplayName("Имя")]
    [Required(ErrorMessage = "Пожалуйста, введите ваше имя")]
    [StringLength(30, ErrorMessage = "Имя должно быть короче 30 символов")]
    public string? Name { get; set; }

    public Player Map()
    {
        return new Player
        {
            Name = Name,
        };
    }

    public void Validate(ModelStateDictionary modelState)
    {
        if (PlayersRepository.All.Any(x => x.Name == Name))
        {
            modelState.AddModelError("Name", "Такое имя уже занято");
        }
    }
}