using BillMaxAPP.Models;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Windows.Input;

namespace BillMaxAPP.Views.Components;

public partial class SalesTrendCard : ContentView
{
    public SalesTrendCard()
    {
        InitializeComponent();
    }

    // Trend Data
    public static readonly BindableProperty TrendDataProperty =
        BindableProperty.Create(
            nameof(TrendData),
            typeof(IEnumerable<SalesTrend>),
            typeof(SalesTrendCard),
            default(IEnumerable<SalesTrend>));

    public IEnumerable<SalesTrend> TrendData
    {
        get => (IEnumerable<SalesTrend>)GetValue(TrendDataProperty);
        set => SetValue(TrendDataProperty, value);
    }

    // Week Command
    public static readonly BindableProperty WeekCommandProperty =
        BindableProperty.Create(
            nameof(WeekCommand),
            typeof(ICommand),
            typeof(SalesTrendCard));

    public ICommand? WeekCommand
    {
        get => (ICommand?)GetValue(WeekCommandProperty);
        set => SetValue(WeekCommandProperty, value);
    }

    // Month Command
    public static readonly BindableProperty MonthCommandProperty =
        BindableProperty.Create(
            nameof(MonthCommand),
            typeof(ICommand),
            typeof(SalesTrendCard));

    public ICommand? MonthCommand
    {
        get => (ICommand?)GetValue(MonthCommandProperty);
        set => SetValue(MonthCommandProperty, value);
    }

    // Year Command
    public static readonly BindableProperty YearCommandProperty =
        BindableProperty.Create(
            nameof(YearCommand),
            typeof(ICommand),
            typeof(SalesTrendCard));

    public ICommand? YearCommand
    {
        get => (ICommand?)GetValue(YearCommandProperty);
        set => SetValue(YearCommandProperty, value);
    }
}