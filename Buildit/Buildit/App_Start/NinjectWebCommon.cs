[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Buildit.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Buildit.App_Start.NinjectWebCommon), "Stop")]

namespace Buildit.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Extensions.Conventions;
    using Buildit.Data;
    using Buildit.Data.Repositories;
    using System.Data.Entity;
    using AutoMapper;
    using System.Reflection;
    using Buildit.Data.Contracts;
    using Buildit.Services.Contracts;
    using Buildit.Services;
    using Buildit.Common.Providers;
    using Buildit.Common.Providers.Contracts;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {

            //kernel.Bind(x =>
            //{
            //    x.FromThisAssembly()
            //     .SelectAllClasses()
            //     .BindDefaultInterface();
            //});

            //kernel.Bind(x =>
            //{
            //    x.FromAssemblyContaining(typeof(IService))
            //     .SelectAllClasses()
            //     .BindDefaultInterface();
            //});


            var mapper = new AutoMapperConfig();
            mapper.Execute(Assembly.GetExecutingAssembly());

            kernel.Bind(typeof(DbContext)).To<BuilditDbContext>().InRequestScope();
            kernel.Bind(typeof(IEfRepository<>)).To(typeof(EfRepository<>)).InRequestScope();
            //kernel.Bind<ISaveContext>().To<SaveContext>();
            kernel.Bind<IMapper>().ToConstant(Mapper.Instance).InRequestScope();
            kernel.Bind<IMapperAdapter>().To<MapperAdapter>().InRequestScope();
            kernel.Bind<ICacheProvider>().To<CacheProvider>().InRequestScope();

            kernel.Bind<IPublicationService>().To<PublicationService>();
            kernel.Bind<IPublicatioTypeService>().To<PublicationTypeService>();
            kernel.Bind<IBuilditData>().To<BuilditData>(); 
            kernel.Bind<HttpContextBase>().ToMethod(ctx=> new HttpContextWrapper(HttpContext.Current)).InRequestScope();
            //kernel.Bind<IMapper>().ToMethod(x => Mapper.Instance).InSingletonScope();
        }
    }
}