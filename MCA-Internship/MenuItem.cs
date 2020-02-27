using System;
using System.Collections.Generic;
using System.Text;

namespace MCA_Internship
{
    class MenuItem
    {
        public string ID { get; set; }
        public string MenuName { get; set; }
        public string ParentID { get; set; }
        public bool isHidden { get; set; }
    }
}
