using System;
using System.Threading.Tasks;
using CourseWork.Domain.Data;
using CourseWork.Domain.Interfaces;
using CourseWork.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace CourseWork.Domain.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private IRepository<Collection, int> _collectionRepository;
        private IRepository<Comment, int> _commentRepository;
        private IRepository<Image, int> _imageRepository;
        private IRepository<Item, int> _itemRepository;
        private IRepository<Tag, int> _tagRepository;
        private IRepository<User, string> _userRepository;
        private bool _disposed = false;
        public UserManager<User> UserManager { get; }
        public SignInManager<User> SignInManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }
        
        public UnitOfWork(
            ApplicationContext context,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }
        
        public ApplicationContext Context => _context;


        public IRepository<Collection, int> Collections
        {
            get
            {
                _collectionRepository = _collectionRepository ?? new CollectionRepository(_context);
                return _collectionRepository;
            }

        }

        public IRepository<Comment, int> Comments
        {
            get
            {
                _commentRepository = _commentRepository ?? new CommentRepository(_context);
                return _commentRepository;
            }
        }

        public IRepository<Image, int> Images
        {
            get
            {
                _imageRepository = _imageRepository ?? new ImageRepository(_context);
                return _imageRepository;
            }
        }

        public IRepository<Item, int> Items
        {
            get
            {
                _itemRepository = _itemRepository ?? new ItemRepository(_context);
                return _itemRepository;
            }
        }

        public IRepository<Tag, int> Tags
        {
            get
            {
                _tagRepository = _tagRepository ?? new TagRepository(_context);
                return _tagRepository;
            }
        }

        public IRepository<User, string> Users
        {
            get
            {
                _userRepository = _userRepository ?? new UserRepository(_context);
                return _userRepository;
            }
        }
        
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}