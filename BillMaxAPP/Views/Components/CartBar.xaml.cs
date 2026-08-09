using System.Windows.Input;

namespace BillMaxAPP.Views.Components;

public partial class CartBar : ContentView
{
    public CartBar()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty ItemCountProperty =
        BindableProperty.Create(nameof(ItemCount), typeof(int), typeof(CartBar), 0);

    public int ItemCount
    {
        get => (int)GetValue(ItemCountProperty);
        set => SetValue(ItemCountProperty, value);
    }

    public static readonly BindableProperty CartTotalProperty =
        BindableProperty.Create(nameof(CartTotal), typeof(decimal), typeof(CartBar), 0m);

    public decimal CartTotal
    {
        get => (decimal)GetValue(CartTotalProperty);
        set => SetValue(CartTotalProperty, value);
    }

    public static readonly BindableProperty TapCommandProperty =
        BindableProperty.Create(nameof(TapCommand), typeof(ICommand), typeof(CartBar));

    public ICommand TapCommand
    {
        get => (ICommand)GetValue(TapCommandProperty);
        set => SetValue(TapCommandProperty, value);
    }
}