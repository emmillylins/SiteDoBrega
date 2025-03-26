using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using Infrastructure.Context;
using Infrastructure.Interfaces;

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

        public async Task<TOutputModel> GetAsync<TOutputModel>(int id) where TOutputModel : class
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
        }

        public TOutputModel Update<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TInputModel : class
            where TOutputModel : class
            where TValidator : AbstractValidator<Faixa>
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                // Mapeamento da entidade
                var entity = _mapper.Map<Faixa>(inputModel) ?? throw new Exception("Erro de mapping.");

                Validation<TValidator>(entity);

                // Atualizar a entidade
                _repository.Update(entity);
                _context.SaveChanges();

                transaction.Commit();

                // Mapear o resultado para o modelo de saída
                var outputModel = _mapper.Map<TOutputModel>(entity);
                return outputModel;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Delete(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var entity = _repository.Select(id) ?? throw new NotFoundException("Registro não existe na base de dados.");

                _repository.Delete(entity);
                _context.SaveChanges();

                // Commit da transação
                transaction.Commit();
            }
            catch (Exception)
            {
                // Rollback em caso de erro
                transaction.Rollback();
                throw;
            }
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
