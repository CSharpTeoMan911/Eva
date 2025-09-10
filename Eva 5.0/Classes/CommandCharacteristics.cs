using System.ComponentModel;

namespace Eva_5._0
{
    public class CommandCharacteristics : INotifyPropertyChanged
    {
        private System.Windows.Visibility _type_panel_visibility = System.Windows.Visibility.Visible;
        public System.Windows.Visibility type_panel_visibility
        {
            get { return _type_panel_visibility; }
            set
            {
                if (_type_panel_visibility != value)
                {
                    _type_panel_visibility = value;
                    OnPropertyChanged(nameof(type_panel_visibility));
                }
            }
        }

        public enum Type
        {
            PRC,
            CMD,
            URI
        }

        private Type _type = Type.PRC;
        public Type type
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    OnPropertyChanged(nameof(type));
                }
            }
        }

        private string _command;
        public string command
        {
            get { return _command; }
            set
            {
                if (_command != value)
                {
                    _command = value;
                    OnPropertyChanged(nameof(command));
                }
            }
        }

        private string _content;
        public string content
        {
            get { return _content; }
            set
            {
                if (_content != value)
                {
                    _content = value;
                    OnPropertyChanged(nameof(content));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CommandCharacteristics() { }
        public CommandCharacteristics(bool visible) => _type_panel_visibility = visible ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
    }
}
