using ApiForMyProjects.IRepository;
using ApiForMyProjects.Repository;
using Autofac;

namespace ApiForMyProjects
{
    public class DependencyContainer
    {

        public static void ConfigureContainer(ContainerBuilder builder)
        {
            #region === Services ===

            builder.RegisterType<User>().As<IUser>();

            #endregion
        }
    }
}
