using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserById(int id);
        Task<AppUser> GetUserbyUsernameAsync(string username);
        Task<IEnumerable<MemberDto>> getMembersAsync();
        Task<MemberDto> GetMemberAsync(string username);
    }
}