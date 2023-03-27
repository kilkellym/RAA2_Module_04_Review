using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAA2_Module_04_Review
{
    internal class SetColor : IExternalEventHandler
    {
        public void Execute(UIApplication app)
        {
            UIDocument uidoc = app.ActiveUIDocument;
            Document doc = uidoc.Document;

            List<Element> selectedElements = new List<Element>();

            // get selected elements
            foreach(string selectedCat in Globals.SelectedCats)
            {
                if (selectedCat == "Columns")
                    selectedElements.AddRange(Utils.GetAllColumns(doc));

                if (selectedCat == "Framing")
                    selectedElements.AddRange(Utils.GetAllFraming(doc));

                if (selectedCat == "Walls")
                    selectedElements.AddRange(Utils.GetAllWalls(doc));
            }

            // get selected color
            Color newColor = new Color(255, 0, 0);

            switch(Globals.SelectedColor)
            {
                case "Yellow":
                    newColor = new Color(255, 255, 0);
                    break;

                case "Blue":
                    newColor = new Color(0, 0, 255);
                    break;

                case "Green":
                    newColor = new Color(0, 255, 0);
                    break;

                default:
                    newColor = new Color(255, 0, 0);
                    break;
            }

            OverrideGraphicSettings newSettings = new OverrideGraphicSettings();

            newSettings.SetCutForegroundPatternColor(newColor);
            newSettings.SetSurfaceForegroundPatternColor(newColor);

            FillPatternElement curPattern = Utils.GetFillPatternByName(doc, "<Solid Fill>");
            newSettings.SetCutForegroundPatternId(curPattern.Id);
            newSettings.SetSurfaceForegroundPatternId(curPattern.Id);

            using (Transaction t = new Transaction(doc))
            {
                t.Start("Set element color");
                foreach (Element curElem in selectedElements)
                {
                    if (curElem.LevelId.Equals(Globals.SelectedLevel.Id))
                    {
                        doc.ActiveView.SetElementOverrides(curElem.Id, newSettings);
                    }
                }
                t.Commit();
            }
        }

        public string GetName()
        {
            return "Set Color";
        }
    }

    internal class ResetColor : IExternalEventHandler
    {
        public void Execute(UIApplication app)
        {
            UIDocument uidoc = app.ActiveUIDocument;
            Document doc = uidoc.Document;

            List<Element> selectedElements = new List<Element>();

            // get selected elements
            foreach (string selectedCat in Globals.SelectedCats)
            {
                if (selectedCat == "Columns")
                    selectedElements.AddRange(Utils.GetAllColumns(doc));

                if (selectedCat == "Framing")
                    selectedElements.AddRange(Utils.GetAllFraming(doc));

                if (selectedCat == "Walls")
                    selectedElements.AddRange(Utils.GetAllWalls(doc));
            }

            OverrideGraphicSettings newSettings = new OverrideGraphicSettings();

            using (Transaction t = new Transaction(doc))
            {
                t.Start("Reset element color");
                foreach (Element curElem in selectedElements)
                {
                    doc.ActiveView.SetElementOverrides(curElem.Id, newSettings);
                }
                t.Commit();
            }
        }

        public string GetName()
        {
            return "Reset Color";
        }
    }
}
