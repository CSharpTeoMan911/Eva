using System.ComponentModel;
using System.Windows;

namespace Eva_5._0
{
    public class Message:INotifyPropertyChanged
    {
        public enum MessageType
        {
            User,
            Assistant
        }

        private MessageType _Type;
        private string _message;
        public int _column = 0;
        public Style _style { get; set; } = Application.Current.FindResource("ConversationUserBubble") as Style;

        public MessageType Type
        {
            get { return _Type; }
            set
            {
                if (_Type != value)
                {
                    _Type = value;
                    OnPropertyChanged(nameof(Type));
                }
            }
        }
        public string message
        {
            get { return _message; }
            set
            {
                if (_message != value)
                {
                    _message = value;
                    OnPropertyChanged(nameof(message));
                }
            }
        }

        public int column
        {
            get { return _column; }
            set
            {
                if (_column != value)
                {
                    _column = value;
                    OnPropertyChanged(nameof(column));
                }
            }
        }

        public Style style
        {
            get { return _style; }
            set
            {
                if (_style != value)
                {
                    _style = value;
                    OnPropertyChanged(nameof(style));
                }
            }
        }


        public Message(MessageType type, string message)
        {
            _Type = type;
            _message = message;

            if (type == MessageType.User)
            {
                _column = 0;
                _style = Application.Current.FindResource("ConversationUserBubble") as Style;
            }
            else
            {
                _column = 1;
                _style = Application.Current.FindResource("ConversationAgentBubble") as Style;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
