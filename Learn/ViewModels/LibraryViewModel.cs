﻿using Learn.Items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Learn.ViewModels
{
    class LibraryViewModel : INotifyPropertyChanged
    {
        private string bookTitle;
        public ObservableCollection<BookItem> Books { get; set; }

        public string BookTitle
        {
            get
            {
                return bookTitle;
            }

            set
            {
                bookTitle = value;
                OnPropertyChanged();
            }
        }

        public LibraryViewModel()
        {
            Books = new ObservableCollection<BookItem>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
