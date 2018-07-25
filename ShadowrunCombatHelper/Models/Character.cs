using ShadowrunCombatHelper.Globals;
using ShadowrunCombatHelper.Interfaces;
using ShadowrunCombatHelper.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ShadowrunCombatHelper.Models
{
    public class Character : INotifyPropertyChanged
    {
        protected static IRandomGenerator gen = new RandomGen();

        protected static IRandomGenerator roller = new RandomGen();

        private int _actionsRemaining;

        private Affiliation _affiliation;

        private int _agi;

        private int _armorValue;

        private int _bod;

        private int _cha;

        private CombatState _charCombatState;

        private Guid _charId;

        private int _currentPhysicalDamage;

        private int _currentStunDamage;

        private int _distanceMoved;

        private int _edge;

        private int _ess;

        private int _freeActionsRemaining;

        private int _initiative;

        private int _intu;

        private int _log;

        private int _magres;

        private string _name;

        private string _player;

        private int _rea;

        private bool _running;

        private CharacterSettings _settings;

        private CharacterBindingObservableCollection<Skill> _skills;

        private int _str;

        private MagicTradition _tradition;

        private int _wil;

        public Character()
        {
            _charId = Guid.NewGuid();
            _skills = new CharacterBindingObservableCollection<Skill>(this);
            CurrentPhysicalDamage = 0;
            CurrentStunDamage = 0;
            Settings = new CharacterSettings();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public enum CombatState { PHYSICAL, ASTRAL, VRCOLDSIM, VRHOTSIM, AR, }

        public enum Status { CONSCIOUS, BLEEDING_OUT, DEAD }

        public int ActionsRemaining
        {
            get { return _actionsRemaining; }
            set
            {
                _actionsRemaining = value;
                NotifyPropertyChanged("ActionsRemaining");
                NotifyPropertyChanged("CanSimpleAction");
                NotifyPropertyChanged("CanComplexAction");
            }
        }

        public Affiliation Affiliation
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
            set
            {
                _agi = value; NotifyPropertyChanged("AGI");
                NotifyPropertyChanged("WalkRate");
                NotifyPropertyChanged("RunRate");
            }
        }

        public int ArmorValue
        {
            get { return _armorValue; }
            set
            {
                _armorValue = value;
                NotifyPropertyChanged("ArmorValue");
                NotifyPropertyChanged("BaseArmor");
            }
        }

        public int BaseArmor
        {
            get
            {
                return BOD + ArmorValue;
            }
        }

        public int BaseDefense
        {
            get
            {
                return REA + INTU;
            }
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
                NotifyPropertyChanged("LiftCarry");
                NotifyPropertyChanged("Skills");
                NotifyPropertyChanged("BaseArmor");
            }
        }

        public Character BoundCharacter
        {
            get
            {
                return this;
            }
        }

        public bool CanComplexAction
        {
            get
            {
                return ActionsRemaining > 1;
            }
        }

        public bool CanFreeAction
        {
            get
            {
                return FreeActionsRemaining > 0;
            }
        }

        public bool CanFullDefense
        {
            get
            {
                return Initiative >= 10;
            }
        }

        public bool CanInterrupt
        {
            get
            {
                return Initiative >= 5;
            }
        }

        public bool CanMove
        {
            get
            {
                return DistanceMoved < MaxMovementThisTurn;
            }
        }

        public bool CanSimpleAction
        {
            get
            {
                return ActionsRemaining > 0;
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
                NotifyPropertyChanged("Composure");
                NotifyPropertyChanged("JudgeIntentions");
                NotifyPropertyChanged("Skills");
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

        public List<CombatState> CombatStatesList
        {
            get
            {
                return Enum.GetValues(typeof(CombatState)).Cast<CombatState>().ToList();
            }
        }

        public int Composure
        {
            get
            {
                return CHA + WIL;
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
                _currentPhysicalDamage = value.Clamp(0, MaxPhysicalHealth + MaxOverFlowHealth + 1);
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
                    _currentStunDamage = value.Clamp(0, MaxStunHealth);
                }
                NotifyPropertyChanged("CurrentStunDamage");
                NotifyPropertyChanged("CurrentDamagePenalty");
            }
        }

        public int DistanceMoved
        {
            get { return _distanceMoved; }
            set
            {
                if (value > MaxMovementThisTurn)
                {
                    _distanceMoved = MaxMovementThisTurn;
                }
                else
                {
                    _distanceMoved = value;
                }
                NotifyPropertyChanged("DistanceMoved");
                NotifyPropertyChanged("CanMove");
            }
        }

        public int EDGE
        {
            get { return _edge; }
            set
            {
                _edge = value; NotifyPropertyChanged("EDGE");
                NotifyPropertyChanged("Skills");
            }
        }

        public int ESS
        {
            get { return _ess; }
            set
            {
                _ess = value;
                NotifyPropertyChanged("ESS");
                NotifyPropertyChanged("SocialLimit");
                NotifyPropertyChanged("Skills");
            }
        }

        public int FreeActionsRemaining
        {
            get { return _freeActionsRemaining; }
            set
            {
                _freeActionsRemaining = value;
                NotifyPropertyChanged("FreeActionsRemaining");
                NotifyPropertyChanged("CanFreeAction");
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
                NotifyPropertyChanged("CanInterrupt");
                NotifyPropertyChanged("CanFullDefense");
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
                NotifyPropertyChanged("JudgeIntentions");
                NotifyPropertyChanged("Skills");
                NotifyPropertyChanged("BaseDefense");
            }
        }

        public int JudgeIntentions
        {
            get
            {
                return CHA + INTU;
            }
        }

        public int LiftCarry
        {
            get
            {
                return BOD + STR;
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
                NotifyPropertyChanged("Memory");
                NotifyPropertyChanged("Skills");
            }
        }

        public int MAGRES
        {
            get { return _magres; }
            set
            {
                _magres = value;
                NotifyPropertyChanged("MAGRES");
                NotifyPropertyChanged("Skills");
            }
        }

        public int MaxMovementThisTurn
        {
            get
            {
                return Running ? RunRate : WalkRate;
            }
        }

        public int MaxOverFlowHealth => BOD;

        public int MaxPhysicalHealth => (int)Math.Ceiling((decimal)BOD / 2) + 8;

        public int MaxStunHealth => (int)Math.Ceiling((decimal)WIL / 2) + 8;

        public int Memory
        {
            get
            {
                return LOG + WIL;
            }
        }

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
                NotifyPropertyChanged("Skills");
                NotifyPropertyChanged("BaseDefense");
            }
        }

        public int ResistDrain
        {
            get
            {
                if (Tradition != null)
                {
                    int total = 0;
                    foreach (var attr in Tradition.ResistDrainAttributes)
                    {
                        total += ProcessExternalAttributeDefinition(attr);
                    }
                    return total;
                }
                else
                {
                    return 0;
                }
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

        public bool Running
        {
            get { return _running; }
            set
            {
                _running = value;
                NotifyPropertyChanged("Running");
                NotifyPropertyChanged("MaxMovementThisTurn");
                NotifyPropertyChanged("CanMove");
            }
        }

        public int RunRate
        {
            get
            {
                return AGI * 4;
            }
        }

        public CharacterSettings Settings
        {
            get { return _settings; }
            set
            {
                _settings = value;
                NotifyPropertyChanged("Settings");
            }
        }

        public CharacterBindingObservableCollection<Skill> Skills
        {
            get { return _skills; }
            set
            {
                _skills = value;
                NotifyPropertyChanged("Skills");
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
                NotifyPropertyChanged("LiftCarry");
                NotifyPropertyChanged("Skills");
            }
        }

        public MagicTradition Tradition
        {
            get { return _tradition; }
            set
            {
                _tradition = value;
                NotifyPropertyChanged("Tradition");
                NotifyPropertyChanged("ResistDrain");
            }
        }

        public int WalkRate
        {
            get
            {
                return AGI * 2;
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
                NotifyPropertyChanged("Composure");
                NotifyPropertyChanged("Memory");
                NotifyPropertyChanged("Skills");
            }
        }

        public void Block()
        {
            Initiative -= 5;
        }

        public void ConsumeComplexAction()
        {
            ActionsRemaining -= 2;
        }

        public void ConsumeFreeAction()
        {
            FreeActionsRemaining -= 1;
        }

        public void ConsumeSimpleAction()
        {
            ActionsRemaining -= 1;
        }

        public void Dodge()
        {
            Initiative -= 5;
        }

        public void EndTurn()
        {
            Initiative -= 10;
            ActionsRemaining = 2;
            FreeActionsRemaining = 1;
        }

        public void FullDefense()
        {
            Initiative -= 10;
        }

        public void generateRandomStats(string name)
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
            Affiliation = new Affiliation();
        }

        public void HitTheDirt()
        {
            Initiative -= 5;
        }

        public void Intercept()
        {
            Initiative -= 5;
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public void Parry()
        {
            Initiative -= 5;
        }

        public void StartRound()
        {
            ActionsRemaining = 2;
            FreeActionsRemaining = 1;
            DistanceMoved = 0;
            Running = false;
        }

        public void ToggleRunning()
        {
            Running = !Running;
        }

        private int ProcessExternalAttributeDefinition(Skill.Attributes a)
        {
            switch (a)
            {
                case Skill.Attributes.AGI:
                    return AGI;

                case Skill.Attributes.BOD:
                    return BOD;

                case Skill.Attributes.CHA:
                    return CHA;

                case Skill.Attributes.EDGE:
                    return EDGE;

                case Skill.Attributes.INT:
                    return INTU;

                case Skill.Attributes.LOG:
                    return LOG;

                case Skill.Attributes.REA:
                    return REA;

                case Skill.Attributes.STR:
                    return STR;

                case Skill.Attributes.WIL:
                    return WIL;

                case Skill.Attributes.MAG:
                    return MAGRES;

                case Skill.Attributes.ESS:
                    return ESS;

                default:
                    return 0;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Character))
            {
                return false;
            }

            Character comparer = (Character)obj;
            return (this.CharacterName == comparer.CharacterName);
        }

        public override int GetHashCode()
        {
            int hashbase = 47;
            hashbase = hashbase * 13 + this.CharacterName.GetHashCode();
            return hashbase;
        }
    }
}