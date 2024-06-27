using Domain.Entities;
using Application.Interfaces;
using FluentValidation;
using AutoMapper;
using Infrastructure.Interfaces;
using Infrastructure.Base;
using Application.Exceptions;
using System.Globalization;

namespace Application.Services
{
    public class UsuarioService<TEntity> : IUsuarioService where TEntity : class
    {
        private readonly IUsuarioRepository _repository;
        private readonly IFaixaRepository _faixaRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository repository, IFaixaRepository faixaRepository, IUnitOfWork unitOfWork, IMapper mapper)
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
                entities.ForEach(entity => entity.Faixas = _faixaRepository.Select().Where(p => p.NomeUsuario == entity.NomeUsuario).ToList());

                var outputModels = entities.Select(s => _mapper.Map<TOutputModel>(s));
                return outputModels;
            }
            catch (Exception) { throw; }
        }

        public IEnumerable<TOutputModel> Add<TInputModel, TOutputModel, TValidator>(IEnumerable<TInputModel> inputModels)
            where TInputModel : class
            where TOutputModel : class
            where TValidator : AbstractValidator<Usuario>
        {
            try
            {
                var entities = inputModels.Select(inputModel => _mapper.Map<Usuario>(inputModel)).ToList();

                foreach (var entity in entities)
                {
                    Validation<TValidator>(entity);
                    if (entity.DataNasc is not null) ValidaDataNasc(entity.DataNasc);

                    if (_repository.Select(entity.NomeUsuario) is not null) throw new ConflictException($"O usuário {entity.NomeUsuario} já existe.");

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
            where TValidator : AbstractValidator<Usuario>
        {
            try
            {
                var entities = inputModels.Select(inputModel => _mapper.Map<Usuario>(inputModel)).ToList();

                foreach (var entity in entities)
                {
                    Validation<TValidator>(entity);
                    if (entity.DataNasc is not null) ValidaDataNasc(entity.DataNasc);

                    if (_repository.Select(entity.NomeUsuario) is null) throw new NotFoundException("Registro não existe na base de dados.");
                    _repository.Update(entity);
                }
                _unitOfWork.Commit();

                var outputModels = entities.Select(entity => _mapper.Map<TOutputModel>(entity));
                return outputModels;
            }
            catch (Exception) { throw; }
        }

        public void Delete(string username)
        {
            try
            {
                var entity = _repository.Select(username);

                if (entity is null) throw new NotFoundException("Registro não existe na base de dados.");

                entity.Ativo = false;
                _repository.Update(entity);
                _unitOfWork.Commit();
            }
            catch (Exception) { throw; }
        }

        #region Validações
        public void Validation<TValidator>(Usuario entity) where TValidator : AbstractValidator<Usuario>
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

        public void ValidaDataNasc(string dataNasc)
        {
            bool dataValida = true;

            // Tenta extrair o dia, mês e ano da string
            if (!int.TryParse(dataNasc.Substring(0, 2), out int day) ||
                !int.TryParse(dataNasc.Substring(2, 2), out int month) ||
                !int.TryParse(dataNasc.Substring(4, 4), out int year))
            {
                dataValida = false;
            }

            // Verifica se a data é válida
            DateTime parsedDate;
            if (!DateTime.TryParseExact(dataNasc, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                dataValida = false;
            }

            // Verifica se a data não é no futuro
            if (parsedDate > DateTime.Now)
            {
                dataValida = false;
            }

            if (!dataValida) throw new Exception($"Data de Nascimento {dataNasc} inválida");
        }
        #endregion
    }
}
