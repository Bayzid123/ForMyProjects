using ApiForMyProjects.DbContexts;
using ApiForMyProjects.DTO;
using ApiForMyProjects.Helper;
using ApiForMyProjects.IRepository;
using ApiForMyProjects.Models;
using DocumentFormat.OpenXml.Wordprocessing;
using LanguageExt.Common;
using MailKit.Search;
using Microsoft.EntityFrameworkCore;
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






        #region AdPlay Written Test

        //Create and Edit Track

        public async Task<MessageHelper> SaveTrack(SaveTrackDTO obj)
        {
            try
            {
                TblTrack dataParent = await _context.TblTracks.Where(x => x.IntTrackId == obj.IntTrackId
                && x.IsActive == true).FirstOrDefaultAsync();

                var msg = new MessageHelper();

                if (dataParent == null)
                {
                    TblTrack data = new TblTrack
                    {
                        IntTrackId = obj.IntTrackId,
                        StrTrackName = obj.StrTrackName,
                        IntAlbumId = obj.IntAlbumId,
                        StrComposer = obj.StrComposer,
                        IsActive = true,
                        DteCreatedAt = DateTime.Now,
                    };
                    await _context.TblTracks.AddAsync(data);
                    await _context.SaveChangesAsync();

                    msg.Message = "Created Successfully";
                    msg.statuscode = 200;
                    return msg;
                }

                dataParent.StrTrackName = obj.StrTrackName;
                dataParent.IntAlbumId = obj.IntAlbumId;
                dataParent.StrComposer = obj.StrComposer;
                dataParent.IsActive = obj.IsActive;
                dataParent.DteCreatedAt = obj.DteCreatedAt;

                _context.TblTracks.Update(dataParent);
                await _context.SaveChangesAsync();

                msg.Message = "Updated Successfully";
                msg.statuscode = 200;
                return msg;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        // Get Track List by searching Track Name
        public async Task<List<SaveTrackDTO>> GetTrackList(string search)
        {
            var data = await (from t in _context.TblTracks
                              where t.IsActive == true && (string.IsNullOrWhiteSpace(search) || t.StrTrackName.Contains(search))
                              orderby t.IntTrackId descending
                              select new SaveTrackDTO
                              {
                                  IntTrackId = t.IntTrackId,
                                  StrTrackName = t.StrTrackName,
                                  IntAlbumId = t.IntAlbumId,
                                  StrComposer = t.StrComposer,
                                  IsActive = t.IsActive,
                                  DteCreatedAt = t.DteCreatedAt,
                              }).ToListAsync();
            return data;
        }


        // Delete Track 

        public async Task<MessageHelper> DeleteTrack(long id)
        {
            try
            {
                // Delete from database
                var msg = new MessageHelper();
                var deleteTrack = await _context.TblTracks.Where(x => x.IntTrackId == id).FirstOrDefaultAsync();
                if (deleteTrack == null)
                    throw new Exception("Track Not Found");

                _context.TblTracks.Remove(deleteTrack);
                await _context.SaveChangesAsync();

                msg.Message = "Deleted Successfully";
                msg.statuscode = 200;
                return msg;



                // For Delete we just inActive Data from our database table,, so we can update just isActive Field.


                //var updateTrack = await _context.TblTracks.Where(x => x.IntTrackId == id).FirstOrDefaultAsync();
                //if (updateTrack == null)
                //    throw new Exception("Track Not Found");

                //updateTrack.IsActive = false;

                //_context.TblTracks.Update(updateTrack);
                //await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
