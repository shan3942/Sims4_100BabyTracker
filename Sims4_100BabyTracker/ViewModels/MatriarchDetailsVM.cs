using PagedList;
using Sims4_100BabyTracker.Models;

namespace Sims4_100BabyTracker.ViewModels
{
    public class MatriarchDetailsVM
    {
        public Matriarch Mother { get; set; }

        public IPagedList<Child> ChildrenList { get; set; }

        public IPagedList<string> DaddiesList { get; set; }
    }
}