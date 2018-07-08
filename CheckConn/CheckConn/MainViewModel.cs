using CheckConn.Annotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Plugin.Connectivity;

namespace CheckConn
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private string _conn;
        public string Conn
        {
            get => _conn;
            set
            {
                _conn = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            CheckWifiOnStart();
            CheckWifiContinuously();
        }

        public void CheckWifiOnStart()
        {
            Conn = CrossConnectivity.Current.IsConnected ? "online.png" : "offline.png";
        }

        public void CheckWifiContinuously()
        {
            CrossConnectivity.Current.ConnectivityChanged += (sender, args) =>
            {
                Conn = args.IsConnected ? "online.png" : "offline.png";
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}