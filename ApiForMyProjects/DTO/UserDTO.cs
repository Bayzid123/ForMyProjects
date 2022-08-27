﻿namespace ApiForMyProjects.DTO
{
    public class UserDTO
    {
    }

    public class CreateUserDTO
    {
        public long IntUserId { get; set; }
        public string StrUserName { get; set; }
        public string StrEmail { get; set; }
        public string StrPassword { get; set; }
        public bool IsActive { get; set; }
        public DateTime DteCreatedAt { get; set; }
        public long? IntCreatedBy { get; set; }
        public DateTime? DteUpdatedAt { get; set; }
        public long? IntUpdatedBy { get; set; }
    }
}
