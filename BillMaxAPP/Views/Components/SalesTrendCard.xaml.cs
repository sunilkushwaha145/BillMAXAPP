using BillMaxAPP.Models;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;

namespace BillMaxAPP.Views.Components;

public partial class SalesTrendCard : ContentView
{
    public SalesTrendCard()
    {
        InitializeComponent();
    }

    // ===================================
    // Trend Data
    // ===================================
    public static readonly BindableProperty TrendDataProperty =
        BindableProperty.Create(
            nameof(TrendData),
            typeof(IEnumerable<SalesTrend>),
            typeof(SalesTrendCard),
            default(IEnumerable<SalesTrend>),
            propertyChanged: OnTrendDataChanged);

    public IEnumerable<SalesTrend> TrendData
    {
        get => (IEnumerable<SalesTrend>)GetValue(TrendDataProperty);
        set => SetValue(TrendDataProperty, value);
    }

    // ===================================
    // Chart Series
    // ===================================
    public static readonly BindableProperty SeriesProperty =
        BindableProperty.Create(nameof(Series), typeof(ISeries[]), typeof(SalesTrendCard), new ISeries[0]);

    public ISeries[] Series
    {
        get => (ISeries[])GetValue(SeriesProperty);
        set => SetValue(SeriesProperty, value);
    }

    // ===================================
    // Chart X Axes
    // ===================================
    public static readonly BindableProperty XAxesProperty =
        BindableProperty.Create(nameof(XAxes), typeof(Axis[]), typeof(SalesTrendCard), new Axis[0]);

    public Axis[] XAxes
    {
        get => (Axis[])GetValue(XAxesProperty);
        set => SetValue(XAxesProperty, value);
    }

    // ===================================
    // Week Command
    // ===================================
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

    // ===================================
    // Month Command
    // ===================================
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

    // ===================================
    // Year Command
    // ===================================
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

    // ===================================
    // Chart Build Logic
    // ===================================
    private static void OnTrendDataChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is SalesTrendCard card)
        {
            if (oldValue is INotifyCollectionChanged oldObservable)
                oldObservable.CollectionChanged -= card.OnCollectionChanged;

            if (newValue is INotifyCollectionChanged newObservable)
                newObservable.CollectionChanged += card.OnCollectionChanged;

            card.BuildChart(newValue as IEnumerable<SalesTrend>);
        }
    }

    private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        BuildChart(TrendData);
    }

    private void BuildChart(IEnumerable<SalesTrend>? data)
    {
        var list = data?.ToList() ?? new List<SalesTrend>();

        Series = new ISeries[]
        {
            new LineSeries<decimal>
            {
                Values = list.Select(x => x.TotalSales).ToArray(),
                Name = "Sales",
                GeometrySize = 6,
                Fill = null
            }
        };

        XAxes = new Axis[]
        {
            new Axis { Labels = list.Select(x => x.SaleRang).ToArray() }
        };
    }
}