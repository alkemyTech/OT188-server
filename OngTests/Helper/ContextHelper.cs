using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using OngProject.Core.Business;
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.DataAccess;
using OngProject.Repositories.Interfaces;

namespace OngTests.Helper
{
    public class ContextHelper
    {
        public static OngProjectDbContext DbContext { get; set; }
        public static IUnitOfWork UnitOfWork;
        public static IEntityMapper EntityMapper;
        public static IConfiguration Config;
        public static IRolesBusiness rolesBusiness;
        public static IJwtTokenProvider JwtHelper;
        public static IHttpContextAccessor httpContext;
        public static IAmazonS3Helper AmazonS3Business;
        public static IUsersBusiness UserBusiness;
        public static ISlidesBusiness SlideBusiness;
        public static IAuthBusiness AuthBusiness;
        public static IEmailServices EmailServices;

        public static void UseContext()
        {
            EntityMapper = new EntityMapper(AmazonS3Business);
            Config = new ConfigurationHelper().Config;
            JwtHelper = new JwtTokenProvider(Config, rolesBusiness);
            httpContext = new HttpContextAccessor();
            SlideBusiness = new SlidesBusiness(UnitOfWork, EntityMapper, AmazonS3Business);

        }

        public static void UseDbContext(bool pupulate = true)
        {
            DbContext = DbContextHelper.UseDbContext(pupulate);
            UnitOfWork = new UnitOfWorkHelper(DbContext)._unitOfWork;
        }
    }   
}
