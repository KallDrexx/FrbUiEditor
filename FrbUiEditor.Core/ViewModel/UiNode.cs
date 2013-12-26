using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using FrbUiEditor.Core.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Xom.Core;
using Xom.Core.Models;

namespace FrbUiEditor.Core.ViewModel
{
    public class UiNode : ViewModelBase
    {
        private readonly ObservableCollection<UiNode> _children;
        private readonly XomNode _xomNode;
        private string _displayedName, _baseName;
        private object _attributeData;
        private bool _isExpanded;

        public UiNode(XomNode node, string name)
        {
            _children = new ObservableCollection<UiNode>();
            _xomNode = node;
            _baseName = name;
            IsExpanded = true;

            var attributes = _xomNode.Attributes;
            var type = XomAttributeTypeGenerator.GenerateType(attributes);
            var instance = Activator.CreateInstance(type);
            AttributeData = instance;

            UpdateNodeName();
        }

        public IEnumerable<UiNode> Children { get { return _children; } }
        public XomNode XomNode { get { return _xomNode; } }

        public string Name
        {
            get { return _displayedName; }
            set { Set(() => Name, ref _displayedName, value); }
        }

        public object AttributeData
        {
            get { return _attributeData; }
            private set { Set(() => AttributeData, ref _attributeData, value); }
        }

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set { Set(() => IsExpanded, ref _isExpanded, value); }
        }

        public void CreateChild(XomNode xomNode, string name)
        {
            var newNode = new UiNode(xomNode, name);
            _children.Add(newNode);
        }

        public void UpdateNodeName()
        {
            string calculatedName = string.Empty;

            // If the node has an attribute named "Name" and its' a string, use that as its name
            if (_attributeData != null)
            {
                var namePropertyValue = _attributeData.GetType()
                                                     .GetProperties()
                                                     .Where(x => x.Name == "Name")
                                                     .Where(x => x.PropertyType == typeof(string))
                                                     .Select(x => x.GetValue(_attributeData) as string)
                                                     .FirstOrDefault();

                if (!string.IsNullOrWhiteSpace(namePropertyValue))
                    calculatedName = namePropertyValue;
            }

            if (string.IsNullOrWhiteSpace(calculatedName))
                calculatedName = _baseName;

            Name = calculatedName;
        }

        public XomNodeData CreateDataNode()
        {
            return new XomNodeData
            {
                NodeType = _xomNode,
                AttributeData = _attributeData,
                ChildNodes = Children.Select(x => new KeyValuePair<string, XomNodeData>(x.Name, x.CreateDataNode()))
                                     .ToArray()

            };
        }

        public static UiNode FromXomNodeData(XomNodeData nodeData, string name)
        {
            if (nodeData == null)
                throw new ArgumentNullException("nodeData");

            var node = new UiNode(nodeData.NodeType, name);
            node.AttributeData = nodeData.AttributeData;
            node.UpdateNodeName();
            foreach (var child in nodeData.ChildNodes)
            {
                var childNodeName = nodeData.NodeType
                                            .Children
                                            .Where(x => x.PropertyName == child.Key)
                                            .SelectMany(x => x.AvailableNodes)
                                            .Where(x => x.Value.Type == child.Value.NodeType.Type)
                                            .Select(x => x.Key)
                                            .First();

                var childNode = UiNode.FromXomNodeData(child.Value, childNodeName);
                node._children.Add(childNode);
            }

            return node;
        }
    }
}
