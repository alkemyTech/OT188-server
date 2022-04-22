using OngProject.Repositories.Interfaces;
using OngProject.Core.Models;
using OngProject.DataAccess;
using System.Threading.Tasks;
using System;
using OngProject.Entities;

namespace OngProject.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly OngProjectDbContext _context;
        private readonly IRepository<Organization> _organizationsRepository;
        private readonly IRepository<Rol> _rolRepository;
        private readonly IRepository<User> _usersRepository;
        private readonly IRepository<Comment> _commentsRepository;
        private readonly IRepository<Testimony> _testimonialsRepository;
        private readonly IRepository<Slide> _slideRepository;
        private readonly IRepository<Activity> _activityRepository;
        private readonly IRepository<Member> _memberRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<New> _newRepository;
        private readonly IRepository<Contact> _contactRepository;

        private bool disposed = false;

        public UnitOfWork(OngProjectDbContext context)
        {
            _context = context;
        }

        public IRepository<Organization> OrganizationsRepository =>
            _organizationsRepository ?? new Repository<Organization>(_context);
        public IRepository<Rol> RolRepository => _rolRepository ?? new Repository<Rol>(_context);
        public IRepository<User> UserRepository => _usersRepository ?? new Repository<User>(_context);
        public IRepository<Testimony> TestimonyRepository => _testimonialsRepository ?? new Repository<Testimony>(_context);
        public IRepository<Comment> CommentRepository => _commentsRepository ?? new Repository<Comment>(_context);
        public IRepository<Category> CategoryRepository => _categoryRepository ?? new Repository<Category>(_context);
        public IRepository<New> NewRepository => _newRepository ?? new Repository<New>(_context);
        public IRepository<Slide> SlideRepository => _slideRepository ?? new Repository<Slide>(_context);
        public IRepository<Activity> ActivityRepository => _activityRepository ?? new Repository<Activity>(_context);
        public IRepository<Member> MemberRepository => _memberRepository ?? new Repository<Member>(_context);
        public IRepository<Contact> ContactRepository => _contactRepository ?? new Repository<Contact>(_context);

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
      
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }        
    }
}
