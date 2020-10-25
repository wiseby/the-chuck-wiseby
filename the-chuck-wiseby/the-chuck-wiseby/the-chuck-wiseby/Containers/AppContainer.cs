using Autofac;
using System;
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
            builder.RegisterType<JokeResultViewModel>();
            builder.RegisterType<RandomJokeViewModel>();
            builder.RegisterType<JokeCategoryViewModel>();
            builder.RegisterType<FavouriteViewModel>();

            // Services:
            builder.RegisterType<ChuckJokeService>().As<IHttpService<ChuckJoke, ChuckMessage>>();
            builder.RegisterType<NavigationService>().As<INavigationService>();
            builder.RegisterType<DataStore>().As<IDataStore<ChuckJoke>>().SingleInstance();
            builder.RegisterType<FavouriteService>().As<IFavouriteService<ChuckJoke>>().SingleInstance();

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
