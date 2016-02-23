using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Backend
{
    class Activity
    {

        public DateTime ActionDate { get; set; }
        public string ActionName { get; set; }
        public string ActionDescription { get; set; }

        // can be negative too
        public string ActionPoints { get; set; }

    }
}
