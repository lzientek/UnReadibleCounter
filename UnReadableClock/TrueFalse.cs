using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace UnReadableClock
{
    public class TrueFalse:DataTemplateSelector
    {
        public DataTemplate Normal { get; set; }
        public DataTemplate Autre { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if ((bool)item)
            {
                return Autre;
            }
            return Normal;
        }
    }
}
