using BillMaxAPP.Models;
using BillMaxAPP.Services.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows.Input;

namespace BillMaxAPP.ViewModels;

public class AdminDashboardViewModel : INotifyPropertyChanged
{
    private readonly IDashboardService _dashboardService;

    private AdminDashboard? _dashboard;

    public AdminDashboard? Dashboard
    {
        get => _dashboard;
        set
        {
            _dashboard = value;
            OnPropertyChanged();
        }
    }

    public ICommand LoadDashboardCommand { get; }
    public ICommand WeekCommand { get; }

    public ICommand MonthCommand { get; }

    public ICommand YearCommand { get; }

    public List<SalesTrend> WeekTrend { get; set; }

    public List<SalesTrend> MonthTrend { get; set; }

    public List<SalesTrend> YearTrend { get; set; }

    public ObservableCollection<SalesTrend> CurrentTrend { get; }
        = new();

    public AdminDashboardViewModel(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;

        LoadDashboardCommand = new Command(async () => await LoadDashboardAsync());
        WeekCommand = new Command(LoadWeek);

        MonthCommand = new Command(LoadMonth);

        YearCommand = new Command(LoadYear);
    }

    public async Task LoadDashboardAsync()
    {
        try
        {
            ResJsonOutput result = new ResJsonOutput();
            result = await _dashboardService.GetAdminDashboardAsync();

            if(result!=null && result.Status.IsSuccess)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                Dashboard = ((JsonElement)result.Data).Deserialize<AdminDashboard>(options);
                WeekTrend = Dashboard.SalesTrend;
                MonthTrend = Dashboard.MonthTrend;
                YearTrend = Dashboard.YearTrend;
                LoadWeek();
            }
            else
            {
                await Application.Current!.Windows[0].Page!
                .DisplayAlert("Error", result.Status.Message, "OK");
            }
        }
        catch (Exception ex)
        {
            await Application.Current!.Windows[0].Page!
                .DisplayAlert("Error", ex.Message, "OK");
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void LoadWeek()
    {
        CurrentTrend.Clear();

        foreach (var item in WeekTrend)
            CurrentTrend.Add(item);
    }

    private void LoadMonth()
    {
        CurrentTrend.Clear();

        foreach (var item in MonthTrend)
            CurrentTrend.Add(item);
    }

    private void LoadYear()
    {
        CurrentTrend.Clear();

        foreach (var item in YearTrend)
            CurrentTrend.Add(item);
    }
}