using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace IpClient.Misc
{
    public class TextChangedBehavior : Behavior<Entry>
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create
        (
            propertyName: "Command",
            returnType: typeof(ICommand),
            declaringType: typeof(TextChangedBehavior)
        );

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += Bindable_TextChanged;
            bindable.BindingContextChanged += Bindable_BindingContextChanged;
        }

        private void Bindable_BindingContextChanged(object sender, EventArgs e)
        {
            var entry = sender as Entry;
            BindingContext = entry?.BindingContext;
        }

        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            Command?.Execute(null);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= Bindable_TextChanged;
            bindable.BindingContextChanged -= Bindable_BindingContextChanged;
        }
    }
}
