using Autofac;
using SocialNW.DAL.Interfaces;
using SocialNW.DAL.Repositories;

namespace SocialNW.BLL.IoC
{
    public class AutofacModule : Module
    {
        private string _connectionString;

        public AutofacModule(string connection)
        {
            _connectionString = connection;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().WithParameter("connectionString", "SocialNW_DB");
        }
    }
}
