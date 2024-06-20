using AutoMapper;
using FluentValidation;
using Infrastructure.Base;

namespace Application.Base
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        private readonly IBaseRepository<TEntity> _baseRepository;
        private readonly IMapper _mapper;

        public BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }
        
        public IEnumerable<TOutputModel> Add<TInputModel, TOutputModel, TValidator>(IEnumerable<TInputModel> inputModels)
            where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TOutputModel> Update<TInputModel, TOutputModel, TValidator>(IEnumerable<TInputModel> inputModels)
            where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class
        {
           throw new NotImplementedException();
        }

        public void Delete(string id) => _baseRepository.Delete(id);

        public IEnumerable<TOutputModel> Get<TOutputModel>() where TOutputModel : class
        {
            throw new NotImplementedException();
        }

        private void Validate(TEntity obj, AbstractValidator<TEntity> validator)
        {
            if (obj == null)
                throw new Exception("Registros não detectados!");

            validator.ValidateAndThrow(obj);
        }
    }
}
