using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectServerHelper
{
    public class PSHWorkflowDefinition
    {
        public Guid WFDefinitionId { get; set; }
        public string WFName { get; set; }
        public string WFDefinitionXAML { get; set; }
    }
}
