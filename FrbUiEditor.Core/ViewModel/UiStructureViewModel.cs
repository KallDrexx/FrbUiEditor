using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrbUi.Xml.Models;
using FrbUiEditor.Core.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Xom.Core;

namespace FrbUiEditor.Core.ViewModel
{
    public class UiStructureViewModel : ViewModelBase
    {
        private readonly ObservableCollection<MenuItem> _menuItems;
        private List<UiNode> _rootNode;
        private bool _menuOpened;

        public UiStructureViewModel()
        {
            _menuItems = new ObservableCollection<MenuItem>();
            var reader = new XomReader();
            var nodes = reader.GenerateNodes(typeof (AssetCollection));
            var rootNode = nodes.First(x => x.IsRoot);

            RootNode = new List<UiNode>
            {
                new UiNode(rootNode, "root")
            };

            Messenger.Default.Register<UiNodeSelectedMessage>(this, HandleNodeSelectedMessage);
        }

        public IEnumerable<MenuItem> MenuItems { get { return _menuItems; } }
        public bool HasMenuItems { get { return _menuItems.Any(); } }

        public List<UiNode> RootNode
        {
            get { return _rootNode; }
            set { Set(() => RootNode, ref _rootNode, value); }
        }

        public bool IsMenuOpened
        {
            get { return _menuOpened; }
            set { Set(() => IsMenuOpened, ref _menuOpened, value); }
        }

        private void HandleNodeSelectedMessage(UiNodeSelectedMessage message)
        {
            _menuItems.Clear();

            if (message.SelectedNode == null)
                return;

            var node = message.SelectedNode;
            var childMenuItems = node.XomNode
                                     .Children
                                     .SelectMany(x => x.AvailableNodes)
                                     .Select(x => new MenuItem
                                     {
                                         Text = x.Key
                                     })
                                     .ToArray();

            foreach (var childMenuItem in childMenuItems)
                _menuItems.Add(childMenuItem);

            IsMenuOpened = false;
            RaisePropertyChanged(() => HasMenuItems);
        }
    }
}
