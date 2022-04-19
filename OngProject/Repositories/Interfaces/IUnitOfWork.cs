using System.Threading.Tasks;
using OngProject.Entities;

namespace OngProject.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Organization> OrganizationsRepository { get; }
        IRepository<Rol> RolRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<Slide> SlideRepository { get; }
        IRepository<Activity> ActivityRepository { get; }
        IRepository<Testimony> TestimonyRepository { get; }
        IRepository<Member> MemberRepository { get; }
        IRepository<Comment> CommentRepository { get; }
        IRepository<Category> CategoryRepository { get; }
        IRepository<New> NewRepository { get; }

        void SaveChanges();

        Task SaveChangesAsync();

        void Dispose();
    }
}
