using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.RestClients;
using Ordsome.Services.CrossCuttingConcerns.Languages;
using RestEase;

namespace RequestService.Application.RestClients
{
    public interface IUserServiceClient
    {
        [Path("userId")]
        Guid UserId { get; set; }

        [Get("check/{userId}")]
        Task<bool> CheckUserId([Path] Guid userId);


        [Get("{userId}")]
        Task<UserDto> GetUserDetails([Path] Guid userId);
    }
}