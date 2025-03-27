
using Domain.Entities;
using FluentValidation;

namespace Application.Interfaces
{
    public interface IFaixaService
    {
        Task DeleteAsync(int id);
        Task<IEnumerable<TOutputModel>> GetAsync<TOutputModel>() where TOutputModel : class;
        Task<IEnumerable<TOutputModel>> GetAsync<TOutputModel>(int categoriaId) where TOutputModel : class;
        Task<TOutputModel> GetByIdAsync<TOutputModel>(int id) where TOutputModel : class;
        Task<TOutputModel> InsertAsync<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TInputModel : class
            where TOutputModel : class
            where TValidator : AbstractValidator<Faixa>;
        Task<TOutputModel> UpdateAsync<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TInputModel : class
            where TOutputModel : class
            where TValidator : AbstractValidator<Faixa>;
    }
}
