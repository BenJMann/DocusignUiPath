using Microsoft.CSharp.Activities;
using System;
using System.Activities;
using System.Activities.Expressions;
using System.Activities.Presentation.Model;
using System.Globalization;
using System.Windows.Data;

namespace BenMann.Docusign.Activities.Design
{
    class ComboBoxConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ModelItem modelItem = value as ModelItem;
            if (value != null)
            {
                InArgument<string> inArgument = modelItem.GetCurrentValue() as InArgument<string>;

                if (inArgument != null)
                {
                    Activity<string> expression = inArgument.Expression;
                    CSharpValue<string> csexpression = expression as CSharpValue<string>;
                    Literal<string> literal = expression as Literal<string>;

                    if (literal != null)
                    {
                        return literal.Value;
                    }
                    else if (csexpression != null)
                    {
                        return csexpression.ExpressionText;
                    }
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            InArgument<string> inArgument = new InArgument<string>((string)value);
            return inArgument;
        }
    }
}
