using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using DiplomProject.ViewModels;

namespace DiplomProject
{
    public class ViewLocator : IDataTemplate
    {
        public Control? Build(object? param)
        {
            if (param is null)
                return new TextBlock { Text = "DataContext is null" };

            var name = param.GetType().FullName!
                .Replace("ViewModel", "View", StringComparison.Ordinal);
            var type = Type.GetType(name);

            return type != null
                ? (Control)Activator.CreateInstance(type)!
                : new TextBlock { Text = "View Not Found: " + name };
        }

        public bool Match(object? data) => data is ViewModelBase;
    }
}
