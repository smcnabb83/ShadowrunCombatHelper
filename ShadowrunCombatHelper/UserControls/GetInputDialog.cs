using ShadowrunCombatHelper.UserControls.GetInputDialogUI;
using System;

namespace ShadowrunCombatHelper.UserControls
{
    internal class GetInputDialog<T> where T : IConvertible
    {
        public static T Show(string inputMessage, string Title)
        {
            var dialog = new GetInputDialog_Window(inputMessage, Title);
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