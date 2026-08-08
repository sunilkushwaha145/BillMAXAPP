using BillMaxAPP.Models;
using System.Collections.Generic;
using System.Linq;

namespace BillMaxAPP.Views.Components;

public partial class LowStockCard : ContentView
{
    public LowStockCard()
    {
        InitializeComponent();
    }

    // =======================
    // LowStockProducts
    // =======================
    public static readonly BindableProperty LowStockProductsProperty =
        BindableProperty.Create(
            nameof(LowStockProducts),
            typeof(IEnumerable<LowStockProductDto>),
            typeof(LowStockCard),
            default(IEnumerable<LowStockProductDto>),
            propertyChanged: OnLowStockProductsChanged);

    public IEnumerable<LowStockProductDto> LowStockProducts
    {
        get => (IEnumerable<LowStockProductDto>)GetValue(LowStockProductsProperty);
        set => SetValue(LowStockProductsProperty, value);
    }

    // =======================
    // Count (auto-derived, shown in the red badge)
    // =======================
    public static readonly BindableProperty CountProperty =
        BindableProperty.Create(
            nameof(Count),
            typeof(int),
            typeof(LowStockCard),
            0);

    public int Count
    {
        get => (int)GetValue(CountProperty);
        private set => SetValue(CountProperty, value);
    }

    private static void OnLowStockProductsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is LowStockCard card)
        {
            card.Count = (newValue as IEnumerable<LowStockProductDto>)?.Count() ?? 0;
        }
    }
}
