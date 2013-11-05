using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace FrbUiEditor.Core.ViewModel
{
    public class MenuItem : ViewModelBase
    {
        private string _text;

        public string Text
        {
            get { return _text; }
            set { Set(() => Text, ref _text, value); }
        }
    }
}
