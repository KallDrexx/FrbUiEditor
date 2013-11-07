using System;
using FrbUiEditor.Core.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Xom.Core;

namespace FrbUiEditor.Core.ViewModel
{
    class NodeDetailsViewModel : ViewModelBase
    {
        private object _nodeData;

        public NodeDetailsViewModel()
        {
            Messenger.Default.Register<UiNodeSelectedMessage>(this, HandleUiNodeSelectedMessage);
        }

        public object NodeData
        {
            get { return _nodeData; }
            set { Set(() => NodeData, ref _nodeData, value); }
        }

        private void HandleUiNodeSelectedMessage(UiNodeSelectedMessage message)
        {
            var attributes = message.SelectedNode.XomNode.Attributes;
            var type = XomAttributeTypeGenerator.GenerateType(attributes, "SelectedType");
            var instance = Activator.CreateInstance(type);
            NodeData = instance;
        }
    }
}
