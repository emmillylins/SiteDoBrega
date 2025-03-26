//using Application.Exceptions;
//using Application.Interfaces;
//using AutoMapper;
//using Domain.Entities;
//using Domain.Enums;
//using FluentValidation;
//using Infrastructure.Context;
//using Infrastructure.Interfaces;
//using System.Globalization;

//namespace Application.Services
//{
//    public class UsuarioService : IUsuarioService
//    {
//        private readonly IBaseRepository<Usuario> _repository;
//        private readonly IBaseRepository<Faixa> _faixaRepository;
//        private readonly IBaseRepository<ApplicationUser> _userRepository;
//        private readonly DataDbContext _context;
//        private readonly IMapper _mapper;

//        public UsuarioService(IBaseRepository<Usuario> repository, IMapper mapper, DataDbContext context, IBaseRepository<ApplicationUser> userRepository)
//        {
//            _repository = repository;
//            _mapper = mapper;
//            _context = context;
//            _userRepository = userRepository;
//        }

//        public async Task<IEnumerable<TOutputModel>> GetAsync<TOutputModel>() where TOutputModel : class
//        {
//            try
//            {
//                var entities = await _repository.SelectAsync();
//                var faixas = await _faixaRepository.SelectAsync();
                
//                var activeEntities = entities.Where(e => e.Status == Status.Ativo).ToList();
//                activeEntities.ForEach(entity => entity.Faixas = faixas.Where(p => p.UsuarioId == entity.Id).ToList());

//                var outputModels = activeEntities.Select(_mapper.Map<TOutputModel>);
//                return outputModels;
//            }
//            catch (Exception) { throw; }
//        }

//        public async Task<TOutputModel> GetAsync<TOutputModel>(int id) where TOutputModel : class
//        {
//            try
//            {
//                var faixas = await _faixaRepository.SelectAsync();

//                var entity = await _repository.SelectAsync(id) ?? throw new NotFoundException("Usuario não encontrada.");
//                entity.Faixas = faixas.Where(p => p.UsuarioId == entity.Id).ToList();

//                var outputModel = _mapper.Map<TOutputModel>(entity);
//                return outputModel;
//            }
//            catch (Exception) { throw; }
//        }

//        public async Task<TOutputModel> InsertAsync<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
//            where TInputModel : class
//            where TOutputModel : class
//            where TValidator : AbstractValidator<Usuario>
//        {
//            using var transaction = await _context.Database.BeginTransactionAsync();
//            try
//            {
//                var entity = _mapper.Map<Usuario>(inputModel);

//                // Validação da entidade
//                Validation<TValidator>(entity);
//                if (entity.DataNasc is not null) ValidaDataNasc(entity.DataNasc);

//                var usuarios = await _repository.SelectAsync();

//                if (usuarios.FirstOrDefault(u => u.NomeUsuario == entity.NomeUsuario) is not null) 
//                    throw new ConflictException($"O nome de usuário {entity.NomeUsuario} já existe.");

//                // Inserir nova Usuario
//                await _repository.InsertAsync(entity);
//                await _context.SaveChangesAsync();

//                // Commit da transação
//                await transaction.CommitAsync();

//                var outputModel = _mapper.Map<TOutputModel>(entity);
//                return outputModel;
//            }
//            catch (Exception)
//            {
//                await transaction.RollbackAsync();
//                throw;
//            }
//        }

//        public TOutputModel Update<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
//            where TInputModel : class
//            where TOutputModel : class
//            where TValidator : AbstractValidator<Usuario>
//        {
//            using var transaction = _context.Database.BeginTransaction();
//            try
//            {
//                // Mapeamento da entidade
//                var entity = _mapper.Map<Usuario>(inputModel) ?? throw new Exception("Erro de mapping.");

//                // Verificar o applicationUser
//                var user = _userRepository.Select().FirstOrDefault(u => u.UserName == entity.NomeUsuario);
//                if (user is not null && user.TipoUsuario != TipoUsuario.Administrador)
//                    throw new ForbiddenException("Apenas o administrador pode atualizar dados.");

//                // Buscar a entidade existente
//                var dbEntity = _repository.Select().FirstOrDefault(u => u.NomeUsuario == entity.NomeUsuario) 
//                    ?? throw new NotFoundException("Registro não existe na base de dados.");

//                // Validação
//                Validation<TValidator>(entity);
//                if (entity.DataNasc is not null) ValidaDataNasc(entity.DataNasc);

//                // Atualizar a entidade
//                _repository.Update(entity);
//                _context.SaveChanges();

//                transaction.Commit();

//                // Mapear o resultado para o modelo de saída
//                var outputModel = _mapper.Map<TOutputModel>(entity);
//                return outputModel;
//            }
//            catch (Exception)
//            {
//                transaction.Rollback();
//                throw;
//            }
//        }

//        public void Delete(int id)
//        {
//            using var transaction = _context.Database.BeginTransaction();
//            try
//            {
//                // Busca a entidade a ser deletada
//                var entity = _repository.Select().FirstOrDefault(u => u.Id == id) 
//                    ?? throw new NotFoundException("Registro não existe na base de dados.");

//                // Verifica o tipo de usuário
//                var user = _userRepository.Select().FirstOrDefault(u => u.UserName == entity.NomeUsuario);
//                if (user is not null && user.TipoUsuario != TipoUsuario.Administrador)
//                    throw new ForbiddenException("Apenas o administrador pode excluir dados.");

//                // exclusão lógica da entidade
//                entity.Status = Status.Inativo;
//                _repository.Update(entity); 
//                _context.SaveChanges();

//                // Commit da transação
//                transaction.Commit();
//            }
//            catch (Exception)
//            {
//                // Rollback em caso de erro
//                transaction.Rollback();
//                throw;
//            }
//        }

//        #region métodos auxiliares
//        private static void Validation<TValidator>(Usuario entity) where TValidator : AbstractValidator<Usuario>
//        {
//            try
//            {
//                var validator = Activator.CreateInstance<TValidator>();
//                var result = validator.Validate(entity);

//                if (!result.IsValid)
//                {
//                    var errors = result.Errors.Select(error => new string(error.ErrorMessage));
//                    var errorString = string.Join(Environment.NewLine, errors);

//                    throw new Exception(errorString);
//                }
//            }
//            catch (Exception) { throw; }
//        }

//        public void ValidaDataNasc(string dataNasc)
//        {
//            bool dataValida = true;

//            // Tenta extrair o dia, mês e ano da string
//            if (!int.TryParse(dataNasc.Substring(0, 2), out int day) ||
//                !int.TryParse(dataNasc.Substring(2, 2), out int month) ||
//                !int.TryParse(dataNasc.Substring(4, 4), out int year))
//            {
//                dataValida = false;
//            }

//            // Verifica se a data é válida
//            DateTime parsedDate;
//            if (!DateTime.TryParseExact(dataNasc, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
//            {
//                dataValida = false;
//            }

//            // Verifica se a data não é no futuro
//            if (parsedDate > DateTime.Now)
//            {
//                dataValida = false;
//            }

//            if (!dataValida) throw new Exception($"Data de Nascimento {dataNasc} inválida");
//        }
//        #endregion
//    }
//}
