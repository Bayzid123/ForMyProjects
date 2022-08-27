using ApiForMyProjects.DbContexts;
using ApiForMyProjects.DTO;
using ApiForMyProjects.Helper;
using ApiForMyProjects.IRepository;
using ApiForMyProjects.Models;
using DocumentFormat.OpenXml.Wordprocessing;
using LanguageExt.Common;
using System.ComponentModel.DataAnnotations;

namespace ApiForMyProjects.Repository
{
    public class User : IUser
    {
        public readonly Context _context;

        public User(Context context)
        {
            _context = context;
        }

        public async Task<Result<MessageHelper>> CreateUser(CreateUserDTO objCreate)
        {
            try
            {
                var result = _context.TblUsers.Where(x => x.StrUserName.Trim().ToLower() == objCreate.StrUserName.Trim().ToLower() && x.IsActive == true).FirstOrDefault();

                if (result != null)
                    return new Result<MessageHelper>(new ValidationException("User Name Already Exist! Try Something Different."));

                var detalis = new TblUser
                {
                    StrUserName = objCreate.StrUserName,
                    StrEmail = objCreate.StrEmail,
                    StrPassword = objCreate.StrPassword,
                    IsActive = true,
                    DteCreatedAt = DateTime.Now,
                    IntCreatedBy = objCreate.IntCreatedBy,
                    DteUpdatedAt = DateTime.Now,
                    IntUpdatedBy = objCreate.IntUpdatedBy,
                };
                await _context.TblUsers.AddAsync(detalis);
                await _context.SaveChangesAsync();

                return new MessageHelper()
                {
                    Message = "Successfully Created",
                    statuscode = 200
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
