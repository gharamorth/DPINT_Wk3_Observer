using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPINT_Wk3_Observer.Model
{
    public class Aankomsthal :IObserver<Baggageband>
    {
        public ObservableCollection<Vlucht> WachtendeVluchten { get; private set; }
        public List<Baggageband> Baggagebanden { get; private set; }

        public Aankomsthal()
        {
            WachtendeVluchten = new ObservableCollection<Vlucht>();
            Baggagebanden = new List<Baggageband>();

            Baggageband band1 = new Baggageband("Band 1", 30);
            Baggageband band2 = new Baggageband("Band 1", 60);
            Baggageband band3 = new Baggageband("Band 1", 90);
            band1.Subscribe(this);
            band2.Subscribe(this);
            band3.Subscribe(this);

            Baggagebanden.Add(band1);
            Baggagebanden.Add(band2);
            Baggagebanden.Add(band3);
            Baggagebanden.ForEach(bb => OnNext(bb));
        }

        public void NieuweInkomendeVlucht(string vertrokkenVanuit, int aantalKoffers)
        {
            //problem nr 1
            Baggageband legeband = Baggagebanden.FirstOrDefault(bb => bb.AantalKoffers == 0);

            if(legeband != null)
            {
                legeband.HandelNieuweVluchtAf(new Vlucht(vertrokkenVanuit, aantalKoffers));
            }
            else
            {
                WachtendeVluchten.Add(new Vlucht(vertrokkenVanuit, aantalKoffers));
            }
        }

        public void OnError(Exception error)
        {
            //should never occur
            throw new NotImplementedException();
        }

        public void OnCompleted()
        {
            //will not be used
            throw new NotImplementedException();
        }

        //what to do with this function?
        //in the MS docs it checks whether the bags gone.
        //it searches for the flights in the list but that's not really possible with this code as the flights have no UID
        public void OnNext(Baggageband value)
        {
            //problem nr 2
            if (value.AantalKoffers == 0 && WachtendeVluchten.Any())
            {
                Baggageband empty = value;
                Vlucht next = WachtendeVluchten.FirstOrDefault();
                WachtendeVluchten.RemoveAt(0);
                empty.HandelNieuweVluchtAf(next);
            }
        }

    }
}
