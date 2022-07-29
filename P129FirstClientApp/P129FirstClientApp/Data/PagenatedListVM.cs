using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P129FirstClientApp.Data
{
    public class PagenatedListVM<T>
    {
        //public PagenatedListVM(List<T> items, int pageIndex, int pageItemCount)
        //{
        //    TotalPage = (int)Math.Ceiling((double)items.Count / pageItemCount);
        //    PageIndex = pageIndex;
        //    HasNext = PageIndex < TotalPage;
        //    HasPrev = PageIndex > 1;
        //    Items.AddRange(items.Skip((pageIndex - 1) * pageItemCount).Take(pageItemCount));
        //}

        public List<T> items { get; set; } = new List<T>();
        public int TotalPage { get; set; }
        public int PageIndex { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrev { get; set; }
    }
}
