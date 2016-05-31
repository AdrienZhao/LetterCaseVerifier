using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIReturnValueLetterCaseVerifier
{
    public interface IEntriesProvider
    {
        IEnumerable<Entry> GetEntries();
    }
}
