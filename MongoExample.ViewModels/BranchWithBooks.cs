using System;
using System.Collections.Generic;
using System.Text;
using MongoExample.Data;

namespace MongoExample.ViewModels
{
    public class BranchWithBooks:Branch
    {
        public List<Book> books { get; set; }
    }
}
