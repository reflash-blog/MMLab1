using System.Windows;

namespace MMLab1.View.Plot
{
    public class SeriesTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ConcetrationTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {

            if (item is ConcentrationCell)
            {
                    return ConcetrationTemplate;
            }

            // default
            return null;

        }
    }
}
