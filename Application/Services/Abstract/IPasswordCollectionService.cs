using Domain.Dtos.PasswordCollection;

namespace Application.Services.Abstract
{
    public interface IPasswordCollectionService
    {
        Task<List<PasswordListDto>> PasswordList(long accountId);
        Task<PasswordCollectionDto> GetPasswordCollection(long accountId, long? id);
        Task<PasswordCollectionDto?> DeletePasswordCollection(long accountId, long id);
        Task<PasswordCollectionDto?> PasswordCollectionAddOrUpdate(long accountId, PasswordCollectionDto passwordCollectionDto);
    }
}
