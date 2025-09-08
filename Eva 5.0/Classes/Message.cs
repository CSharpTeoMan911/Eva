using System.ComponentModel;
using System.Windows;

namespace Eva_5._0
{
    public class Message : INotifyPropertyChanged
    {
        public enum MessageType
        {
            User,
            Assistant
        }

        private MessageType _type;
        private string _message;
        private int _column = 0;
        private Style _style { get; set; } = Application.Current.FindResource("ConversationUserBubble") as Style;

        private Visibility _pending = Visibility.Collapsed;
        private Visibility _chat = Visibility.Visible;

        public MessageType type
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


        public Visibility pending
        {
            get { return _pending; }
            set
            {
                if (_pending != value)
                {
                    _pending = value;
                    OnPropertyChanged(nameof(pending));
                }
            }
        }

        public Visibility chat
        {
            get { return _chat; }
            set
            {
                if (_chat != value)
                {
                    _chat = value;
                    OnPropertyChanged(nameof(chat));
                }
            }
        }


        public Message(MessageType type, string message = null)
        {
            _type = type;
            _message = message;

            if (type == MessageType.User)
            {
                _column = 0;
                _style = Application.Current.FindResource("ConversationUserBubble") as Style;
            }
            else if (type == MessageType.Assistant)
            {
                _column = 1;
                _style = Application.Current.FindResource("ConversationAgentBubble") as Style;
            }
        }

        public void UpdateMessage(string newMessage, bool isContentAppended = false)
        {
            if (_message != newMessage)
            {
                switch (isContentAppended)
                {
                    case true:
                        _message += newMessage;
                        break;
                    case false:
                        _message = newMessage;
                        break;
                }

                OnPropertyChanged(nameof(message));
            }
        }

        public void PendingStarted()
        {
            _pending = Visibility.Visible;
            _chat = Visibility.Collapsed;

            OnPropertyChanged(nameof(pending));
            OnPropertyChanged(nameof(chat));
        }

        public void PendingFinished()
        {
            _pending = Visibility.Collapsed;
            _chat = Visibility.Visible;

            OnPropertyChanged(nameof(pending));
            OnPropertyChanged(nameof(chat));
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
