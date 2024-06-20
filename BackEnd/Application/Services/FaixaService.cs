using Domain.Entities;
using Application.Interfaces;
using FluentValidation;
using AutoMapper;
using Infrastructure.Interfaces;
using Infrastructure.Base;
using Application.Exceptions;

namespace Application.Services
{
    public class FaixaService<TEntity> : IFaixaService where TEntity : class
    {
        private readonly IFaixaRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FaixaService(IFaixaRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<TOutputModel> Get<TOutputModel>() where TOutputModel : class
        {
            try
            {
                var entities = _repository.Select().ToList();

                entities.ForEach(entity =>
                {
                    entity.Usuario = _repository.GetUsuarios().First(p => p.NomeUsuario == entity.NomeUsuario);
                    entity.Categoria = _repository.GetCategorias().First(p => p.Id == entity.CategoriaId);
                });

                var outputModels = entities.Select(s => _mapper.Map<TOutputModel>(s));
                return outputModels;
            }
            catch (Exception) { throw; }
        }

        public IEnumerable<TOutputModel> Add<TInputModel, TOutputModel, TValidator>(IEnumerable<TInputModel> inputModels)
            where TInputModel : class
            where TOutputModel : class
            where TValidator : AbstractValidator<Faixa>
        {
            try
            {
                var entities = inputModels.Select(inputModel => _mapper.Map<Faixa>(inputModel)).ToList();

                foreach (var entity in entities)
                {
                    Validation<TValidator>(entity);

                    if (_repository.Select(entity.Id) is not null) throw new ConflictException($"O Registro {entity.Id} já existe.");

                    _repository.Insert(entity);
                }
                _unitOfWork.Commit();

                var outputModels = entities.Select(entity => _mapper.Map<TOutputModel>(entity));
                return outputModels;
            }
            catch (Exception) { throw; }
        }

        public IEnumerable<TOutputModel> Update<TInputModel, TOutputModel, TValidator>(IEnumerable<TInputModel> inputModels)
            where TInputModel : class
            where TOutputModel : class
            where TValidator : AbstractValidator<Faixa>
        {
            try
            {
                var entities = inputModels.Select(inputModel => _mapper.Map<Faixa>(inputModel)).ToList();

                foreach (var entity in entities)
                {
                    Validation<TValidator>(entity);

                    if (_repository.Select(entity.Id) is null) throw new NotFoundException("Registro não existe na base de dados.");
                    _repository.Update(entity);
                }
                _unitOfWork.Commit();

                var outputModels = entities.Select(entity => _mapper.Map<TOutputModel>(entity));
                return outputModels;
            }
            catch (Exception) { throw; }
        }

        public void Delete(string id)
        {
            try
            {
                if (_repository.Select(int.Parse(id)) is null) throw new NotFoundException("Registro não existe na base de dados;");

                _repository.Delete(int.Parse(id));
                _unitOfWork.Commit();
            }
            catch (Exception) { throw; }
        }

        public void Validation<TValidator>(Faixa entity) where TValidator : AbstractValidator<Faixa>
        {
            try
            {
                var validator = Activator.CreateInstance<TValidator>();
                var result = validator.Validate(entity);

                if (!result.IsValid)
                {
                    var errors = result.Errors.Select(error => new { Property = error.PropertyName, Message = error.ErrorMessage });
                    var errorString = string.Join(", ", errors);

                    throw new Exception(errorString);
                }
            }
            catch (Exception) { throw; }
        }
    }
}
