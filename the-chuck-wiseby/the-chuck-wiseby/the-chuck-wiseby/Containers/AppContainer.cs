using Autofac;
using System;
using System.Net.Http;
using the_chuck_wiseby.Models;
using the_chuck_wiseby.Services;
using the_chuck_wiseby.ViewModels;

namespace the_chuck_wiseby.Containers
{
    public class AppContainer
    {
        private static IContainer Container;

        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            // ViewModels:
            builder.RegisterType<MainViewModel>();
            builder.RegisterType<SearchViewModel>();
            builder.RegisterType<JokeResultViewModel>();

            // Services:
            builder.RegisterType<ChuckJokeService>().As<IHttpService<ChuckJoke>>();
            builder.RegisterType<NavigationService>().As<INavigationService>();

            Container = builder.Build();
        }

        public static object Resolve(Type typeName)
        {
            return Container.Resolve(typeName);
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}
