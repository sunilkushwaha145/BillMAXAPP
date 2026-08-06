using BillMaxAPP.Models;
using System.Collections.Generic;

namespace BillMaxAPP.Views.Components;

public partial class TopStoreCard : ContentView
{
    public TopStoreCard()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty TopStoresProperty =
        BindableProperty.Create(
            nameof(TopStores),
            typeof(IEnumerable<TopStore>),
            typeof(TopStoreCard),
            default(IEnumerable<TopStore>));

    public IEnumerable<TopStore> TopStores
    {
        get => (IEnumerable<TopStore>)GetValue(TopStoresProperty);
        set => SetValue(TopStoresProperty, value);
    }
}