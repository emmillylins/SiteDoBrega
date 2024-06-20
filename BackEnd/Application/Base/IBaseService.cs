using FluentValidation;

namespace Application.Base
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        IEnumerable<TOutputModel> Get<TOutputModel>() where TOutputModel : class;
        IEnumerable<TOutputModel> Add<TInputModel, TOutputModel, TValidator>(IEnumerable<TInputModel> inputModel) where TInputModel : class where TOutputModel : class where TValidator : AbstractValidator<TEntity>;
        IEnumerable<TOutputModel> Update<TInputModel, TOutputModel, TValidator>(IEnumerable<TInputModel> inputModel) where TInputModel : class where TOutputModel : class where TValidator : AbstractValidator<TEntity>;
        void Delete(string id);
    }
}
