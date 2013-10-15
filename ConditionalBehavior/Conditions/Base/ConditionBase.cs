using System;
using Windows.UI.Xaml;

namespace Gears.ConditionalBehavior.Conditions.Base
{
    public abstract class ConditionBase : DependencyObject
    {
        public abstract bool Result { get; }
    }

    public abstract class TwoValueConditionBase : ConditionBase
    {
        public object LeftValue
        {
            get { return GetValue(LeftValueProperty); }
            set { SetValue(LeftValueProperty, value); }
        }
        public object RightValue
        {
            get { return GetValue(RightValueProperty); }
            set { SetValue(RightValueProperty, value); }
        }

        public static readonly DependencyProperty LeftValueProperty =
            DependencyProperty.Register("LeftValue", typeof(object), typeof(TwoValueConditionBase), new PropertyMetadata(null));

        public static readonly DependencyProperty RightValueProperty =
            DependencyProperty.Register("RightValue", typeof(object), typeof(TwoValueConditionBase), new PropertyMetadata(null));
    }

    public abstract class ComparableConditionBase : TwoValueConditionBase
    {
        protected abstract bool Compare(IComparable lhs, IComparable rhs);

        public override bool Result
        {
            get
            {
                if (LeftValue as IComparable == null || RightValue as IComparable == null) return false;
                return Compare(LeftValue as IComparable, RightValue as IComparable);
            }
        }
    }

    public class ConditonCollection : DependencyObjectCollection
    {
        // nothing to do.
    }
}
