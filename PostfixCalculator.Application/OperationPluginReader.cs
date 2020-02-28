namespace PostfixCalculator.Application
{
    using global::PostfixCalculator.Domain;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public class OperationPluginReader : IPluginReader
    {
        public List<IOperation> ReadPluginsFrom(string directory)
        {
            var operations = new List<IOperation>();

            var plugins = GetPluginFiles(directory);
            foreach (var fileInfo in plugins)
            {
                var assembly = Assembly.LoadFrom(fileInfo.FullName);
                GetOperationsFromAssembly(assembly, operations);
            }
            return operations;
        }

        private void GetOperationsFromAssembly(Assembly assembly, List<IOperation> operations)
        {
            var types = assembly.GetTypes();
            
            operations.AddRange(types.Where(type => typeof(IOperation).IsAssignableFrom(type))
                            .Select(type => (IOperation)Activator.CreateInstance(type)));
        }

        private IEnumerable<FileInfo> GetPluginFiles(string path)
        {
            var directory = new DirectoryInfo(path);
            if (!directory.Exists)
            {
                throw new DirectoryNotFoundException();
            }
            return directory.GetFiles("*.dll");
        }
    }
}