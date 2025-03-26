
using Domain.Entities;
using FluentValidation;

namespace Application.Interfaces
{
    public interface IMultaService
    {
        void Delete(Guid id, string? username);
        Task<IEnumerable<TOutputModel>> GetAsync<TOutputModel>() where TOutputModel : class;
        Task<TOutputModel> GetAsync<TOutputModel>(Guid id) where TOutputModel : class;
        Task<TOutputModel> InsertAsync<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TInputModel : class
            where TOutputModel : class
            where TValidator : AbstractValidator<Multa>;
        TOutputModel Update<TInputModel, TOutputModel, TValidator>(TInputModel inputModel, string? username)
            where TInputModel : class
            where TOutputModel : class
            where TValidator : AbstractValidator<Multa>;
    }
}
