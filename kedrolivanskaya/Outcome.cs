using System;
using System.Runtime.Serialization;
using GalaSoft.MvvmLight;

namespace kedrolivanskaya
{
    [DataContract(Name = "Outcome",Namespace = "kedrolivanskaya")]
    public class Outcome : ObservableObject

    {
        private string _name;
        private double _price;
        private DateTime _date;
        [DataMember()]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(()=>Name);
            }
        }
        public Outcome() { }
        [DataMember()]
        public double Price
        {
            get { return _price; }
            set
            {
                _price = value;
                RaisePropertyChanged(() => Price);
            }
        }
        [DataMember()]
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                RaisePropertyChanged(() => Date);
            }
        }

        public Outcome(string name, double price, DateTime date)
    {
        Name = name;
        Price = price;
        Date = date;
    }
    }
}
