using BillMaxAPP.Models;
using System.Windows.Input;

namespace BillMaxAPP.Views.Components;

public partial class ProductCard : ContentView
{
    public ProductCard()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty ProductProperty =
        BindableProperty.Create(
            nameof(Product),
            typeof(Product),
            typeof(ProductCard),
            default(Product));

    public Product Product
    {
        get => (Product)GetValue(ProductProperty);
        set => SetValue(ProductProperty, value);
    }

    public static readonly BindableProperty AddCommandProperty =
        BindableProperty.Create(
            nameof(AddCommand),
            typeof(ICommand),
            typeof(ProductCard));

    public ICommand AddCommand
    {
        get => (ICommand)GetValue(AddCommandProperty);
        set => SetValue(AddCommandProperty, value);
    }

    public static readonly BindableProperty FavoriteCommandProperty =
        BindableProperty.Create(
            nameof(FavoriteCommand),
            typeof(ICommand),
            typeof(ProductCard));

    public ICommand FavoriteCommand
    {
        get => (ICommand)GetValue(FavoriteCommandProperty);
        set => SetValue(FavoriteCommandProperty, value);
    }
}