using System;
using System.Runtime.Serialization;
using GalaSoft.MvvmLight;

namespace kedrolivanskaya
{
    [DataContract(Name = "Income", Namespace = "kedrolivanskaya")]
    public class Income : ObservableObject
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
                RaisePropertyChanged(() => Name);
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
        public Income() { }
        public Income(string name, double price,DateTime date)
        {
            Date = date;
            Name = name;
            Price = price;
        }
        //private Dictionary<User, Incomes> _incomeses;
        //private Dictionary<User,>
    }
}
