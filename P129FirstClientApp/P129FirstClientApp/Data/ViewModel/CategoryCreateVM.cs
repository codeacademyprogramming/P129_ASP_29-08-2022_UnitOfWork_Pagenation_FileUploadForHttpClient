using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P129FirstClientApp.Data.ViewModel
{
    public class CategoryCreateVM
    {
        public string Name { get; set; }
        public bool IsMain { get; set; }
        public Nullable<int> ParentId { get; set; }
        public IFormFile File { get; set; }
    }
}
