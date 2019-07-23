using AutoMapper;
using Cinema.Managers;
using Cinema.Services;
using LightInject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Cinema
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = new ServiceContainer(); //создание контейнера LigthInject
            container.RegisterControllers(); //


            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            InitialAutomapperProfiles(container);
            //container.Register<ITicketService, JsonTicketService>(new PerRequestLifeTime()); // регистрация контейнера
            container.Register<ITicketService, SqlTicketService>(new PerRequestLifeTime());
            container.Register<ICacheManager, CacheManager>(new PerRequestLifeTime());
            container.EnableMvc();
        }

        private static void InitialAutomapperProfiles(ServiceContainer container)
        {
            var assembly = Assembly.GetCallingAssembly(); //получение вызываемой библиотеки dll
            var definedTypes = assembly.DefinedTypes;

            //выбираем все типы которые унаследованы от Profile
            var profiles = definedTypes.Where(type => typeof(Profile).GetTypeInfo().IsAssignableFrom(type) && !type.IsAbstract).ToArray();

            //проходимся по всем Profile и регистрируем все типы
            void ConfigAction(IMapperConfigurationExpression cfg)
            {
                foreach (var profile in profiles.Select(t => t.AsType()))
                {
                    cfg.AddProfile(profile);

                }
            }
            Mapper.Initialize(ConfigAction);
            MapperConfiguration config = (MapperConfiguration)Mapper.Configuration;
            config.AssertConfigurationIsValid();

            container.Register(sp => config.CreateMapper(), new PerRequestLifeTime());
        }
    }
}
