using System;
using Xamarin.Forms;

namespace cardsbug.ViewModels
{
    public class CardsTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Template1 { get; set; }
        public DataTemplate Template2 { get; set; }
        public DataTemplate Template3 { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            switch (((Card)item).CardType)
            {
                case CardTypes.Type1: return Template1;

                case CardTypes.Type2: return Template2;

                default: return Template3;
            }
        }
    }
}
