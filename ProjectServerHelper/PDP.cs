using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectServerHelper
{
    public class PSHPDP
    {
        public string PDPName { get; set; }
        public Guid PDPGuid { get; set; }
    }

    public class PSHStage
    {
        public string StageName { get; set; }
        public Guid StageGuid { get; set; }
    }

    public class PSHPhase
    {
        public string PhaseName { get; set; }
        public Guid PhaseGuid { get; set; }
    }
}
