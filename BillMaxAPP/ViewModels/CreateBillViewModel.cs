using BillMaxAPP.Models;
using BillMaxAPP.Models.DTOs;
using BillMaxAPP.Services.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows.Input;
using System.Xml.Linq;

namespace BillMaxAPP.ViewModels
{
    public class CreateBillViewModel : INotifyPropertyChanged
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public ObservableCollection<CategoryOption> Categories { get; } = new();
        public ObservableCollection<Product> Products { get; } = new();
        public ObservableCollection<CartItem> CartItems { get; } = new();

        private CategoryOption? _selectedCategory;
        public CategoryOption? SelectedCategory
        {
            get => _selectedCategory;
            set { _selectedCategory = value; OnPropertyChanged(); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set { _isBusy = value; OnPropertyChanged(); }
        }

        private bool _isCartSheetVisible;
        public bool IsCartSheetVisible
        {
            get => _isCartSheetVisible;
            set { _isCartSheetVisible = value; OnPropertyChanged(); }
        }

        private string? _emptyStateMessage;
        public string? EmptyStateMessage
        {
            get => _emptyStateMessage;
            set { _emptyStateMessage = value; OnPropertyChanged(); }
        }

        // ---------------- Cart totals ----------------

        public int CartCount => CartItems.Sum(c => c.Qty);

        public decimal SubTotal => CartItems.Sum(c => c.Price);

        // Split GST 50/50 into CGST/SGST per line, using each line's own GST%.
        public decimal CGST => CartItems.Sum(c => (c.Price * c.GSTPercentage / 100m) / 2m);
        public decimal SGST => CGST;

        public decimal Discount { get; set; } = 0; // hook up a discount field/command later if needed

        public decimal GrandTotal => SubTotal + CGST + SGST - Discount;

        public bool IsCartVisible => CartItems.Count > 0;

        // ---------------- Commands ----------------

        public ICommand LoadCategoriesCommand { get; }
        public ICommand SelectCategoryCommand { get; }
        public ICommand AddToCartCommand { get; }
        public ICommand IncreaseQtyCommand { get; }
        public ICommand DecreaseQtyCommand { get; }
        public ICommand RemoveFromCartCommand { get; }
        public ICommand OpenCartCommand { get; }
        public ICommand CloseCartCommand { get; }
        public ICommand GenerateBillCommand { get; }

        public CreateBillViewModel(
            ICategoryService categoryService,
            IProductService productService,
            IBillService billService)
        {
            _categoryService = categoryService;
            _productService = productService;

            LoadCategoriesCommand = new Command(async () => await LoadCategoriesAsync());
            SelectCategoryCommand = new Command<CategoryOption>(async c => await SelectCategoryAsync(c));
            AddToCartCommand = new Command<int>(async c => await AddToCart(c));
            IncreaseQtyCommand = new Command<CartItem>(IncreaseQty);
            DecreaseQtyCommand = new Command<CartItem>(DecreaseQty);
            RemoveFromCartCommand = new Command<CartItem>(RemoveFromCart);
            OpenCartCommand = new Command(() => IsCartSheetVisible = true);
            CloseCartCommand = new Command(() => IsCartSheetVisible = false);
            GenerateBillCommand = new Command(async () => await GenerateBillAsync(), () => !IsBusy && CartItems.Count > 0);

            CartItems.CollectionChanged += (_, _) => RaiseCartTotalsChanged();
        }

        public async Task InitializeAsync()
        {
            if (Categories.Count == 0)
                await LoadCategoriesAsync();
        }

        private async Task LoadCategoriesAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                EmptyStateMessage = null;

                ResJsonOutput result = await _categoryService.GetCategoriesAsync();

                Categories.Clear();

                if (!result.Status.IsSuccess)
                {
                    EmptyStateMessage = "No categories found.";
                    return;
                }
                else
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var response = ((JsonElement)result.Data).Deserialize<List<CategoryOption>>(options);

                    if (response == null || response.Count == 0)
                    {
                        EmptyStateMessage = "No products found in this category.";
                        return;
                    }
                    foreach (var c in response)
                        Categories.Add(c);
                }
                await SelectCategoryAsync(Categories.FirstOrDefault());
            }
            catch (Exception ex)
            {
                EmptyStateMessage = "Could not load categories. Please check your connection.";
                await SafeShowAlert("Error", FriendlyError(ex));
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task SelectCategoryAsync(CategoryOption? category)
        {
            if (category == null) return;

            foreach (var c in Categories)
                c.Selected = c.Value == category.Value;

            SelectedCategory = category;

            try
            {
                IsBusy = true;
                EmptyStateMessage = null;
                Products.Clear();

                var result = await _productService.GetProductsByCategoryAsync(
     new Dictionary<string, string>
     {
        { "catId", category.Value ?? string.Empty }
     });

                if (result.Status.IsSuccess)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var response = ((JsonElement)result.Data).Deserialize<List<Product>>(options);

                    if (response == null || response.Count == 0)
                    {
                        EmptyStateMessage = "No products found in this category.";
                        return;
                    }

                    foreach (var p in response)
                        Products.Add(p);
                }
            }
            catch (Exception ex)
            {
                EmptyStateMessage = "Could not load products. Please check your connection.";
                await SafeShowAlert("Error", FriendlyError(ex));
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task AddToCart(int ProductId)
        {
            if (ProductId <= 0) return;

            var existing = CartItems.FirstOrDefault(c => c.ProductId == ProductId);

            if (existing != null)
            {
                existing.Qty++;
            }
            else
            {
                ResJsonOutput result = await _productService.GetProductByID(ProductId);

                if (result.Status.IsSuccess)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var product = ((JsonElement)result.Data).Deserialize<Product>(options);

                    CartItems.Add(new CartItem
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        //ProductImage = product.ProductImage,
                        Price = (decimal)product.Price,
                        GSTPercentage = product.IsGSTApplicable ? product.GSTPercentage : 0,
                        Qty = 1
                    });
                }

                RaiseCartTotalsChanged();
            }
        }

        private void IncreaseQty(CartItem? item)
        {
            if (item == null) return;
            item.Qty++;
            RaiseCartTotalsChanged();
        }

        private void DecreaseQty(CartItem? item)
        {
            if (item == null) return;

            if (item.Qty <= 1)
            {
                RemoveFromCart(item);
                return;
            }

            item.Qty--;
            RaiseCartTotalsChanged();
        }

        private void RemoveFromCart(CartItem? item)
        {
            if (item == null) return;
            CartItems.Remove(item);
            RaiseCartTotalsChanged();

            if (CartItems.Count == 0)
                IsCartSheetVisible = false;
        }

        private async Task GenerateBillAsync()
        {
            if (IsBusy || CartItems.Count == 0) return;

            try
            {
                IsBusy = true;

                var request = new Invoices
                {
                    SubTotal = SubTotal,
                    CGST = CGST,
                    SGST = SGST,
                    Discount = Discount,
                    GrandTotal = GrandTotal,
                    PayType = "Cash",
                    PayStatus = true
                    //Iems = CartItems.Select(c => new CartItem
                    //{
                    //    ProductId = c.ProductId,
                    //    Quantity = c.Quantity,q
                    //    UnitPrice = c.UnitPrice,
                    //    SubTotal = c.ItemTotal
                    //}).ToList()
                };

                //var response = await _billService.CreateBillAsync(request);

                //if (response != null)
                //{
                //    await SafeShowAlert("Success", $"Bill {(string.IsNullOrWhiteSpace(response.InvoiceNo) ? "" : response.InvoiceNo)} generated successfully.");

                //    CartItems.Clear();
                //    IsCartSheetVisible = false;
                //    RaiseCartTotalsChanged();

                //    if (SelectedCategory != null)
                //        await SelectCategoryAsync(SelectedCategory);
                //}
                //else
                //{
                //    await SafeShowAlert("Error", "Could not generate the bill. Please try again.");
                //}
            }
            catch (Exception ex)
            {
                await SafeShowAlert("Error", FriendlyError(ex));
            }
            finally
            {
                IsBusy = false;
            }
        }

        private static async Task SafeShowAlert(string title, string message)
        {
            var page = Application.Current?.Windows.Count > 0
                ? Application.Current.Windows[0].Page
                : null;

            if (page != null)
                await page.DisplayAlert(title, message, "OK");
        }

        private static string FriendlyError(Exception ex)
        {
            var msg = ex.Message ?? string.Empty;

            if (msg.Contains("401"))
                return "Your session has expired. Please log in again.";
            if (msg.Contains("404"))
                return "The requested data could not be found.";

            return "Something went wrong. Please try again.";
        }

        private void RaiseCartTotalsChanged()
        {
            OnPropertyChanged(nameof(CartCount));
            OnPropertyChanged(nameof(SubTotal));
            OnPropertyChanged(nameof(CGST));
            OnPropertyChanged(nameof(SGST));
            OnPropertyChanged(nameof(GrandTotal));
            OnPropertyChanged(nameof(IsCartVisible));
            ((Command)GenerateBillCommand).ChangeCanExecute();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}