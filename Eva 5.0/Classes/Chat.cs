using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva_5._0
{
    public class Chat : INotifyPropertyChanged
    {
        private string _chatTitle { get; set; }
        
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

        public Chat(long key, string chatTitle)
        {
            _key = key;
            _chatTitle = chatTitle;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
