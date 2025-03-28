﻿
using Domain.Entities;
using FluentValidation;

namespace Application.Interfaces
{
    public interface ICategoriaService
    {
        Task DeleteAsync(int id);
        Task<IEnumerable<TOutputModel>> GetAsync<TOutputModel>() where TOutputModel : class;
        Task<TOutputModel> GetAsync<TOutputModel>(int id) where TOutputModel : class;
        Task<TOutputModel> InsertAsync<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TInputModel : class
            where TOutputModel : class
            where TValidator : AbstractValidator<Categoria>;
        Task<TOutputModel> UpdateAsync<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TInputModel : class
            where TOutputModel : class
            where TValidator : AbstractValidator<Categoria>;
    }
}
