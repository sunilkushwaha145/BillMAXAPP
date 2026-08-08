using BillMaxAPP.Models;
using System.Collections;
using System.Windows.Input;

namespace BillMaxAPP.Views.Components;

public partial class RecentBillsCard : ContentView
{
    public RecentBillsCard()
    {
        InitializeComponent();
    }


    // ==============================
    // ItemsSource
    // ==============================

    public static readonly BindableProperty ItemsSourceProperty =
        BindableProperty.Create(
            nameof(ItemsSource),
            typeof(IEnumerable<RecentBillDto>),
            typeof(RecentBillsCard),
            default(IEnumerable<RecentBillDto>));

    public IEnumerable<RecentBillDto> ItemsSource
    {
        get => (IEnumerable<RecentBillDto>)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }


    // ==============================
    // View All Command
    // ==============================

    public static readonly BindableProperty ViewAllCommandProperty =
        BindableProperty.Create(
            nameof(ViewAllCommand),
            typeof(ICommand),
            typeof(RecentBillsCard));

    public ICommand ViewAllCommand
    {
        get => (ICommand)GetValue(ViewAllCommandProperty);
        set => SetValue(ViewAllCommandProperty, value);
    }


    // ==============================
    // View Command
    // ==============================

    public static readonly BindableProperty ViewCommandProperty =
        BindableProperty.Create(
            nameof(ViewCommand),
            typeof(ICommand),
            typeof(RecentBillsCard));

    public ICommand ViewCommand
    {
        get => (ICommand)GetValue(ViewCommandProperty);
        set => SetValue(ViewCommandProperty, value);
    }


    // ==============================
    // Print Command
    // ==============================

    public static readonly BindableProperty PrintCommandProperty =
        BindableProperty.Create(
            nameof(PrintCommand),
            typeof(ICommand),
            typeof(RecentBillsCard));

    public ICommand PrintCommand
    {
        get => (ICommand)GetValue(PrintCommandProperty);
        set => SetValue(PrintCommandProperty, value);
    }
}