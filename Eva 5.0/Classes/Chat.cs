using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Eva_5._0
{
    public class Chat : INotifyPropertyChanged
    {
        private string _chatTitle { get; set; }
        private Style _style {  get; set; }

        public enum Selection
        {
            Current,
            Default
        }
        
        public string chatTitle
        {
            get { return _chatTitle; }
            set
            {
                if (_chatTitle != value)
                {
                    _chatTitle = value;
                    OnPropertyChanged(nameof(chatTitle));
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

        private long _key { get; set; }

        public long key
        {
            get { return _key; }
            set
            {
                if (_key != value)
                {
                    _key = value;
                    OnPropertyChanged(nameof(key));
                }
            }
        }

        public Chat(long key, string chatTitle, Selection selection)
        {
            _key = key;
            _chatTitle = chatTitle;

            switch (selection)
            {
                case Selection.Current:
                    _style = Application.Current.FindResource("CurrentChatHistoryItem") as Style;
                    break;
                case Selection.Default:
                    _style = Application.Current.FindResource("DefaultChatHistoryItem") as Style;
                    break;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
