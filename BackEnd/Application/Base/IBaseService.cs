using FluentValidation;

namespace Application.Base
{
    public interface IBaseService<TEntity> where TEntity : class 
    { 
        TOutputModel Add<TInputModel, TOutputModel, TValidator>(TInputModel inputModel) where TInputModel : class where TOutputModel : class where TValidator : AbstractValidator<TEntity>;
        TOutputModel Update<TInputModel, TOutputModel, TValidator>(TInputModel inputModel) where TInputModel : class where TOutputModel : class where TValidator : AbstractValidator<TEntity>;
        void Delete(int id);
    }
}
