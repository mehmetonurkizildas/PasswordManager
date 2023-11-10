using Application.Services.Abstract;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Dtos.Category;
using Domain.Dtos.PasswordCollection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Concrete
{
    public class PasswordCollectionManager : IPasswordCollectionService
    {
        private readonly IPasswordCollectionRepository _passwordCollectionRepository;
        private readonly IMapper _mapper;

        public PasswordCollectionManager(IPasswordCollectionRepository passwordCollectionRepository, IMapper mapper)
        {
            _passwordCollectionRepository = passwordCollectionRepository;
            _mapper = mapper;
        }

        public async Task<PasswordCollectionDto?> DeletePasswordCollection(long accountId, long id)
        {
            var passwordCollection = await _passwordCollectionRepository.Get(x => x.Id == id && x.AccountId == accountId);
            if (passwordCollection == null)
            {
                return null;
            }
            else
            {
                passwordCollection.IsActive = false;
                var updatedResult = await _passwordCollectionRepository.Update(passwordCollection);
                if (updatedResult != null)
                {
                    return _mapper.Map<PasswordCollectionDto>(updatedResult);
                }
            }
            return null;
        }

        public async Task<PasswordCollectionDto> GetPasswordCollection(long accountId, long? id)
        {
            var passwordCollection = await _passwordCollectionRepository.Get(x => x.AccountId == accountId && x.Id == id);
            if (passwordCollection != null)
            {
                return _mapper.Map<PasswordCollectionDto>(passwordCollection);
            }
            return new PasswordCollectionDto();
        }

        public async Task<PasswordCollectionDto?> PasswordCollectionAddOrUpdate(long accountId, PasswordCollectionDto passwordCollectionDto)
        {
            if (passwordCollectionDto.Id > 0)
            {
                var passwordCollection = await _passwordCollectionRepository.Get(x => x.Id == passwordCollectionDto.Id && x.AccountId == accountId);
                if (passwordCollection == null)
                {
                    return null;
                }

                passwordCollection = _mapper.Map<PasswordCollection>(passwordCollectionDto);
                passwordCollection.AccountId = accountId;
                var updatedResult = await _passwordCollectionRepository.Update(passwordCollection);
                if (updatedResult != null)
                {
                    return passwordCollectionDto;
                }
            }
            else
            {
                var addedPasswordCollection = _mapper.Map<PasswordCollection>(passwordCollectionDto);
                addedPasswordCollection.AccountId = accountId;
                var addResult = await _passwordCollectionRepository.Add(addedPasswordCollection);
                if (addResult != null)
                {
                    return passwordCollectionDto;
                }
            }
            return null;
        }

        public async Task<List<PasswordListDto>> PasswordList(long accountId)
        {
            var data = await _passwordCollectionRepository.GetList(x => x.AccountId == accountId && x.IsActive == true, include: x => x.Include(a => a.Category));
            return _mapper.Map<List<PasswordListDto>>(data);
        }
    }
}
