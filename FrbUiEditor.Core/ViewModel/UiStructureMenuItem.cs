using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Xom.Core.Models;

namespace FrbUiEditor.Core.ViewModel
{
    public class UiStructureMenuItem : ViewModelBase
    {
        private string _text;

        public UiStructureMenuItem()
        {
            Command = new RelayCommand(() =>
            {
                if (UiNode != null)
                {
                    UiNode.CreateChild(XomNode, Text);
                }
            });
        }

        public string Text
        {
            get { return _text; }
            set { Set(() => Text, ref _text, value); }
        }

        public XomNode XomNode { get; set; }

        public UiNode UiNode { get; set; }

        public RelayCommand Command { get; private set; }
    }
}
