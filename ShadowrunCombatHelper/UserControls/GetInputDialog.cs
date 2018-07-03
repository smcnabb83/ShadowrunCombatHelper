using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowrunCombatHelper.UserControls.GetInputDialogUI;

namespace ShadowrunCombatHelper.UserControls
{
    class GetInputDialog<T> where T : IConvertible
    {
        
        public static T Show(string inputMessage, string Title)
        {
            GetInputDialog_Window dialog = new GetInputDialog_Window(inputMessage, Title);
            bool? result = dialog.ShowDialog();
            if (result ?? false)
            {
                return (T)Convert.ChangeType(dialog.Result, typeof(T));
            }
            else
            {
                return (T)Convert.ChangeType(0, typeof(T));
            }
        }

    }
}
