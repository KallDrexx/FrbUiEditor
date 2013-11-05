using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrbUi.Xml.Models;
using GalaSoft.MvvmLight;
using Xom.Core;

namespace FrbUiEditor.Core.ViewModel
{
    public class UiStructureViewModel : ViewModelBase
    {
        private List<UiNode> _rootNode;

        public UiStructureViewModel()
        {
            var reader = new XomReader();
            var nodes = reader.GenerateNodes(typeof (AssetCollection));
            var rootNode = nodes.First(x => x.IsRoot);

            RootNode = new List<UiNode>
            {
                new UiNode(rootNode, "root")
            };
        }

        public List<UiNode> RootNode
        {
            get { return _rootNode; }
            set { Set(() => RootNode, ref _rootNode, value); }
        }
    }
}
