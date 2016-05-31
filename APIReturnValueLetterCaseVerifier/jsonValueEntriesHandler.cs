using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIReturnValueLetterCaseVerifier
{
    public class jsonValueEntriesHandler : IEntryHandler
    {
        private static ILog logger = LogManager.GetLogger(typeof(jsonValueEntriesHandler));

        public void Handle(IEnumerable<Entry> entries)
        {
            using (StreamWriter writer = new StreamWriter(@"C:\log\lettercase.csv"))
            {
                writer.WriteLine("location, value");
                foreach (var entry in entries)
                {

                    writer.WriteLine(string.Format(" {0},  {1} ", entry.Location, entry.Value));
                }
                //logger.Info(string.Format("Location: {0} \r\n, Value {1} \r\n", entry.Location, entry.Value));
            }
        }
    }
}
