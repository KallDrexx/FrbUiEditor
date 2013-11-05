using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private readonly ObservableCollection<MenuItem> _menuItems;
        private List<UiNode> _rootNode;

        public UiStructureViewModel()
        {
            _menuItems = new ObservableCollection<MenuItem>(new []
            {
                new MenuItem {Text = "Item 1"},
                new MenuItem {Text = "Item 2"},
                new MenuItem {Text = "Item 3"}
            });

            var reader = new XomReader();
            var nodes = reader.GenerateNodes(typeof (AssetCollection));
            var rootNode = nodes.First(x => x.IsRoot);

            RootNode = new List<UiNode>
            {
                new UiNode(rootNode, "root")
            };
        }

        public IEnumerable<MenuItem> MenuItems { get { return _menuItems; } } 

        public List<UiNode> RootNode
        {
            get { return _rootNode; }
            set { Set(() => RootNode, ref _rootNode, value); }
        }
    }
}
