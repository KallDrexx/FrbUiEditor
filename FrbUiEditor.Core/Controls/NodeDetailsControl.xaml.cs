using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls.Data.PropertyGrid;

namespace FrbUiEditor.Core.Controls
{
    /// <summary>
    /// Interaction logic for NodeDetailsControl.xaml
    /// </summary>
    public partial class NodeDetailsControl : UserControl
    {
        public NodeDetailsControl()
        {
            InitializeComponent();
        }

        private void RadPropertyGrid_AutoGeneratingPropertyDefinition(object sender, AutoGeneratingPropertyDefinitionEventArgs e)
        {
            var type = e.PropertyDefinition.SourceProperty.PropertyType;

            var description = new StringBuilder();
            AppendFriendlyTypeName(type, description);
            e.PropertyDefinition.Description = description.ToString();
        }

        private void AppendFriendlyTypeName(Type type, StringBuilder description)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                description.Append("Nullable");
            else if (type == typeof (Single))
                description.Append("float");
            else
                description.Append(type.Name);

            if (type.IsGenericType)
            {
                description.Append("<");

                var genericArguments = type.GetGenericArguments();
                for (int x = 0; x < genericArguments.Length; x++)
                {
                    if (x > 0)
                        description.Append(",");

                    AppendFriendlyTypeName(genericArguments[0], description);
                }

                description.Append(">");
            }
        }
    }
}
