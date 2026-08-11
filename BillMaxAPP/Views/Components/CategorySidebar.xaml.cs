using BillMaxAPP.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace BillMaxAPP.Views.Components;

public partial class CategorySidebar : ContentView
{
    public CategorySidebar()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty CategoriesProperty =
        BindableProperty.Create(
            nameof(Categories),
            typeof(IEnumerable<CategoryOption>),
            typeof(CategorySidebar),
            default(IEnumerable<CategoryOption>));

    public IEnumerable<CategoryOption> Categories
    {
        get => (IEnumerable<CategoryOption>)GetValue(CategoriesProperty);
        set => SetValue(CategoriesProperty, value);
    }

    public static readonly BindableProperty SelectCommandProperty =
        BindableProperty.Create(
            nameof(SelectCommand),
            typeof(ICommand),
            typeof(CategorySidebar));

    public ICommand SelectCommand
    {
        get => (ICommand)GetValue(SelectCommandProperty);
        set => SetValue(SelectCommandProperty, value);
    }
}