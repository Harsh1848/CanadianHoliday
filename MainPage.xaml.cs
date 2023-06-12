using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace CanadianHoliday
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<Holiday> _holidays;
        private Holidayservice _holidayService;

        public MainPage()
        {
            InitializeComponent();

            _holidays = new ObservableCollection<Holiday>();
            _holidayService = new Holidayservice();

            BindingContext = this;
        }

        private async Task LoadHolidaysAsync(int year, string province)
        {
            var holidays = await _holidayService.GetHolidaysAsync(year, province);
            
            foreach (var holiday in holidays)
            {
                _holidays.Add(holiday);
            }
        }

        private async void FilterButton_Clicked(object sender, EventArgs e)
        {
            var selectedYear = int.Parse(YearPicker.SelectedItem.ToString());
            var selectedProvince = ProvincePicker.SelectedItem?.ToString();

            await LoadHolidaysAsync(selectedYear, selectedProvince);
        }

        private async void YearPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedYear = int.Parse(YearPicker.SelectedItem.ToString());
            var selectedProvince = ProvincePicker.SelectedItem?.ToString();

            await LoadHolidaysAsync(selectedYear, selectedProvince);
        }

        private async void ProvincePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedYear = int.Parse(YearPicker.SelectedItem.ToString());
            var selectedProvince = ProvincePicker.SelectedItem?.ToString();

            await LoadHolidaysAsync(selectedYear, selectedProvince);
        }

        public ObservableCollection<Holiday> Holidays
        {
            get { return _holidays; }
            set
            {
                if (_holidays != value)
                {
                    _holidays = value;
                    OnPropertyChanged(nameof(Holidays));
                }
            }
        }
    }
}
