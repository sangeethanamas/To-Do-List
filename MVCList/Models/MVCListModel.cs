using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCList.Models
{
    public class MVCListModel
    {
        public int Id { get; set; }
        public string content { get; set; }

        public bool IsSelected { get; set; }
    }
}