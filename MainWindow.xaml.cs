using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace StreamDeckConfigurator
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Pad _selectedPad;

        public ObservableCollection<Pad> Pads { get; set; }
        public ObservableCollection<string> Styles { get; } = new ObservableCollection<string> { "Czarny", "Podświetlony", "Gradient" };
        public ObservableCollection<string> Actions { get; } = new ObservableCollection<string> { "Otwórz aplikację", "Start / Stop muzyka", "Screenshot", "Nagrywanie", "Wycziszenie mikrofonu", "Otwórz przeglądarkę" };
        public ObservableCollection<string> Icons { get; } = new ObservableCollection<string> { "🚀", "🎵", "🌐", "🔇", "📸", "🔴" };
        public ObservableCollection<string> Statuses { get; } = new ObservableCollection<string> { "Połączono", "Odłączono" };


        public string ConnectionStatus { get; private set; } = "Połączono";         

        public Pad SelectedPad
        {
            get => _selectedPad;
            set { _selectedPad = value; OnPropertyChanged(); }
        }

        public ICommand SelectPadCommand { get; }
        public ICommand SendToDeckCommand { get; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Pads = new ObservableCollection<Pad>
            {
                new Pad { Name = "Aplikacja", Action = "Otwórz aplikację", Style = "Czarny", Icon = "🚀" },
                new Pad { Name = "Muzyka", Action = "Start / Stop muzyka", Style = "Podświetlony", Icon = "🎵" },
                new Pad { Name = "Przeglądarka", Action = "Otwórz przeglądarkę", Style = "Gradient", Icon = "🌐" },
                new Pad { Name = "Wycisz", Action = "Wycziszenie mikrofonu", Style = "Czarny", Icon = "🔇" },
                new Pad { Name = "Screenshot", Action = "Screenshot", Style = "Podświetlony", Icon = "📸" },
                new Pad { Name = "Nagraj", Action = "Nagrywanie", Style = "Gradient", Icon = "🔴" }
            };

            SelectPadCommand = new RelayCommand(SelectPad, CanSelectPad);
            SendToDeckCommand = new RelayCommand(SendAllConfigurationsToDeck, CanSendConfiguration);

            if (Pads.Count > 0)
            {
                SelectPad(Pads[0]);
            }
        }

        private void SelectPad(object pad)
        {
            if (pad is Pad p)          
                SelectedPad = p;
 
            
        }

        private bool CanSelectPad(object parameter)
        {
            return true;
        }

        private void SendAllConfigurationsToDeck(object parameter)
        {
            //method that in future could send configuration to the hardware
            //for now just sending test message that approves send button working

            var summary = new StringBuilder();
            summary.AppendLine("Przesyłanie konfiguracji:");

            foreach (var pad in Pads)
            {
                summary.AppendLine($"  - Przycisk '{pad.Name}': Akcja='{pad.Action}', Ikona='{pad.Icon}'");
            }

            summary.AppendLine("\nPrzesłano konfigurację przycisków");
            MessageBox.Show(summary.ToString(), "Konfiguracja stream deck");
        }

        private bool CanSendConfiguration(object parameter)
        {
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}