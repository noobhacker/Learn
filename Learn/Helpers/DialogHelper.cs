using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Learn.Helpers
{
    public static class DialogHelper
    {
        public static async Task<int> ShowYesNoDialogAsync(string message)
        {
            var dialog = new MessageDialog(message);
            dialog.Title = "Confirmation";
            dialog.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
            dialog.Commands.Add(new UICommand { Label = "Cancel", Id = 1 });
            var res = await dialog.ShowAsync();
            return (int)res.Id;
        }
    }
}
