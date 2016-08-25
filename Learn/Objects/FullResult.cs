using Learn.Items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Objects
{
    public class FullResult
    {
        public List<ResultItem> ResultList = new List<ResultItem>();

        // based on list dunno player max combo
        public int MaxCombo { get; set; }
    }
}
