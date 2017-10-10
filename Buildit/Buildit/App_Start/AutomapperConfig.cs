using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using AutoMapper;
using Buildit.Web.Models.Contracts;

namespace Buildit.App_Start
{
    //public class AutoMapperConfig
    //{
    //    public static void RegisterMapper(params Assembly[] assemblies)
    //    {
    //        Mapper.Initialize(cfg =>
    //        {
    //            RegisterMappings(cfg, assemblies);
    //        });
    //    }

    //    public static void RegisterMappings(IMapperConfigurationExpression config, params Assembly[] assemblies)
    //    {
    //        config.ConstructServicesUsing(t => DependencyResolver.Current.GetService(t));

    //        var types = new List<Type>();
    //        foreach (var assembly in assemblies)
    //        {
    //            types.AddRange(assembly.GetExportedTypes());
    //        }

    //        LoadStandardMappings(config, types);
    //        LoadCustomMappings(config, types);
    //    }

    //    private static void LoadStandardMappings(IMapperConfigurationExpression config, IEnumerable<Type> types)
    //    {
    //        var maps = types.SelectMany(t => t.GetInterfaces(), (t, i) => new { t, i })
    //            .Where(
    //                type =>
    //                    type.i.IsGenericType && type.i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
    //                    !type.t.IsAbstract
    //                    && !type.t.IsInterface)
    //            .Select(type => new { Source = type.i.GetGenericArguments()[0], Destination = type.t });

    //        foreach (var map in maps)
    //        {
    //            config.CreateMap(map.Source, map.Destination);
    //            config.CreateMap(map.Destination, map.Source);
    //        }
    //    }

    //    private static void LoadCustomMappings(IMapperConfigurationExpression config, IEnumerable<Type> types)
    //    {
    //        var maps =
    //            types.SelectMany(t => t.GetInterfaces(), (t, i) => new { t, i })
    //                .Where(
    //                    type =>
    //                        typeof(ICustomMappings).IsAssignableFrom(type.t) && !type.t.IsAbstract &&
    //                        !type.t.IsInterface)
    //                .Select(type => (ICustomMappings)Activator.CreateInstance(type.t));

    //        foreach (var map in maps)
    //        {
    //            map.CreateMappings(config);
    //        }
    //    }
    //}

    public class AutoMapperConfig
    {
        public static IMapperConfigurationExpression Configuration { get; private set; }

        public void Execute(Assembly assembly)
        {
            Mapper.Initialize(
                cfg =>
                {
                    var types = assembly.GetExportedTypes();
                    LoadStandardMappings(types, cfg);
                    LoadCustomMappings(types, cfg);
                    Configuration = cfg;
                });
        }

        private static void LoadStandardMappings(IEnumerable<Type> types, IMapperConfigurationExpression mapperConfiguration)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMap<>) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select new
                        {
                            Source = i.GetGenericArguments()[0],
                            Destination = t
                        }).ToArray();

            foreach (var map in maps)
            {
                mapperConfiguration.CreateMap(map.Source, map.Destination);
                mapperConfiguration.CreateMap(map.Destination, map.Source);
            }
        }

        private static void LoadCustomMappings(IEnumerable<Type> types, IMapperConfigurationExpression mapperConfiguration)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where typeof(ICustomMappings).IsAssignableFrom(t) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select (ICustomMappings)Activator.CreateInstance(t)).ToArray();

            foreach (var map in maps)
            {
                map.CreateMappings(mapperConfiguration);
            }
        }
    }
}

