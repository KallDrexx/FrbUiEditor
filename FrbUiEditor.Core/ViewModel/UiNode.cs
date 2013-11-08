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
        private string _name;
        private object _attributeData;

        public UiNode(XomNode node, string name)
        {
            _children = new ObservableCollection<UiNode>();
            _xomNode = node;
            Name = name;

            var attributes = _xomNode.Attributes;
            var type = XomAttributeTypeGenerator.GenerateType(attributes);
            var instance = Activator.CreateInstance(type);
            AttributeData = instance;
        }

        public IEnumerable<UiNode> Children { get { return _children; } }
        public XomNode XomNode { get { return _xomNode; } }

        public string Name
        {
            get { return _name; }
            set { Set(() => Name, ref _name, value); }
        }

        public object AttributeData
        {
            get { return _attributeData; }
            private set { Set(() => AttributeData, ref _attributeData, value); }
        }

        public void CreateChild(XomNode xomNode, string name)
        {
            var newNode = new UiNode(xomNode, name);
            _children.Add(newNode);
        }
    }
}
