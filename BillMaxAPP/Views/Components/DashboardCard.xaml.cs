using Microsoft.Maui.Controls;

namespace BillMaxAPP.Views.Components;

public partial class DashboardCard : ContentView
{
    public DashboardCard()
    {
        InitializeComponent();
    }

    // =======================
    // Title
    // =======================
    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(DashboardCard),
            string.Empty);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    // =======================
    // Value
    // =======================
    public static readonly BindableProperty ValueProperty =
        BindableProperty.Create(
            nameof(Value),
            typeof(string),
            typeof(DashboardCard),
            string.Empty);

    public string Value
    {
        get => (string)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    // =======================
    // Subtitle
    // =======================
    public static readonly BindableProperty SubtitleProperty =
        BindableProperty.Create(
            nameof(Subtitle),
            typeof(string),
            typeof(DashboardCard),
            string.Empty);

    public string Subtitle
    {
        get => (string)GetValue(SubtitleProperty);
        set => SetValue(SubtitleProperty, value);
    }

    // =======================
    // Icon
    // =======================
    public static readonly BindableProperty IconProperty =
        BindableProperty.Create(
            nameof(Icon),
            typeof(string),
            typeof(DashboardCard),
            string.Empty);

    public string Icon
    {
        get => (string)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    // =======================
    // Card Color
    // =======================
    public static readonly BindableProperty CardColorProperty =
        BindableProperty.Create(
            nameof(CardColor),
            typeof(Color),
            typeof(DashboardCard),
            Colors.Blue);

    public Color CardColor
    {
        get => (Color)GetValue(CardColorProperty);
        set => SetValue(CardColorProperty, value);
    }
}