using System;
using FrbUiEditor.Core.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Telerik.Windows.Controls.Data.PropertyGrid;
using Xom.Core;
using Xom.Core.Models;

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
            NodeData = message.SelectedNode.AttributeData;
        }
    }
}
