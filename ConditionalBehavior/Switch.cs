using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;
using Gears.ConditionalBehavior.Conditions.Base;
using Microsoft.Xaml.Interactivity;

namespace Gears.ConditionalBehavior
{
    [ContentProperty(Name = "Cases")]
    public class Switch : DependencyObject, IAction
    {
        public Switch()
        {
            Cases = new CaseCollection();
        }

        public CaseCollection Cases
        {
            get { return (CaseCollection)GetValue(CasesProperty); }
            set { SetValue(CasesProperty, value); }
        }

        public static readonly DependencyProperty CasesProperty =
            DependencyProperty.Register("Cases", typeof(CaseCollection), typeof(Switch), new PropertyMetadata(null));

        private ActionCollection Evalute()
        {
            var c = Cases.OfType<Case>().FirstOrDefault(@case => @case.Condition != null && @case.Condition.Result);
            return c == null ? new ActionCollection() : c.Actions;
        }

        public object Execute(object sender, object parameter)
        {
            return Evalute().OfType<IAction>().Select(a => a.Execute(sender, parameter)).LastOrDefault();
        }
    }

    [ContentProperty(Name = "Condition")]
    public class Case : DependencyObject
    {
        public Case()
        {
            Actions = new ActionCollection();
        }

        public ConditionBase Condition
        {
            get { return (ConditionBase)GetValue(ConditionProperty); }
            set { SetValue(ConditionProperty, value); }
        }
        public ActionCollection Actions
        {
            get { return (ActionCollection)GetValue(ActionsProperty); }
            set { SetValue(ActionsProperty, value); }
        }

        public static readonly DependencyProperty ConditionProperty =
            DependencyProperty.Register("Condition", typeof(ConditionBase), typeof(Case), new PropertyMetadata(null));
        public static readonly DependencyProperty ActionsProperty =
            DependencyProperty.Register("Actions", typeof(ActionCollection), typeof(Case), new PropertyMetadata(null));
    }

    public class CaseCollection : DependencyObjectCollection
    {
        // nothing to do.
    }
}
