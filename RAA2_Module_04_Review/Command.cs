#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

#endregion

namespace RAA2_Module_04_Review
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            // put any code needed for the form here
            List<Level> LevelList = Utils.GetAllLevels(doc);

            SetColor mySetColorAction = new SetColor();
            ExternalEvent mySetColorEvent = ExternalEvent.Create(mySetColorAction);

            ResetColor myResetColorAction = new ResetColor();
            ExternalEvent myResetColorEvent = ExternalEvent.Create(myResetColorAction);

            // open form
            MyForm currentForm = new MyForm(LevelList, myResetColorEvent, mySetColorEvent)
            {
                Width = 600,
                Height = 400,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
                Topmost = true,
            };

            currentForm.Show();

            // get form data and do something

            return Result.Succeeded;
        }

        public static String GetMethod()
        {
            var method = MethodBase.GetCurrentMethod().DeclaringType?.FullName;
            return method;
        }
    }
}
