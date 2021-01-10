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
        }

        public void NieuweInkomendeVlucht(string vertrokkenVanuit, int aantalKoffers)
        {
            // TODO: Het proces moet straks automatisch gaan, dus als er lege banden zijn moet de vlucht niet in de wachtrij.
            // Dan moet de vlucht meteen naar die band.

            // Denk bijvoorbeeld aan: Baggageband legeBand = Baggagebanden.FirstOrDefault(b => b.AantalKoffers == 0);

            Baggageband legeband = Baggagebanden.FirstOrDefault(bb=> bb.AantalKoffers == 0);

            if (legeband != null)
            {
                int index = Baggagebanden.IndexOf(legeband);
                //middels index direct het object aanpassen
                Baggagebanden[index].VluchtVertrokkenVanuit = vertrokkenVanuit;
                Baggagebanden[index].AantalKoffers = aantalKoffers;
            }
            else
            {
                // if there are no empty BaggageBanden
                WachtendeVluchten.Add(new Vlucht(vertrokkenVanuit, aantalKoffers));
            }
        }

        public void WachtendeVluchtenNaarBand()
        {
            //while(Baggagebanden.Any(bb => bb.AantalKoffers == 0) && WachtendeVluchten.Any())
            // TODO: Straks krijgen we een update van een baggageband. Dan hoeven we alleen maar te kijken of hij leeg is.
            // Als dat zo is kunnen we vrijwel de hele onderstaande code hergebruiken en hebben we geen while meer nodig.

            if (Baggagebanden.Any(bb => bb.AantalKoffers == 0) && WachtendeVluchten.Any())
            {
                Baggageband legeBand = Baggagebanden.FirstOrDefault(bb => bb.AantalKoffers == 0);
                Vlucht volgendeVlucht = WachtendeVluchten.FirstOrDefault();
                WachtendeVluchten.RemoveAt(0);

                legeBand.HandelNieuweVluchtAf(volgendeVlucht);
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
            WachtendeVluchtenNaarBand();
        }

    }
}
