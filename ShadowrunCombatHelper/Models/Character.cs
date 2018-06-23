using System;
using System.ComponentModel;

namespace ShadowrunCombatHelper.Models
{
    public class Character : INotifyPropertyChanged
    {
        public enum Status { CONSCIOUS, BLEEDING_OUT, DEAD }

        private static Random gen = new Random();
        private string _affiliation;
        private int _agi;
        private int _bod;
        private int _cha;
        private Guid _charId;
        private int _currentPhysicalDamage;
        private int _currentStunDamage;
        private int _edge;
        private int _initiative;
        private int _intu;
        private int _log;
        private string _name;
        private int _rea;
        private int _str;
        private int _wil;

        public Character()
        {
            _charId = Guid.NewGuid();
        }

        public Character BoundCharacter
        { get { return this; } }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Affiliation
        {
            get { return _affiliation; }
            set { _affiliation = value; }
        }

        public int AGI
        {
            get { return _agi; }
            set { _agi = value; NotifyPropertyChanged("AGI"); }
        }

        public int BOD
        {
            get { return _bod; }
            set
            {
                _bod = value;
                NotifyPropertyChanged("BOD");
                NotifyPropertyChanged("MaxPhysicalHealth");
                NotifyPropertyChanged("MaxOverflowHealth");
                NotifyPropertyChanged("PhysicalLimit");
            }
        }

        public int CHA
        {
            get { return _cha; }
            set
            {
                _cha = value;
                NotifyPropertyChanged("CHA");
                NotifyPropertyChanged("SocialLimit");
            }
        }

        public string CharacterName
        {
            get { return _name; }
            set { _name = value; }
        }

        public Guid CharID
        {
            get { return _charId; }
        }

        public int CurrentDamagePenalty
        {
            get { return (int)Math.Floor((decimal)CurrentPhysicalDamage / 3) + (int)Math.Floor((decimal)CurrentStunDamage / 3); }
        }

        public int CurrentPhysicalDamage
        {
            get { return _currentPhysicalDamage; }
            set
            {
                _currentPhysicalDamage = value;
                NotifyPropertyChanged("CurrentPhysicalDamage");
                NotifyPropertyChanged("CurrentDamagePenalty");
                NotifyPropertyChanged("CharStatus");
            }
        }

        public int CurrentStunDamage
        {
            get { return _currentStunDamage; }
            set
            {
                _currentStunDamage = value;
                NotifyPropertyChanged("CurrentStunDamage");
                NotifyPropertyChanged("CurrentDamagePenalty");
            }
        }

        public int EDGE
        {
            get { return _edge; }
            set { _edge = value; NotifyPropertyChanged("EDGE"); }
        }

        public int Initiative
        {
            get { return _initiative; }
            set { _initiative = value;
                NotifyPropertyChanged("Initiative");
            }
        }

        public int INTU
        {
            get { return _intu; }
            set
            {
                _intu = value; NotifyPropertyChanged("INTU");
                NotifyPropertyChanged("MentalLimit");
            }
        }

        public Status CharStatus
        {
            get
            {
                if(CurrentPhysicalDamage < MaxPhysicalHealth)
                {
                    return Status.CONSCIOUS;
                }
                else if(CurrentPhysicalDamage >= MaxPhysicalHealth && CurrentPhysicalDamage < (MaxPhysicalHealth + MaxOverFlowHealth))
                {
                    return Status.BLEEDING_OUT;
                }
                else
                {
                    return Status.DEAD;
                }
            }
        }

        private int _ess;

        public int ESS
        {
            get { return _ess; }
            set
            {
                _ess = value;
                NotifyPropertyChanged("ESS");
                NotifyPropertyChanged("SocialLimit");
            }
        }

        public int LOG
        {
            get { return _log; }
            set
            {
                _log = value;
                NotifyPropertyChanged("LOG");
                NotifyPropertyChanged("MentalLimit");
            }
        }

        public int MaxOverFlowHealth => BOD;

        public int MaxPhysicalHealth => (int)Math.Ceiling((decimal)BOD / 2) + 8;

        public int MaxStunHealth => (int)Math.Ceiling((decimal)WIL / 2) + 8;

        public int MentalLimit
        {
            get
            {
                int intermediateCalculation = (LOG * 2) + INTU + WIL;
                return (int)Math.Ceiling((decimal)intermediateCalculation / 3);
            }
        }

        public int PhysicalLimit
        {
            get
            {
                int intermediateCalculation = (STR * 2) + BOD + REA;
                return (int)Math.Ceiling((decimal)intermediateCalculation / 3);
            }
        }

        public int SocialLimit
        {
            get
            {
                int intermediateCalculation = (CHA * 2) + WIL + ESS;
                return (int)Math.Ceiling((decimal)intermediateCalculation / 3);
            }
        }

        public int REA
        {
            get { return _rea; }
            set
            {
                _rea = value;
                NotifyPropertyChanged("REA");
                NotifyPropertyChanged("PhysicalLimit");
            }
        }

        public int STR
        {
            get { return _str; }
            set
            {
                _str = value;
                NotifyPropertyChanged("STR");
                NotifyPropertyChanged("PhysicalLimit");
            }
        }

        public int WIL
        {
            get { return _wil; }
            set
            {
                _wil = value;
                NotifyPropertyChanged("WIL");
                NotifyPropertyChanged("MaxStunHealth");
                NotifyPropertyChanged("MentalLimit");
                NotifyPropertyChanged("SocialLimit");
            }
        }

        public void EndTurn()
        {
            Initiative -= 10;
        }

        public void generateRandomStats(string name, string affil)
        {
            AGI = gen.Next(1, 11);
            BOD = gen.Next(1, 11);
            CHA = gen.Next(1, 11);
            EDGE = gen.Next(1, 6);
            INTU = gen.Next(1, 11);
            LOG = gen.Next(1, 11);
            REA = gen.Next(1, 11);
            STR = gen.Next(1, 11);
            WIL = gen.Next(1, 11);
            Initiative = gen.Next(1, 20);
            CharacterName = name;
            Affiliation = affil;
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}