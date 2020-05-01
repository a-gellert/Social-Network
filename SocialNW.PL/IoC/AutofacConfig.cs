using Autofac;
using Autofac.Integration.Mvc;
using SocialNW.BLL.Interfaces;
using SocialNW.BLL.IoC;
using SocialNW.BLL.Services;
using System.Web.Mvc;

namespace SocialNW.PL.IoC
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModule(new AutofacModule("SocialDB"));
            builder.RegisterType<PostService>().As<IPostService>();
            builder.RegisterType<MessageService>().As<IMessageService>();
            builder.RegisterType<CommentService>().As<ICommentService>();
            builder.RegisterType<FriendRequestService>().As<IFriendService>();
            builder.RegisterType<ProfileService>().As<IProfileService>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}