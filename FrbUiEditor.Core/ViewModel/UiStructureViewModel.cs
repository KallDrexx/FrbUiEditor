using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace FrbUiEditor.Core.ViewModel
{
    public class UiStructureViewModel : ViewModelBase
    {
        private List<UiNode> rootNode;

        public UiStructureViewModel()
        {
            RootNode = new List<UiNode>
            {
                new UiNode
                {
                    Name = "Root",
                    Children = new List<UiNode>
                    {
                        new UiNode {Name = "Child 1"},
                        new UiNode {Name = "Child 2"},
                        new UiNode {Name = "Child 3"}
                    }
                }
            };
        }

        public List<UiNode> RootNode
        {
            get { return rootNode; }
            set { Set(() => RootNode, ref rootNode, value); }
        }
    }
}
