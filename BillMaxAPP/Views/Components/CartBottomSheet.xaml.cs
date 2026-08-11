using BillMaxAPP.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace BillMaxAPP.Views.Components;

public partial class CartBottomSheet : ContentView
{
    public CartBottomSheet()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty CartItemsProperty =
        BindableProperty.Create(
            nameof(CartItems),
            typeof(IEnumerable<CartItem>),
            typeof(CartBottomSheet),
            default(IEnumerable<CartItem>));

    public IEnumerable<CartItem> CartItems
    {
        get => (IEnumerable<CartItem>)GetValue(CartItemsProperty);
        set => SetValue(CartItemsProperty, value);
    }

    public static readonly BindableProperty SubTotalProperty =
        BindableProperty.Create(nameof(SubTotal), typeof(decimal), typeof(CartBottomSheet), 0m);
    public decimal SubTotal { get => (decimal)GetValue(SubTotalProperty); set => SetValue(SubTotalProperty, value); }

    public static readonly BindableProperty CGSTProperty =
        BindableProperty.Create(nameof(CGST), typeof(decimal), typeof(CartBottomSheet), 0m);
    public decimal CGST { get => (decimal)GetValue(CGSTProperty); set => SetValue(CGSTProperty, value); }

    public static readonly BindableProperty SGSTProperty =
        BindableProperty.Create(nameof(SGST), typeof(decimal), typeof(CartBottomSheet), 0m);
    public decimal SGST { get => (decimal)GetValue(SGSTProperty); set => SetValue(SGSTProperty, value); }

    public static readonly BindableProperty GrandTotalProperty =
        BindableProperty.Create(nameof(GrandTotal), typeof(decimal), typeof(CartBottomSheet), 0m);
    public decimal GrandTotal { get => (decimal)GetValue(GrandTotalProperty); set => SetValue(GrandTotalProperty, value); }

    public static readonly BindableProperty IncreaseCommandProperty =
        BindableProperty.Create(nameof(IncreaseCommand), typeof(ICommand), typeof(CartBottomSheet));
    public ICommand IncreaseCommand { get => (ICommand)GetValue(IncreaseCommandProperty); set => SetValue(IncreaseCommandProperty, value); }

    public static readonly BindableProperty DecreaseCommandProperty =
        BindableProperty.Create(nameof(DecreaseCommand), typeof(ICommand), typeof(CartBottomSheet));
    public ICommand DecreaseCommand { get => (ICommand)GetValue(DecreaseCommandProperty); set => SetValue(DecreaseCommandProperty, value); }

    public static readonly BindableProperty GenerateBillCommandProperty =
        BindableProperty.Create(nameof(GenerateBillCommand), typeof(ICommand), typeof(CartBottomSheet));
    public ICommand GenerateBillCommand { get => (ICommand)GetValue(GenerateBillCommandProperty); set => SetValue(GenerateBillCommandProperty, value); }

    public static readonly BindableProperty CloseCommandProperty =
        BindableProperty.Create(nameof(CloseCommand), typeof(ICommand), typeof(CartBottomSheet));
    public ICommand CloseCommand { get => (ICommand)GetValue(CloseCommandProperty); set => SetValue(CloseCommandProperty, value); }

    public static readonly BindableProperty CustomerNameProperty =
    BindableProperty.Create(
        nameof(CustomerName),
        typeof(string),
        typeof(CartBottomSheet),
        null,
        defaultBindingMode: BindingMode.TwoWay);

    public string? CustomerName
    {
        get => (string?)GetValue(CustomerNameProperty);
        set => SetValue(CustomerNameProperty, value);
    }


    public static readonly BindableProperty CustomerMobileProperty =
        BindableProperty.Create(
            nameof(CustomerMobile),
            typeof(string),
            typeof(CartBottomSheet),
            null,
            defaultBindingMode: BindingMode.TwoWay);

    public string? CustomerMobile
    {
        get => (string?)GetValue(CustomerMobileProperty);
        set => SetValue(CustomerMobileProperty, value);
    }


    public static readonly BindableProperty PaymentTypeProperty =
        BindableProperty.Create(
            nameof(PaymentType),
            typeof(string),
            typeof(CartBottomSheet),
            "Cash",
            defaultBindingMode: BindingMode.TwoWay);

    public string PaymentType
    {
        get => (string)GetValue(PaymentTypeProperty);
        set => SetValue(PaymentTypeProperty, value);
    }

    public static readonly BindableProperty SelectPaymentCommandProperty =
    BindableProperty.Create(
        nameof(SelectPaymentCommand),
        typeof(ICommand),
        typeof(CartBottomSheet));

    public ICommand SelectPaymentCommand
    {
        get => (ICommand)GetValue(SelectPaymentCommandProperty);
        set => SetValue(SelectPaymentCommandProperty, value);
    }
}