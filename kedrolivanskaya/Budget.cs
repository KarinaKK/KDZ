using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using static kedrolivanskaya.Security;

namespace kedrolivanskaya
{
    [DataContract(Name = "Budget",Namespace = "kedrolivanskaya")]
    public class Budget:ObservableObject
    {
        private ObservableCollection<User> _family;
        private double _sumIncome;
        private double _sumOutcome;
        private double _total;

        
        [DataMember()]
        public ObservableCollection<User> Family
        {
            get { return _family; }
            set
            {
                _family = value;
                RaisePropertyChanged(() => Family);               
            }
        }

        public void calc_incomes()
        {
            SumIncome = 0;
            foreach (var user in Family)
            {
                SumIncome += user.Incomes.Sum(x=>x.Price);
            }
        }

        public void calc_outcomes()
        {
            SumOutcome = 0;
            foreach (var user in Family)
            {
                SumOutcome += user.Outcomes.Sum(x => x.Price);
            }

        }

        public void calc_total()
        {
            Total = _sumIncome - _sumOutcome;
        }        
        [IgnoreDataMember()]
        public double Total
        {
            get { return _total; }
            set
            {
                _total = value;
                RaisePropertyChanged(() => Total);
            }
        }
        [IgnoreDataMember()]
        public double SumIncome
        {
            get { return _sumIncome; }
            set
            {
                _sumIncome = value; 
                RaisePropertyChanged(()=>SumIncome);
            }
        }

        [IgnoreDataMember()]
        public double SumOutcome
        {
            get { return _sumOutcome; }
            set
            {
                _sumOutcome = value;
                RaisePropertyChanged(() => SumOutcome);
            }
        }
        

        private readonly ObservableCollection<User> _initialValue;

        public bool check_Updates()
        {
            if (_initialValue == null)
                return false;
            SHA1Managed sHash = new SHA1Managed();            
            
            byte[] hashValue = sHash.ComputeHash(new UnicodeEncoding().GetBytes(obs_to_stirng(_initialValue)));
            byte[] hashValue1 = sHash.ComputeHash(new UnicodeEncoding().GetBytes(obs_to_stirng(Family)));

            if (!Buffer_Equals(hashValue1, hashValue))
                return false;            
            return true;

        }
        
        private string obs_to_stirng(ObservableCollection<User> users)
        {
            string res = "";            
            if (users!=null)
                foreach (var user in users)
                {
                    res += user.Name;
                    res += user.Surname;                    
                    foreach (var income in user.Incomes.ToList())
                    {
                        res += income.Name;
                        res += income.Price;
                        res += income.Date;
                    }
                    
                    foreach (var outcome in user.Outcomes)
                    {
                        res += outcome.Name;
                        res += outcome.Price;
                        res += outcome.Date;
                    }
                    
                }
            return res;
        }
        public ObservableCollection<User> LoadData()
        {
            
            Family=new ObservableCollection<User>();
            try
            {
                string myDocPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                using (StreamReader inputFile = new StreamReader(myDocPath + @"\Budget.txt"))
                {
                    string resLine = inputFile.ReadLine();
                    if (resLine.Length > 0)
                    {    
                        Family= JsonConvert.DeserializeObject<ObservableCollection<User>>(resLine);
                        
                        return JsonConvert.DeserializeObject<ObservableCollection<User>>(resLine);
                        
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Check path to file");
            }
            return null;
            //Family = new ObservableCollection<User>
            //{
            //    new User
            //    {
            //        BirthDate = DateTime.Today,
            //        Incomes = new ObservableCollection<Income>
            //        {
            //            new Income("test", 300),
            //            new Income("test2", 540),
            //        },
            //        Name = "testUser",
            //        Outcomes = new ObservableCollection<Outcome>
            //        {
            //            new Outcome("test", 300, DateTime.Today),
            //            new Outcome("test2", 540, DateTime.Today),
            //        },
            //        Password = "12345",
            //        Surname = "TestUserSurn"
            //    },
            //    new User
            //    {
            //        BirthDate = DateTime.Today,
            //        Incomes = new ObservableCollection<Income>
            //        {
            //            new Income("dasdt", 300),
            //            new Income("tedfv2", 540),
            //            new Income("tedfv2", 540),
            //            new Income("tedfv2", 540),
            //        },
            //        Name = "tvfdstUfddfr",
            //        Outcomes = new ObservableCollection<Outcome>
            //        {
            //            new Outcome("tevfdvst", 300, DateTime.Today),
            //            new Outcome("tcascest2", 540, DateTime.Today),
            //        },
            //        Password = "12345",
            //        Surname = "TestasdasdUserSurn"
            //    }
            //};
        }

        public void Save_Data()
        {
            string jsonBudget = JsonConvert.SerializeObject(Family);
            string myDocPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            using (StreamWriter outputFile = new StreamWriter(myDocPath + @"\Budget.txt"))
            {
                outputFile.WriteLine(jsonBudget);
            }
        }
        //private ICommand _delete;        

        //public ICommand Delete
        //{
        //    get
        //    {
        //        return _delete ?? (_delete = new RelayCommand<object>(
        //                   (income) =>
        //                   {
        //                       foreach (User t in Family)
        //                       {
        //                           if (t.Incomes.Remove(income as Income))
        //                               break;
        //                       }
        //                   }
        //            ));
        //    }
        //    set { _delete = value; }
        //}



        public Budget()
        {                        
            _initialValue = LoadData();
            if(_initialValue==null)
                _initialValue = new ObservableCollection<User>();            
        }
        public Budget(ObservableCollection<User> family)
        {
            Family = family;
        }
    }
}
