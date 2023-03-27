using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAA2_Module_04_Review
{
    internal static class Utils
    {
        internal static RibbonPanel CreateRibbonPanel(UIControlledApplication app, string tabName, string panelName)
        {
            RibbonPanel currentPanel = GetRibbonPanelByName(app, tabName, panelName);

            if (currentPanel == null)
                currentPanel = app.CreateRibbonPanel(tabName, panelName);

            return currentPanel;
        }

        internal static IEnumerable<Element> GetAllColumns(Document doc)
        {
            List<BuiltInCategory> catList = new List<BuiltInCategory> { 
                BuiltInCategory.OST_StructuralColumns, BuiltInCategory.OST_Columns };

            ElementMulticategoryFilter catFilter = new ElementMulticategoryFilter(catList);

            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.WhereElementIsNotElementType();
            collector.WherePasses(catFilter);

            return collector.ToList();
        }

        internal static IEnumerable<Element> GetAllFraming(Document doc)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfCategory(BuiltInCategory.OST_StructuralFraming);
            collector.WhereElementIsNotElementType();

            return collector.ToList();
        }

        internal static List<Level> GetAllLevels(Document doc)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(Level));
            collector.WhereElementIsNotElementType();

            return collector.Cast<Level>().ToList();
        }

        internal static IEnumerable<Element> GetAllWalls(Document doc)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfCategory(BuiltInCategory.OST_Walls);
            collector.WhereElementIsNotElementType();

            return collector.ToList();
        }

        internal static FillPatternElement GetFillPatternByName(Document doc, string name)
        {
            FillPatternElement curFPE = null;

            curFPE = FillPatternElement.GetFillPatternElementByName(doc, FillPatternTarget.Drafting, name);

            return curFPE;
        }

        internal static RibbonPanel GetRibbonPanelByName(UIControlledApplication app, string tabName, string panelName)
        {
            foreach (RibbonPanel tmpPanel in app.GetRibbonPanels(tabName))
            {
                if (tmpPanel.Name == panelName)
                    return tmpPanel;
            }

            return null;
        }
    }
}
