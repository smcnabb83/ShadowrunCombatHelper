using System;
using System.ComponentModel;

namespace ShadowrunCombatHelper.Models
{
    public class Character : INotifyPropertyChanged
    {
        private static Random gen = new Random();
        private static Random roller = new Random();

        private string _affiliation;

        private int _agi;

        private int _bod;

        private int _cha;

        private CombatState _charCombatState;

        private Guid _charId;

        private int _currentPhysicalDamage;

        private int _currentStunDamage;

        private int _edge;

        private int _ess;

        private int _initiative;

        private int _intu;

        private int _log;

        private bool _manuallyRollInitiative;

        private string _name;

        private int _rea;

        private int _str;

        private int _wil;

        private string _player;

        public Character()
        {
            _charId = Guid.NewGuid();
            CurrentPhysicalDamage = 0;
            CurrentStunDamage = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public enum CombatState { PHYSICAL, ASTRAL, VRCOLDSIM, VRHOTSIM, AR, }

        public enum Status { CONSCIOUS, BLEEDING_OUT, DEAD }
        public string Affiliation
        {
            get { return _affiliation; }
            set
            {
                _affiliation = value;
                NotifyPropertyChanged("Affiliation");
            }
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

        public Character BoundCharacter
        { get { return this; } }
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
            set
            {
                _name = value;
                NotifyPropertyChanged("CharacterName");
            }
        }

        public CombatState CharCombatState
        {
            get { return _charCombatState; }
            set
            {
                _charCombatState = value;
                NotifyPropertyChanged("CharCombatState");
            }
        }

        public Guid CharID
        {
            get { return _charId; }
        }

        public Status CharStatus
        {
            get
            {
                if (CurrentPhysicalDamage <= MaxPhysicalHealth)
                {
                    return Status.CONSCIOUS;
                }
                else if (CurrentPhysicalDamage > MaxPhysicalHealth && CurrentPhysicalDamage <= (MaxPhysicalHealth + MaxOverFlowHealth))
                {
                    return Status.BLEEDING_OUT;
                }
                else
                {
                    return Status.DEAD;
                }
            }
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
                if (CharStatus != Status.CONSCIOUS)
                {
                    Initiative = 0;
                }
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
                if (value > MaxStunHealth)
                {
                    int overflow = value - MaxStunHealth;
                    CurrentPhysicalDamage += overflow / 2;
                    _currentStunDamage = MaxStunHealth;
                }
                else
                {
                    _currentStunDamage = value;
                }
                NotifyPropertyChanged("CurrentStunDamage");
                NotifyPropertyChanged("CurrentDamagePenalty");
            }
        }

        public int EDGE
        {
            get { return _edge; }
            set { _edge = value; NotifyPropertyChanged("EDGE"); }
        }

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

        public int Initiative
        {
            get { return _initiative; }
            set
            {
                if (CharStatus == Status.CONSCIOUS)
                {
                    _initiative = value;
                }
                else
                {
                    _initiative = 0;
                }
                NotifyPropertyChanged("Initiative");
            }
        }

        public string InitiativeRollText
        {
            get
            {
                switch (CharCombatState)
                {
                    case CombatState.PHYSICAL:
                        return $"Roll {REA + INTU} + 1D6";

                    case CombatState.ASTRAL:
                        return $"Roll {INTU * 2} + 2D6";

                    case CombatState.AR:
                        return $"Roll {REA + INTU} + 1D6";

                    case CombatState.VRCOLDSIM:
                        return $"Roll {LOG + INTU} + 3D6";

                    case CombatState.VRHOTSIM:
                        return $"Roll {LOG + INTU} + 4D6";

                    default:
                        return "";
                }
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

        public bool ManuallyRollInitiative
        {
            get { return _manuallyRollInitiative; }
            set
            {
                _manuallyRollInitiative = value;
                NotifyPropertyChanged("ManuallyRollInitiative");
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

        public string Player
        {
            get { return _player; }
            set
            {
                _player = value;
                NotifyPropertyChanged("Player");
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

        public int RollInitiative
        {
            get
            {
                switch (CharCombatState)
                {
                    case CombatState.PHYSICAL:
                        return REA + INTU + roller.Next(1, 7);

                    case CombatState.ASTRAL:
                        return INTU * 2 + roller.Next(2, 13);

                    case CombatState.AR:
                        return REA + INTU + roller.Next(1, 7);

                    case CombatState.VRCOLDSIM:
                        return LOG + INTU + roller.Next(3, 19);

                    case CombatState.VRHOTSIM:
                        return LOG + INTU + roller.Next(4, 25);

                    default:
                        return 1;
                }
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