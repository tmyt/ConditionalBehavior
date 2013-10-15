using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;
using Gears.ConditionalBehavior.Conditions.Base;
using Microsoft.Xaml.Interactivity;

namespace Gears.ConditionalBehavior
{
    [ContentProperty(Name = "Condition")]
    public class If : DependencyObject, IAction
    {
        public If()
        {
            Then = new ActionCollection();
            Else = new ActionCollection();
        }

        public ConditionBase Condition
        {
            get { return (ConditionBase)GetValue(ConditionProperty); }
            set { SetValue(ConditionProperty, value); }
        }
        public ActionCollection Then
        {
            get { return (ActionCollection)GetValue(ThenProperty); }
            private set { SetValue(ThenProperty, value); }
        }
        public ActionCollection Else
        {
            get { return (ActionCollection)GetValue(ElseProperty); }
            private set { SetValue(ElseProperty, value); }
        }

        public static readonly DependencyProperty ConditionProperty =
            DependencyProperty.Register("Condition", typeof(ConditionBase), typeof(If), new PropertyMetadata(null));
        public static readonly DependencyProperty ThenProperty =
            DependencyProperty.Register("Then", typeof(ActionCollection), typeof(If), new PropertyMetadata(null));
        public static readonly DependencyProperty ElseProperty =
            DependencyProperty.Register("Else", typeof(ActionCollection), typeof(If), new PropertyMetadata(null));

        private ActionCollection Evalute()
        {
            return (Condition == null || Condition.Result) ? Then : Else;
        }

        public object Execute(object sender, object parameter)
        {
            return Evalute().OfType<IAction>().Select(a => a.Execute(sender, parameter)).LastOrDefault();
        }
    }
}
