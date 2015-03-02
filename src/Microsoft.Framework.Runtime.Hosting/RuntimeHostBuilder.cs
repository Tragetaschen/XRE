﻿using System.Collections.Generic;
using System.Runtime.Versioning;
using NuGet.DependencyResolver;
using NuGet.Frameworks;
using NuGet.ProjectModel;

namespace Microsoft.Framework.Runtime.Hosting
{
    public class RuntimeHostBuilder
    {
        public IAssemblyLoaderContainer LoaderContainer { get; }
        public IList<IDependencyProvider> DependencyProviders { get; }
        public NuGetFramework TargetFramework { get; set; }
        public Project Project { get; set; }
        public LockFile LockFile { get; set;  }

        public RuntimeHostBuilder(IAssemblyLoaderContainer loaderContainer)
        {
            LoaderContainer = loaderContainer;
            DependencyProviders = new List<IDependencyProvider>();
        }

        /// <summary>
        /// Create a <see cref="RuntimeHostBuilder"/> for the project in the specified
        /// <paramref name="projectDirectory"/>.
        /// </summary>
        /// <remarks>
        /// This method will throw if the project.json file cannot be found in the
        /// specified folder. If a project.lock.json file is present in the directory
        /// it will be loaded. 
        /// </remarks>
        /// <param name="projectDirectory">The directory of the project to host</param>
        public static RuntimeHostBuilder ForProjectDirectory(string projectDirectory, IApplicationEnvironment applicationEnvironment, IAssemblyLoaderContainer loaderContainer)
        {
            var hostBuilder = new RuntimeHostBuilder(loaderContainer);

            // Load the Project
            hostBuilder.Project = ProjectReader.ReadProjectFile(projectDirectory);

            // Load the Lock File if present
            if (ProjectReader.HasLockFile(projectDirectory))
            {
                hostBuilder.LockFile = ProjectReader.ReadLockFile(projectDirectory);
            }

            // Set the framework
            hostBuilder.TargetFramework = NuGetFramework.Parse(applicationEnvironment.RuntimeFramework.FullName);

            return hostBuilder;
        }

        /// <summary>
        /// Builds a <see cref="RuntimeHost"/> from the parameters specified in this
        /// object.
        /// </summary>
        public RuntimeHost Build()
        {
            return new RuntimeHost(this);
        }
    }
}