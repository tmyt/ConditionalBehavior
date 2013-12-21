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
                // 左が文字列 && 右が非文字列 の場合、右の型に合わせる
                if (LeftValue is string && !(RightValue is string))
                {
                    LeftValue = TypeConverter.ChangeType(RightValue.GetType(), LeftValue);
                }
                // そうでない場合、左の型に合わせる
                else
                {
                    RightValue = TypeConverter.ChangeType(LeftValue.GetType(), RightValue);
                }
                // 結果が比較可能でない場合false
                if (LeftValue as IComparable == null || RightValue as IComparable == null) return false;
                return Compare(LeftValue as IComparable, RightValue as IComparable);
            }
        }
    }

    public class ConditionCollection : DependencyObjectCollection
    {
        // nothing to do.
    }
}
