using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Xom.Core.Models;

namespace FrbUiEditor.Core.ViewModel
{
    public class UiNode : ViewModelBase
    {
        private readonly ObservableCollection<UiNode> _children;
        private readonly XomNode _xomNode;
        private string _name;
        private bool _isSelected;
        private bool _isExpanded;
        private bool _isLoaded;

        public UiNode(XomNode node, string name)
        {
            _children = new ObservableCollection<UiNode>();
            _xomNode = node;
            Name = name;

            if (node.Children.Any())
                _children.Add(new UiNode());
        }

        /// <summary>
        /// Constructor meant for creating dummy children
        /// </summary>
        private UiNode() {}

        public IEnumerable<UiNode> Children { get { return _children; } }

        public string Name
        {
            get { return _name; }
            set { Set(() => Name, ref _name, value); }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set { Set(() => IsSelected, ref _isSelected, value); }
        }

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                Set(() => IsExpanded, ref _isExpanded, value);
                if (_isExpanded && !_isLoaded)
                    LoadChildren();
            }
        }

        private void LoadChildren()
        {
            _children.Clear();

            var childNodes = _xomNode.Children
                                     .SelectMany(x => x.AvailableNodes)
                                     .OrderBy(x => x.Key);

            foreach (var keyValuePair in childNodes)
                _children.Add(new UiNode(keyValuePair.Value, keyValuePair.Key));

            _isLoaded = true;
        }
    }
}
