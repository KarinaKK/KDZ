using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using GalaSoft.MvvmLight;

namespace kedrolivanskaya
{
    [DataContract(Name = "User",Namespace = "kedrolivanskaya")]
    public class User : ViewModelBase
    {
        private ObservableCollection<Income> _incomes;
        private ObservableCollection<Outcome> _outcomes;        

        [DataMember()]
        public string Name { get; set; }
        [DataMember()]
        public string Surname { get; set; }
        [DataMember()]
        public DateTime BirthDate { get; set; }
        [DataMember()]
        public string Password { get; set; }

        [DataMember()]
        public ObservableCollection<Income> Incomes
        {
            get { return _incomes; }
            set
            {
                _incomes = value;
                RaisePropertyChanged(() => Incomes);                
            }
        }      
        [DataMember()]
        public ObservableCollection<Outcome> Outcomes
        {
            get { return _outcomes; }
            set
            {
                _outcomes = value;
                RaisePropertyChanged(() => Outcomes);                               
            }
        }       

        public User()
        {
            Incomes = new ObservableCollection<Income>();
            Outcomes = new ObservableCollection<Outcome>();
        }
        public User(string name, string surname, DateTime birthDate, String password)
        {            
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
            Password = password;
            Incomes = new ObservableCollection<Income>();
            Outcomes=new ObservableCollection<Outcome>();
        }       
    }
}
