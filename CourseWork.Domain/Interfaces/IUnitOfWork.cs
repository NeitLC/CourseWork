using System;
using System.Threading.Tasks;
using CourseWork.Domain.Data;
using CourseWork.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace CourseWork.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationContext Context { get; }
        
        IRepository<Collection, int> Collections { get; }
        
        IRepository<Comment, int> Comments { get; }
        
        IRepository<Image, int> Images { get; }
        
        IRepository<Item, int> Items { get; }
        
        IRepository<User, string> Users { get; }
        
        IRepository<Tag, int> Tags { get; }
        
        UserManager<User> UserManager { get; }
        
        RoleManager<IdentityRole> RoleManager { get; }
        
        SignInManager<User> SignInManager { get; }
        
        Task SaveAsync();
    }
}