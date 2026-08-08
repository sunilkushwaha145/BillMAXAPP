using BillMaxAPP.Models;
using BillMaxAPP.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Windows.Input;

namespace BillMaxAPP.ViewModels
{
    public class StoreDashboardViewModel: INotifyPropertyChanged
    {
        private readonly IDashboardService _dashboardService;

        private StoreDashboard? _dashboard;

        public StoreDashboard? Dashboard
        {
            get => _dashboard;
            set
            {
                _dashboard = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoadDashboardCommand { get; }

        public ObservableCollection<SalesTrend> CurrentTrend { get; }
            = new();

        public StoreDashboardViewModel(IDashboardService dashboardService, IAuthService authService)
        {
            _dashboardService = dashboardService;

            LoadDashboardCommand = new Command(async () => await LoadDashboardAsync());
        }

        public async Task LoadDashboardAsync()
        {
            try
            {
                StoreDashboard result = new StoreDashboard();
                result = await _dashboardService.GetStoreDashboardAsync();

                if (result != null)
                {
                    Dashboard = result;
                }
                else
                {
                    await Application.Current!.Windows[0].Page!
                    .DisplayAlert("Error", "Error in loading data", "OK");
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

    }
}
