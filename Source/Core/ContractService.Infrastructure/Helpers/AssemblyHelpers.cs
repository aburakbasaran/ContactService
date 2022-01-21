using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyModel;

namespace ContactService.Infrastructure.Helpers
{
    public static class AssemblyHelpers
    {
        public const string ModulesRoot = "ContactService.*";
        private const string MicrosoftAssemblyNamePrefix = "Microsoft.";
        private const string SystemAssemblyNamePrefix = "System.";

        public static IEnumerable<Assembly> LoadFromSearchPatterns(params string[] searchPatterns)
        {
            if (searchPatterns == null || searchPatterns.Length == 0)
            {
                return Enumerable.Empty<Assembly>();
            }

            HashSet<Assembly> assemblies = new();
            foreach (string searchPattern in searchPatterns)
            {
                Regex searchRegex = new(searchPattern, RegexOptions.IgnoreCase);
                List<RuntimeLibrary> moduleAssemblyFiles = DependencyContext
                    .Default
                    .RuntimeLibraries
                    .Where(x => searchRegex.IsMatch(x.Name))
                    .ToList();

                foreach (RuntimeLibrary assemblyFiles in moduleAssemblyFiles)
                {
                    assemblies.Add(Assembly.Load(new AssemblyName(assemblyFiles.Name)));
                }
            }

            return assemblies.ToList();
        }

        public static List<Type> GetAssembliesAssignableFrom(Type assignTypeFrom)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(l => !l.GetName().Name.StartsWith(MicrosoftAssemblyNamePrefix) && !l.GetName().Name.StartsWith(SystemAssemblyNamePrefix))
                .SelectMany(c => c.GetTypes())
                .Where(c => c.IsClass && !c.IsAbstract && assignTypeFrom.IsAssignableFrom(c))
                .ToList();
        }
    }
}
