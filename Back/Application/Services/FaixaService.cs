using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class FaixaService : IFaixaService
    {
        private readonly IBaseRepository<Faixa> _repository;
        private readonly DataDbContext _context;
        private readonly IMapper _mapper;

        public FaixaService(IBaseRepository<Faixa> repository, IMapper mapper, DataDbContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<TOutputModel>> GetAsync<TOutputModel>() where TOutputModel : class
        {
            try
            {
                var entities = await _repository.SelectAsync();

                var outputModels = entities.Select(_mapper.Map<TOutputModel>);
                return outputModels;
            }
            catch (Exception) { throw; }
        }

        public async Task<IEnumerable<TOutputModel>> GetAsync<TOutputModel>(int categoriaId) where TOutputModel : class
        {
            try
            {
                var entities = await _repository.SelectAsync();

                var outputModels = entities.Where(e => e.CategoriaId == categoriaId).Select(_mapper.Map<TOutputModel>);
                return outputModels;
            }
            catch (Exception) { throw; }
        }

        public async Task<TOutputModel> GetByIdAsync<TOutputModel>(int id) where TOutputModel : class
        {
            try
            {
                var entity = await _repository.SelectAsync(id) ?? throw new NotFoundException("Faixa não encontrada.");

                var outputModel = _mapper.Map<TOutputModel>(entity);
                return outputModel;
            }
            catch (Exception) { throw; }
        }

        public async Task<TOutputModel> InsertAsync<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TInputModel : class
            where TOutputModel : class
            where TValidator : AbstractValidator<Faixa>
        {
            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    var entity = _mapper.Map<Faixa>(inputModel);
                    Validation<TValidator>(entity);

                    var faixas = await _repository.SelectAsync();

                    // Inserir nova Faixa
                    await _repository.InsertAsync(entity);
                    await _context.SaveChangesAsync();

                    // Commit da transação
                    await transaction.CommitAsync();

                    var outputModel = _mapper.Map<TOutputModel>(entity);
                    return outputModel;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }

        public async Task<TOutputModel> UpdateAsync<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
                  where TInputModel : class
                  where TOutputModel : class
                  where TValidator : AbstractValidator<Faixa>
        {
            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    // Mapeamento da entidade
                    var entity = _mapper.Map<Faixa>(inputModel) ?? throw new Exception("Erro de mapeamento.");

                    Validation<TValidator>(entity);

                    // Atualizar a entidade
                    _repository.Update(entity);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    // Mapear o resultado para o modelo de saída
                    var outputModel = _mapper.Map<TOutputModel>(entity);
                    return outputModel;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }

        public async Task DeleteAsync(int id)
        {
            var strategy = _context.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    var entity = await _repository.SelectAsync(id)
                        ?? throw new NotFoundException("Registro não existe na base de dados.");

                    _repository.Delete(entity);
                    await _context.SaveChangesAsync();

                    // Commit da transação
                    await transaction.CommitAsync();
                }
                catch
                {
                    // Rollback em caso de erro
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }

        #region métodos auxiliares
        private static void Validation<TValidator>(Faixa entity) where TValidator : AbstractValidator<Faixa>
        {
            try
            {
                var validator = Activator.CreateInstance<TValidator>();
                var result = validator.Validate(entity);

                if (!result.IsValid)
                {
                    var errors = result.Errors.Select(error => new string(error.ErrorMessage));
                    var errorString = string.Join(Environment.NewLine, errors);

                    throw new Exception(errorString);
                }
            }
            catch (Exception) { throw; }
        }
        #endregion
    }
}
