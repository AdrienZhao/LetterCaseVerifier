using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace APIReturnValueLetterCaseVerifier
{
    public class JsonValueEntriesProvider : IEntriesProvider
    {
        DtoEntityHandler handler;

        public JsonValueEntriesProvider(Assembly assembly)
        {
            handler = new DtoEntityHandler(assembly);
        }

        public IEnumerable<Entry> GetEntries()
        {
            return handler.GetEntities();
        }
    }
}
