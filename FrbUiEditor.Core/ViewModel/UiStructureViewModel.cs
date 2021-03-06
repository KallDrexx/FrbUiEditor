﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrbUi.Xml.Models;
using FrbUiEditor.Core.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Xom.Core;
using Xom.Core.Models;
using System.Xml.Serialization;
using System.IO;

namespace FrbUiEditor.Core.ViewModel
{
    public class UiStructureViewModel : ViewModelBase
    {
        private readonly ObservableCollection<UiStructureMenuItem> _menuItems;
        private ObservableCollection<UiNode> _rootNode;
        private bool _menuOpened;
        private UiNode _selectedNode;

        public UiStructureViewModel()
        {
            _menuItems = new ObservableCollection<UiStructureMenuItem>();
            var reader = new XomReader();
            var nodes = reader.GenerateNodes(typeof(AssetCollection));
            var rootNode = nodes.First(x => x.IsRoot);

            RootNode = new ObservableCollection<UiNode>(new[]
            {
                CreateRootNodeFromFile(@"E:\temp\ui.xml")
            });

            TestCommand = new RelayCommand(() =>
            {
                var root = _rootNode.First();
                var serializer = new XomDataConverter();
                var nodeData = root.CreateDataNode();
                var serializedData = serializer.ConvertToXmlObject(nodeData);
                var xmlSerializer = new XmlSerializer(serializedData.GetType());
                using (var stream = File.OpenWrite(@"c:\temp\test.xml"))
                    xmlSerializer.Serialize(stream, serializedData);
            });
        }

        public IEnumerable<UiStructureMenuItem> MenuItems { get { return _menuItems; } }
        public bool HasMenuItems { get { return _menuItems.Any(); } }
        public RelayCommand TestCommand { get; private set; }

        public ObservableCollection<UiNode> RootNode
        {
            get { return _rootNode; }
            set { Set(() => RootNode, ref _rootNode, value); }
        }

        public bool IsMenuOpened
        {
            get { return _menuOpened; }
            set { Set(() => IsMenuOpened, ref _menuOpened, value); }
        }

        public UiNode SelectedNode
        {
            get { return _selectedNode; }
            set
            {
                var previousNode = _selectedNode;
                Set(() => SelectedNode, ref _selectedNode, value);
                UpdateMenuItems();
                Messenger.Default.Send(new UiNodeSelectedMessage { SelectedNode = value });

                if (previousNode != null)
                    previousNode.UpdateNodeName();
            }
        }

        private void UpdateMenuItems()
        {
            _menuItems.Clear();

            if (SelectedNode == null)
                return;

            var node = SelectedNode;
            var childMenuItems = node.XomNode
                                     .Children
                                     .SelectMany(x => x.AvailableNodes)
                                     .Select(x => CreateMenuItem(x, node))
                                     .ToArray();

            foreach (var childMenuItem in childMenuItems)
                _menuItems.Add(childMenuItem);

            IsMenuOpened = false;
            RaisePropertyChanged(() => HasMenuItems);
        }

        private UiStructureMenuItem CreateMenuItem(KeyValuePair<string, XomNode> xomNodePair, UiNode uiNode)
        {
            return new UiStructureMenuItem
            {
                Text = xomNodePair.Key,
                XomNode = xomNodePair.Value,
                UiNode = uiNode
            };
        }

        private UiNode CreateRootNodeFromFile(string filename)
        {
            AssetCollection collection;

            using (var stream = File.OpenRead(filename))
            {
                var serializer = new XmlSerializer(typeof(AssetCollection));
                collection = (AssetCollection)serializer.Deserialize(stream);
            }

            var converter = new XomDataConverter();
            var nodeData = converter.ConvertToXomNodeData(collection, new XomReader());
            var node = UiNode.FromXomNodeData(nodeData, "UI Package");

            return node;
        }
    }
}
