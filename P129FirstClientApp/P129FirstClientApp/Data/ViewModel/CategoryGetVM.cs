using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P129FirstClientApp.Data.ViewModel
{
    public class CategoryGetVM
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public bool Esasdirmi { get; set; }
        public Nullable<int> AidOlduguUstCategoryId { get; set; }
        public string Sekil { get; set; }
    }
}
