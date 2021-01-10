using DPINT_Wk3_Observer.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPINT_Wk3_Observer.ViewModel
{
    public class BaggagebandViewModel : ViewModelBase, IObserver<Baggageband>
    {
        private string _vluchtVertrokkenVanuit;
        public string VluchtVertrokkenVanuit
        {
            get { return _vluchtVertrokkenVanuit; }
            set { _vluchtVertrokkenVanuit = value; RaisePropertyChanged("VluchtVertrokkenVanuit"); }
        }

        private int _aantalKoffers;
        public int AantalKoffers
        {
            get { return _aantalKoffers; }
            set { _aantalKoffers = value; RaisePropertyChanged("AantalKoffers"); }
        }

        private string _naam;
        public string Naam
        {
            get { return _naam; }
            set { _naam = value; RaisePropertyChanged("Naam"); }
        }

        public BaggagebandViewModel(Baggageband band)
        {
            band.Subscribe(this);
        }

        public void OnNext(Baggageband value)
        {
            //_aantalKoffers = value.AantalKoffers;
            //_vluchtVertrokkenVanuit = value.VluchtVertrokkenVanuit;

            VluchtVertrokkenVanuit = value.VluchtVertrokkenVanuit;
            AantalKoffers = value.AantalKoffers;
            Naam = value.Naam;
        }

        public void OnError(Exception error)
        {
            //not used
            throw new NotImplementedException();
        }

        public void OnCompleted()
        {
            //not used
            throw new NotImplementedException();
        }

    }
}
