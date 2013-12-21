using System;
using System.Linq;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;
using Gears.ConditionalBehavior.Conditions.Base;
using Microsoft.Xaml.Interactions.Core;

namespace Gears.ConditionalBehavior.Conditions
{
    public class True : ConditionBase
    {
        public override bool Result { get { return true; } }
    }

    public class False : ConditionBase
    {
        public override bool Result { get { return false; } }
    }

    public class Equals : TwoValueConditionBase
    {
        public override bool Result
        {
            get
            {
                if (LeftValue != null && RightValue != null)
                {
                    if (LeftValue is string && !(RightValue is string))
                    {
                        var value = TypeConverter.ChangeType(RightValue.GetType(), LeftValue);
                        return RightValue.Equals(value);
                    }
                    if (!(LeftValue is string) && RightValue is string)
                    {
                        var value = TypeConverter.ChangeType(LeftValue.GetType(), RightValue);
                        return LeftValue.Equals(value);
                    }
                }
                return (LeftValue != null && LeftValue.Equals(RightValue)) || LeftValue == RightValue;
            }
        }
    }

    public class GreaterThanEquals : ComparableConditionBase
    {
        protected override bool Compare(IComparable lhs, IComparable rhs)
        {
            return lhs.CompareTo(rhs) >= 0;
        }
    }

    public class LessThanEquals : ComparableConditionBase
    {
        protected override bool Compare(IComparable lhs, IComparable rhs)
        {
            return lhs.CompareTo(rhs) <= 0;
        }
    }

    public class GreaterThan : ComparableConditionBase
    {
        protected override bool Compare(IComparable lhs, IComparable rhs)
        {
            return lhs.CompareTo(rhs) > 0;
        }
    }

    public class LessThan : ComparableConditionBase
    {
        protected override bool Compare(IComparable lhs, IComparable rhs)
        {
            return lhs.CompareTo(rhs) < 0;
        }
    }

    [ContentProperty(Name = "Condition")]
    public class Negate : ConditionBase
    {
        public override bool Result
        {
            get
            {
                if (Condition == null) return false;
                return !Condition.Result;
            }
        }

        public ConditionBase Condition
        {
            get { return (ConditionBase)GetValue(ConditionProperty); }
            set { SetValue(ConditionProperty, value); }
        }

        public static readonly DependencyProperty ConditionProperty =
            DependencyProperty.Register("Condition", typeof(ConditionBase), typeof(Negate), new PropertyMetadata(null));
    }

    [ContentProperty(Name = "Conditions")]
    public class And : ConditionBase
    {
        public ConditonCollection Conditions
        {
            get { return (ConditonCollection)GetValue(ConditionsProperty); }
            private set { SetValue(ConditionsProperty, value); }
        }

        public static readonly DependencyProperty ConditionsProperty =
            DependencyProperty.Register("Conditions", typeof(ConditonCollection), typeof(And), new PropertyMetadata(null));


        public And()
        {
            Conditions = new ConditonCollection();
        }

        public override bool Result
        {
            get { return Conditions.OfType<ConditionBase>().Aggregate(true, (b, o) => b & o.Result); }
        }
    }

    [ContentProperty(Name = "Conditions")]
    public class Or : ConditionBase
    {
        public ConditonCollection Conditions
        {
            get { return (ConditonCollection)GetValue(ConditionsProperty); }
            private set { SetValue(ConditionsProperty, value); }
        }

        public static readonly DependencyProperty ConditionsProperty =
            DependencyProperty.Register("Conditions", typeof(ConditonCollection), typeof(Or), new PropertyMetadata(null));

        public Or()
        {
            Conditions = new ConditonCollection();
        }

        public override bool Result
        {
            get { return Conditions.OfType<ConditionBase>().Aggregate(false, (b, o) => b | o.Result); }
        }
    }
}
