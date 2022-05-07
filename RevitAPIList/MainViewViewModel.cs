using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPIList
{
    public class MainViewViewModel
    {
        private ExternalCommandData _commandData;
        private Document _doc;
        private FamilySymbol selectedTitleType;
        private int quantity;
        private string designer;
        private ViewPlan selectedView;

        public List<ViewSheet> ViewSheets { get; set; } = new List<ViewSheet>();
        public List<FamilySymbol> TitleBlockTypes { get; } = new List<FamilySymbol>();
        public FamilySymbol SelectedTitleType { get => selectedTitleType; set => selectedTitleType = value; }
        public List<ViewPlan> Views { get; } = new List<ViewPlan>();
        public ViewPlan SelectedView { get => selectedView; set => selectedView = value; }
        public DelegateCommand SaveCommand { get; }
        public int Quantity { get => quantity; set => quantity = value; }
        public string Designer { get => designer; set => designer = value; }
        public MainViewViewModel(ExternalCommandData commandData)
        {
            _commandData = commandData;
            _doc = _commandData.Application.ActiveUIDocument.Document;
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Views = ViewsUtils.GetFloorPlanViews(_doc);
            TitleBlockTypes = TitleBlocksUtils.GetTitleBlockTypes(_commandData);
            SaveCommand = new DelegateCommand(OnSaveCommand);
        }
        private void OnSaveCommand()
        {
            UIApplication uiapp = _commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            if (SelectedTitleType == null || quantity < 1)
                return;
            XYZ insertPoint = new XYZ(1, 0.9, 0);
            using (var ts = new Transaction(_doc, "Создать листы"))
            {
                ts.Start();

                for (int i = 0; i < quantity; i++)
                {

                    ViewSheet viewsheet = ViewSheet.Create(_doc, SelectedTitleType.Id);

                    Parameter dsr = viewsheet.get_Parameter(BuiltInParameter.SHEET_DESIGNED_BY);
                    dsr.Set(designer);
                    if (i <= 0)
                    {
                        Viewport.Create(doc, viewsheet.Id, selectedView.Id, insertPoint);
                    }
                    else
                    {
                        selectedView = DuplicateView.CreateDependentCopy(selectedView);
                        Viewport.Create(doc, viewsheet.Id, selectedView.Id, insertPoint);
                    }

                }

                ts.Commit();
            }

            System.Windows.Controls.Grid viewGrid = MVUtils.CreateGrid();

            for (int i = 0; i < quantity; i++)
            {

                MVUtils.CreateRow(viewGrid, i, Views, i);
            }

            RaiseCloseRequest();

        }

        private void OnAddViewsCommand()
        {
            using (var ts = new Transaction(_doc, "Создать листы"))
            {
                ts.Start();




                ts.Commit();
            }
            RaiseCloseRequest();

        }


        public event EventHandler CloseRequest;
        private void RaiseCloseRequest()
        {
            CloseRequest?.Invoke(this, EventArgs.Empty);
        }
    }

}