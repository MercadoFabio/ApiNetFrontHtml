using FluentValidation;
using Parcial.Dtos;

namespace Parcial.Validators
{
    public class AlbanilValidator : AbstractValidator<AlbanilDto>
    {
        public AlbanilValidator()
        {
            RuleFor(a => a.Nombre).NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(a => a.Apellido).NotEmpty().WithMessage("El apellido es obligatorio");
            RuleFor(a => a.Dni).NotEmpty().WithMessage("El DNI es obligatorio");
        }
       
    }
}
