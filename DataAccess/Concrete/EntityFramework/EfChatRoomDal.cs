﻿using Core.DataAccess.EntityFramework;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfChatRoomDal : EfEntityRepositoryBase<ChatRoom, TaskTrackingAppDBContext>, IChatRoomDal
    {
        public IDataResult<List<ChatRoomDetailDto>> GetAll()
        {
            using (var context = new TaskTrackingAppDBContext())
            {
                var chatRooms = from cr in context.ChatRooms
                                join u in context.Users on cr.CreatedByUserId equals u.Id into userGroup
                                from u in userGroup.DefaultIfEmpty()
                                join p in context.Projects on cr.RelatedProjectId equals p.Id into projectGroup
                                from p in projectGroup.DefaultIfEmpty()

                                select new ChatRoomDetailDto
                                {
                                    Id = cr.Id,
                                    Name = cr.Name,
                                    RelatedProjectName = p != null ? p.Name : null,
                                    CreatedAt = cr.CreatedAt,
                                    CreatedByUserName = u != null ? u.FirstName + " " + u.LastName : " ",
                                    CreatedByUserEmail = u != null ? u.Email : null
                                };

                return new SuccessDataResult<List<ChatRoomDetailDto>>(chatRooms.ToList());
            }
        }

        public List<ChatRoomDetailDto> GetAllChatRoomsWithProjectsAndUsers()
        {
            using (var context = new TaskTrackingAppDBContext())
            {
                var result = (from cr in context.ChatRooms
                              join u in context.Users on cr.CreatedByUserId equals u.Id
                              join p in context.Projects on cr.RelatedProjectId equals p.Id into projectGroup
                              from project in projectGroup.DefaultIfEmpty() 
                              select new ChatRoomDetailDto
                              {
                                  Id = cr.Id,
                                  Name = cr.Name,
                                  CreatedByUserEmail = u.Email,
                                  CreatedByUserName = u != null ? u.FirstName + " " + u.LastName : " ",
                                  CreatedAt = cr.CreatedAt,
                                  RelatedProjectName = project != null ? project.Name : null 
                              }).ToList();

                return result;
            }
            
        }

        public List<ChatRoomDetailDto> GetChatRoomsWithProjectsAndUsersByUserId(int userId)
        {
            using (var context = new TaskTrackingAppDBContext())
            {
                var result = (from cru in context.ChatRoomUsers 
                              join cr in context.ChatRooms on cru.ChatRoomId equals cr.Id
                              join u in context.Users on cr.CreatedByUserId equals u.Id
                              join p in context.Projects on cr.RelatedProjectId equals p.Id into projectGroup
                              from project in projectGroup.DefaultIfEmpty() 
                              where cru.UserId == userId 
                              select new ChatRoomDetailDto
                              {
                                  Id = cr.Id,
                                  Name = cr.Name,
                                  CreatedByUserEmail = u.Email,
                                  CreatedByUserName = u != null ? u.FirstName + " " + u.LastName : " ", 
                                  CreatedAt = cr.CreatedAt,
                                  RelatedProjectName = project != null ? project.Name : null 
                              }).ToList();

                return result;
            }
            
        }
    }
}
