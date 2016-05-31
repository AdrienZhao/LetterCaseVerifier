using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIReturnValueLetterCaseVerifier
{
    public interface IEntryHandler
    {
        void Handle(IEnumerable<Entry> entry);
    }
}
