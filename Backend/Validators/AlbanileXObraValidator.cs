using FluentValidation;
using Parcial.Dtos;

namespace Parcial.Validators
{
    public class AlbanileXObraValidator : AbstractValidator<AlbanilXObraDto>
    {
        public AlbanileXObraValidator()
        {
            RuleFor(a => a.IdAlbanil).NotEmpty().WithMessage("El albañil es obligatorio.");
            RuleFor(a => a.IdObra).NotEmpty().WithMessage("La obra es obligatoria.");
            RuleFor(a => a.TareaArealizar).NotEmpty().WithMessage("La tarea a realizar es obligatoria.");
        }
    }
}
