using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace FrbUiEditor.Core.ViewModel
{
    public class UiNode : ViewModelBase
    {
        private string name;
        private readonly ObservableCollection<UiNode> children;

        public UiNode()
        {
            children = new ObservableCollection<UiNode>();
        }

        public string Name
        {
            get { return name; }
            set { Set(() => Name, ref name, value); }
        }

        public IEnumerable<UiNode> Children
        {
            get { return children; }
            set
            {
                children.Clear();
                foreach (var node in value)
                {
                    children.Add(node);
                }
            }
        }
    }
}
