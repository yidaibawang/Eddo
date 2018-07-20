﻿using System;
using Eddo.Modules;
using Eddo.Reflection;
using System.Reflection;
using AutoMapper;
using Castle.MicroKernel.Registration;
using Eddo;
namespace Eddo.AutoMapper
{
    [DependsOn(typeof(EddoKernelModule))]
    public class EddoAutoMapperModule:EddoModule
    {
        private readonly ITypeFinder _typeFinder;
        private static volatile bool _createdMappingsBefore;
        private static readonly object SyncObj = new object();

        public EddoAutoMapperModule(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }

        public override void PreInitialize()
        {
            IocManager.Register<IEddoAutoMapperConfiguration, EddoAutoMapperConfiguration>();

            IocManager.Register<ObjectMapper.IObjectMapper, AutoMapperObjectMapper>();


        }

        public override void PostInitialize()
        {
            CreateMappings();
        }

        private void CreateMappings()
        {
            lock (SyncObj)
            {
                Action<IMapperConfigurationExpression> configurer = configuration =>
                {
                    FindAndAutoMapTypes(configuration);
                    foreach (var configurator in Configuration.Modules.EddoAutoMapper().Configurators)
                    {
                        configurator(configuration);
                    }
                };

                if (Configuration.Modules.EddoAutoMapper().UseStaticMapper)
                {
                    //We should prevent duplicate mapping in an application, since Mapper is static.
                    if (!_createdMappingsBefore)
                    {
                        Mapper.Initialize(configurer);
                        _createdMappingsBefore = true;
                    }

                    IocManager.IocContainer.Register(
                        Component.For<IMapper>().Instance(Mapper.Instance).LifestyleSingleton()
                    );
                }
                else
                {
                    var config = new MapperConfiguration(configurer);
                    IocManager.IocContainer.Register(
                        Component.For<IMapper>().Instance(config.CreateMapper()).LifestyleSingleton()
                    );
                }
            }
        }

        private void FindAndAutoMapTypes(IMapperConfigurationExpression configuration)
        {
            var types = _typeFinder.Find(type =>
            {
                var typeInfo = type.GetTypeInfo();
                return typeInfo.IsDefined(typeof(AutoMapAttribute)) ||
                       typeInfo.IsDefined(typeof(AutoMapFromAttribute)) ||
                       typeInfo.IsDefined(typeof(AutoMapToAttribute));
            }
            );

            Logger.DebugFormat("Found {0} classes define auto mapping attributes", types.Length);

            foreach (var type in types)
            {
                Logger.Debug(type.FullName);
                configuration.CreateAutoAttributeMaps(type);
            }
        }

     
    }
}
