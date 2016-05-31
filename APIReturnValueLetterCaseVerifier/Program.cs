using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace APIReturnValueLetterCaseVerifier
{
    class Program
    {
        private const string codeBase = @"D:\md\Public2\MD.API\bin";

        static void Main(string[] args)
        {
            try
            {
                args = new string[] { @"D:\md\Public2\MD.API\bin\MD.API.dll" };

                Arguments arguments = HandleArgs(args);
                var assembly = Assembly.LoadFrom(arguments.AssemblyLocation);
                AppDomain.CurrentDomain.AssemblyResolve += MyHandler;
                IEntriesProvider provider = new JsonValueEntriesProvider(assembly);
                var entries = provider.GetEntries();
                IEntryHandler entryHandler = new jsonValueEntriesHandler();
                entryHandler.Handle(entries);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.Read();
            }
        }

        static Assembly MyHandler(object source, ResolveEventArgs e)
        {
            Assembly assembly = null;

            var directory = codeBase;
            string path = Path.Combine(directory, e.Name.Split(',')[0]) + ".dll";
            Console.WriteLine("Resolving {0}", e.Name);
            assembly = Assembly.LoadFrom(path);


            return assembly;
        }

        private static Arguments HandleArgs(string[] args)
        {
            if (args.Count() < 1 || args.Count() > 1)
            {
                throw new ArgumentException(nameof(args));
            }

            Arguments arguments = new Arguments();
            arguments.AssemblyLocation = args[0];

            return arguments;
        }
    }

    public class Arguments
    {
        public string AssemblyLocation { get; set; }
    }
}
