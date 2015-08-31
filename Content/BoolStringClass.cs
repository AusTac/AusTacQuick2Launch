using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AusTacQuick2Launch.Pages
{
    public class BoolStringClass : INotifyPropertyChanged
    {
        public string TheText { get; set; }
        public string TheUrl { get; set; }

        //Provide change-notification for IsSelected
        private bool _fIsSelected = false;
        public bool IsSelected
        {
            get { return _fIsSelected; }
            set
            {
                _fIsSelected = value;
                this.OnPropertyChanged("IsSelected");

            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string strPropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(strPropertyName));
        }

        #endregion
    }
}
