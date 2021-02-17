using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectServerHelper
{
    public class PSHWFAssociation
    {
        public Guid WFAssoId { get; set; }
        public Guid WFDefinitionId { get; set; }
        public string WFAssoName { get; set; }
        public IDictionary<string,string> WFAssoPropertyDefinitions { get; set; }
        public IList<string> WFAssoEventTypes { get; set; }
    }
}
