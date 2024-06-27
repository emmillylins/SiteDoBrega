using Domain.Entities;
using Application.Interfaces;
using FluentValidation;
using AutoMapper;
using Infrastructure.Interfaces;
using Infrastructure.Base;
using Application.Exceptions;

namespace Application.Services
{
    public class CategoriaService<TEntity> : ICategoriaService where TEntity : class
    {
        private readonly ICategoriaRepository _repository;
        private readonly IFaixaRepository _faixaRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoriaService(ICategoriaRepository repository, IFaixaRepository faixaRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _faixaRepository = faixaRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<TOutputModel> Get<TOutputModel>() where TOutputModel : class
        {
            try
            {
                var entities = _repository.Select().Where(e => e.Ativo is true).ToList();
                entities.ForEach(entity => entity.Faixas = _faixaRepository.Select().Where(p => p.CategoriaId == entity.Id).ToList());

                var outputModels = entities.Select(s => _mapper.Map<TOutputModel>(s));
                return outputModels;
            }
            catch (Exception) { throw; }
        }

        public IEnumerable<TOutputModel> Add<TInputModel, TOutputModel, TValidator>(IEnumerable<TInputModel> inputModels)
            where TInputModel : class
            where TOutputModel : class
            where TValidator : AbstractValidator<Categoria>
        {
            try
            {
                var entities = inputModels.Select(inputModel => _mapper.Map<Categoria>(inputModel)).ToList();

                foreach (var entity in entities)
                {
                    Validation<TValidator>(entity);

                    if (_repository.Select(entity.Id) is not null) throw new ConflictException($"O usuário {entity.Id} já existe.");

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
            where TValidator : AbstractValidator<Categoria>
        {
            try
            {
                var entities = inputModels.Select(inputModel => _mapper.Map<Categoria>(inputModel)).ToList();

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
                var entity = _repository.Select(int.Parse(id));

                if (entity is null) throw new NotFoundException("Registro não existe na base de dados.");

                entity.Ativo = false;
                _repository.Update(entity);
                _unitOfWork.Commit();
            }
            catch (Exception) { throw; }
        }

        public void Validation<TValidator>(Categoria entity) where TValidator : AbstractValidator<Categoria>
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
    }
}
