using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StreamDeckConfigurator
{
    public class Pad : INotifyPropertyChanged
    {
        private string _name;
        private string _action;
        private string _style;
        private string _icon;

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public string Action
        {
            get => _action;
            set { _action = value; OnPropertyChanged(); }
        }

        public string Style
        {
            get => _style;
            set { _style = value; OnPropertyChanged(); }
        }

        public string Icon
        {
            get => _icon;
            set { _icon = value; OnPropertyChanged(); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}