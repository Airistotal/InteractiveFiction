﻿using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Procedure;
using Microsoft.Extensions.DependencyInjection;

namespace InteractiveFiction.Business.Infrastructure
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddInteractiveFictionBusiness(this IServiceCollection service)
        {
            service
                .AddSingleton<ITextLoader, TextLoader>()
                .AddSingleton<IGameArchetypeLoader, GameArchetypeLoader>()
                .AddSingleton<IDirectoryFacade, DirectoryFacade>()
                .AddSingleton<IEntityBuilder, EntityBuilder>()
                .AddSingleton<IEntityBuilderFactory, EntityBuilderFactory>()
                .AddSingleton<IUniverseBuilder, UniverseBuilder>()
                .AddSingleton<IProcedureBuilder, ProcedureBuilder>()
                .AddSingleton(_ => MessageBus.MessageBus.GetMessageBus());

            return service;
        }
    }
}
