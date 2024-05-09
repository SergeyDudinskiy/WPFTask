using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    public interface IDialogService
    {
        bool OpenWindow();
        void CloseWindow();
        void ShowMessage(string message);
    }
}
