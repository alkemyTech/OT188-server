using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using OngProject.Core.Business;
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.DataAccess;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OngTests.Helper
{
    public class ContextHelper
    {
        public static OngProjectDbContext DbContext { get; set; }
        public static IHttpContextAccessor HttpContext;
        public static IAmazonS3Helper amazonS3;
        public static IUnitOfWork UnitOfWork;
        public static IEntityMapper EntityMapper;
        public static IConfiguration Config;
        public static IJwtTokenProvider JwtTokenProvider;
        public static IRolesBusiness RolesBusiness;
        public static IEmailServices EmailService;

        public static void CreateContext()
        {
            // acá se inicializa todo menos el DbContext SIN datos??

            Config = new ConfigurationHelper().Config; // si            
            EntityMapper = new EntityMapper(amazonS3); // si
            JwtTokenProvider = new JwtTokenProvider(Config, RolesBusiness); // si
            HttpContext = new HttpContextAccessor(); // si
            UnitOfWork = new UnitOfWorkHelper(DbContext).unitOfWork; /// va acá???

            RolesBusiness = new RolesBusiness(UnitOfWork);
            EmailService = new SendgridEmailServices(Config);            
        }

        // metodo para inicializar el context CON datos?

        public static void SeedDbContext()
        {
            DbContext = DbContextHelper.ConfigureDbContext(true); // de donde saco el modelBuilder ???
            UnitOfWork = new UnitOfWorkHelper(DbContext).unitOfWork; //  acá????
            //amazonS3??
        }
    }
}
