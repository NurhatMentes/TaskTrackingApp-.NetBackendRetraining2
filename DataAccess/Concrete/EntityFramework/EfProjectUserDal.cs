﻿using Core.Constants;
using Core.DataAccess.EntityFramework;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProjectUserDal : EfEntityRepositoryBase<ProjectUser, TaskTrackingAppDBContext>, IProjectUserDal
    {
        public IDataResult<List<ProjectUserDto>> GetAll()
        {
            using (var context = new TaskTrackingAppDBContext())
            {
                var projectUsers = from pu in context.ProjectUsers
                                   join p in context.Projects on pu.ProjectId equals p.Id
                                   join u in context.Users on pu.UserId equals u.Id
                                   select new ProjectUserDto
                                   {
                                       Id = pu.Id,
                                       ProjectId = pu.ProjectId,
                                       UserId = pu.UserId,
                                       Role = pu.Role,
                                       ProjectName = p.Name, 
                                       UserName = u.FirstName + " " + u.LastName, 
                                       UserEmail = u.Email 
                                   };

                return new SuccessDataResult<List<ProjectUserDto>>(projectUsers.ToList());
            }
        }

        public IDataResult<List<ProjectUserDto>> GetAllByProjectId(int projectId)
        {
            using (var context = new TaskTrackingAppDBContext())
            {
                var projectUsers = (from pu in context.ProjectUsers
                                    join p in context.Projects on pu.ProjectId equals p.Id
                                    join u in context.Users on pu.UserId equals u.Id
                                    where pu.ProjectId == projectId
                                    select new ProjectUserDto
                                    {
                                        Id = pu.Id,
                                        ProjectId = pu.ProjectId,
                                        UserId = pu.UserId,
                                        Role = pu.Role,
                                        ProjectName = p.Name,
                                        UserName = u.FirstName + " " + u.LastName,
                                        UserEmail = u.Email
                                    }).ToList();

                if (!projectUsers.Any())
                {
                    return new ErrorDataResult<List<ProjectUserDto>>("Proje bulunamadı.");
                }

                return new SuccessDataResult<List<ProjectUserDto>>(projectUsers);
            }
        }

        public IDataResult<List<ProjectUserDto>> GetAllByUserId(int userId)
        {
            using (var context = new TaskTrackingAppDBContext())
            {
                var projectUsers = from pu in context.ProjectUsers
                                   join p in context.Projects on pu.ProjectId equals p.Id
                                   join u in context.Users on pu.UserId equals u.Id
                                   where pu.UserId == userId
                                   select new ProjectUserDto
                                   {
                                       Id = pu.Id,
                                       ProjectId = pu.ProjectId,
                                       UserId = pu.UserId,
                                       Role = pu.Role,
                                       ProjectName = p.Name,
                                       UserName = u.FirstName + " " + u.LastName,
                                       UserEmail = u.Email
                                   };

                return new SuccessDataResult<List<ProjectUserDto>>(projectUsers.ToList());
            }
        }

        public IDataResult<ProjectUserDto> GetById(int projectUserId)
        {
            using (var context = new TaskTrackingAppDBContext())
            {
                var projectUser = (from pu in context.ProjectUsers
                                   join p in context.Projects on pu.ProjectId equals p.Id
                                   join u in context.Users on pu.UserId equals u.Id
                                   where pu.Id == projectUserId
                                   select new ProjectUserDto
                                   {
                                       Id = pu.Id,
                                       ProjectId = pu.ProjectId,
                                       UserId = pu.UserId,
                                       Role = pu.Role,
                                       ProjectName = p.Name,
                                       UserName = u.FirstName + " " + u.LastName,
                                       UserEmail = u.Email
                                   }).FirstOrDefault();

                if (projectUser != null)
                {
                    return new SuccessDataResult<ProjectUserDto>(projectUser);
                }

                return new ErrorDataResult<ProjectUserDto>("Proje kullanıcısı bulunamadı.");
            }
        }
    }
}
