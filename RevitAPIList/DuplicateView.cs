using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPIList
{
    public class DuplicateView
    {
        public static ViewPlan CreateDependentCopy(ViewPlan view)
        {
            ViewPlan dependentView = null;
            ElementId newViewId = ElementId.InvalidElementId;
            if (view.CanViewBeDuplicated(ViewDuplicateOption.AsDependent))
            {
                newViewId = view.Duplicate(ViewDuplicateOption.AsDependent);
                dependentView = view.Document.GetElement(newViewId) as ViewPlan;
            }

            return dependentView;
        }
    }
}
