using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPIList
{
    public class TitleBlocksUtils
    {
        public static List<FamilySymbol> GetTitleBlockTypes(ExternalCommandData commandData)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;


            List<FamilySymbol> titleBlocks = new FilteredElementCollector(doc)
            .OfClass(typeof(FamilySymbol))
            .OfCategory(BuiltInCategory.OST_TitleBlocks)
            //.WhereElementIsNotElementType()
            .Cast<FamilySymbol>()
            .ToList();

            return titleBlocks;
        }
    }
}
