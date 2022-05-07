using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPIList
{
    public class ViewsUtils
    {
        public static List<ViewPlan> GetFloorPlanViews(Document doc)
        {
            var views
               = new FilteredElementCollector(doc)
                   .OfClass(typeof(ViewPlan))
                   .Cast<ViewPlan>()
                   .Where(p => p.ViewType == ViewType.FloorPlan)
                   .ToList();
            return views;
        }
    }
}
