using ApiForMyProjects.DTO;
using ApiForMyProjects.Helper;
using LanguageExt.Common;

namespace ApiForMyProjects.IRepository
{
    public interface IUser
    {
        public Task<Result<MessageHelper>> CreateUser(CreateUserDTO objCreate);

        #region AdPlay Written Test
        public Task<Result<MessageHelper>> SaveTrack(SaveTrackDTO obj);
        public Task<List<SaveTrackDTO>> GetTrackList(string search);
        #endregion
    }
}
