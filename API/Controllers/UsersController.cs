using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace API.Controllers
{
    [Authorize]
    public class UsersController: BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IPhotoService _photoService;
        public UsersController(IUserRepository userRepository,IMapper mapper, IPhotoService photoService){
            _photoService = photoService;
            _mapper = mapper;
            _userRepository = userRepository;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUser(){
            var users = await _userRepository.getMembersAsync();
            return Ok(users);
            
        }
        
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username){
        
         return await _userRepository.GetMemberAsync(username);
         
            
        }
        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto){
            var username = User.GetUsername();
            var user= await _userRepository.GetUserbyUsernameAsync(username);
            if(user == null) return NotFound();

            _mapper.Map(memberUpdateDto, user);
            if(await _userRepository.SaveAllAsync()) return NoContent();
            return BadRequest("Fail!!");
        }
        [HttpPost("add-phot")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file){
            var user = await _userRepository.GetUserbyUsernameAsync(User.GetUsername());
            if(user == null) return NotFound();

            var result = await _photoService.AddPhotoAsync(file);
            if(result.Error != null) return BadRequest(result.Error.Message);
            var photo = new Photo{
                Url= result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };
            if(user.Photos.Count == 0 ) photo.IsMain = true;
            user.Photos.Add(photo);
            if (await _userRepository.SaveAllAsync()) {
                return CreatedAtAction(nameof(GetUser), new {username = user.UserName},_mapper.Map<PhotoDto>(photo));
            }
            return BadRequest("Problem adding photo");

        }
    }
}