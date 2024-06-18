using Domain.Entities;
using Application.Interfaces;
using FluentValidation;
using AutoMapper;
using Infrastructure.Interfaces;
using Infrastructure.Base;

namespace Application.Services
{
    public class UsuarioService<TEntity> : IUsuarioService where TEntity : class
    {
        private readonly IUsuarioRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public TOutputModel Add<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TInputModel : class
            where TOutputModel : class
            where TValidator : AbstractValidator<Usuario>
        {
            Usuario entity = _mapper.Map<Usuario>(inputModel);
            IsValid<TValidator>(entity);
            
            _repository.Insert(entity);

            _unitOfWork.Commit();
            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);

            return outputModel;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
            _unitOfWork.Commit();
        }

        public TOutputModel Update<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TInputModel : class
            where TOutputModel : class
            where TValidator : AbstractValidator<Usuario>
        {
            Usuario entity = _mapper.Map<Usuario>(inputModel);
            IsValid<TValidator>(entity);

            _repository.Update(entity);

            _unitOfWork.Commit();
            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);

            return outputModel;
        }

        public void IsValid<TValidator>(Usuario entity)
            where TValidator : AbstractValidator<Usuario>
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
