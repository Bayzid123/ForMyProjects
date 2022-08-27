using ApiForMyProjects.DTO;
using ApiForMyProjects.Helper;
using LanguageExt.Common;

namespace ApiForMyProjects.IRepository
{
    public interface IUser
    {
        public Task<Result<MessageHelper>> CreateUser(CreateUserDTO objCreate);
    }
}
