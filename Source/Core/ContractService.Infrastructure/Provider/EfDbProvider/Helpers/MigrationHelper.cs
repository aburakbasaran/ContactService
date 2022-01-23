using System;
using System.Linq;

namespace ContactService.Infrastructure.Provider.EfDbProvider.Helpers
{
    public static class MigrationHelper
    {
        public static bool IsMigrationOperationExecuting()
        {
            string[] commandLineArguments = Environment.GetCommandLineArgs();
            string[] orderedMigrationArguments = { "migrations", "add" };

            for (int i = 0; i <= commandLineArguments.Length - orderedMigrationArguments.Length; i++)
            {
                if (commandLineArguments.Skip(i).Take(orderedMigrationArguments.Length).SequenceEqual(orderedMigrationArguments))
                {
                    return true;
                }
            }

            return false;
        }
    }
}