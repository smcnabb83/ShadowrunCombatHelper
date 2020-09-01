using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using ShadowrunCombatHelper.Globals;
using ShadowrunCombatHelper.Interfaces;
using ShadowrunCombatHelper.Objects;

namespace ShadowrunCombatHelper.Models
{
    public class Character : INotifyPropertyChanged
    {
        public enum CombatState
        {
            PHYSICAL,
            ASTRAL,
            VRCOLDSIM,
            VRHOTSIM,
            AR
        }

        public enum Status
        {
            CONSCIOUS,
            BLEEDING_OUT,
            DEAD
        }

        protected static IRandomGenerator gen = new RandomGen();

        protected static IRandomGenerator roller = new RandomGen();

        private int _actionsRemaining;

        private Affiliation _affiliation;

        private int _agi;

        private int _armorValue;

        private int _bod;

        private int _cha;

        private CombatState _charCombatState;

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
            CharID = Guid.NewGuid();
            _skills = new CharacterBindingObservableCollection<Skill>(this);
            CurrentPhysicalDamage = 0;
            CurrentStunDamage = 0;
            Settings = new CharacterSettings();
        }

        public int ActionsRemaining
        {
            get => _actionsRemaining;
            set
            {
                _actionsRemaining = value;
                NotifyPropertyChanged(nameof(ActionsRemaining));
                NotifyPropertyChanged(nameof(CanSimpleAction));
                NotifyPropertyChanged(nameof(CanComplexAction));
            }
        }

        public Affiliation Affiliation
        {
            get => _affiliation;
            set
            {
                _affiliation = value;
                NotifyPropertyChanged(nameof(Affiliation));
            }
        }

        public int AGI
        {
            get => _agi;
            set
            {
                _agi = value;
                NotifyPropertyChanged(nameof(AGI));
                NotifyPropertyChanged(nameof(WalkRate));
                NotifyPropertyChanged(nameof(RunRate));
            }
        }

        public int ArmorValue
        {
            get => _armorValue;
            set
            {
                _armorValue = value;
                NotifyPropertyChanged(nameof(ArmorValue));
                NotifyPropertyChanged(nameof(BaseArmor));
            }
        }

        public int BaseArmor => BOD + ArmorValue;

        public int BaseDefense => REA + INTU;

        public int BOD
        {
            get => _bod;
            set
            {
                _bod = value;
                NotifyPropertyChanged(nameof(BOD));
                NotifyPropertyChanged(nameof(MaxPhysicalHealth));
                NotifyPropertyChanged(nameof(MaxOverflowHealth));
                NotifyPropertyChanged(nameof(PhysicalLimit));
                NotifyPropertyChanged(nameof(LiftCarry));
                NotifyPropertyChanged(nameof(Skills));
                NotifyPropertyChanged(nameof(BaseArmor));
            }
        }

        public Character BoundCharacter => this;

        public bool CanComplexAction => ActionsRemaining > 1;

        public bool CanFreeAction => FreeActionsRemaining > 0;

        public bool CanFullDefense => Initiative >= 10;

        public bool CanInterrupt => Initiative >= 5;

        public bool CanMove => DistanceMoved < MaxMovementThisTurn;

        public bool CanSimpleAction => ActionsRemaining > 0;

        public int CHA
        {
            get => _cha;
            set
            {
                _cha = value;
                NotifyPropertyChanged(nameof(CHA));
                NotifyPropertyChanged(nameof(SocialLimit));
                NotifyPropertyChanged(nameof(Composure));
                NotifyPropertyChanged(nameof(JudgeIntentions));
                NotifyPropertyChanged(nameof(Skills));
            }
        }

        public string CharacterName
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged(nameof(CharacterName));
            }
        }

        public CombatState CharCombatState
        {
            get => _charCombatState;
            set
            {
                _charCombatState = value;
                NotifyPropertyChanged(nameof(CharCombatState));
            }
        }

        public Guid CharID { get; }

        public Status CharStatus
        {
            get
            {
                if (CurrentPhysicalDamage <= MaxPhysicalHealth)
                {
                    return Status.CONSCIOUS;
                }

                if (CurrentPhysicalDamage > MaxPhysicalHealth &&
                    CurrentPhysicalDamage <= MaxPhysicalHealth + MaxOverflowHealth)
                {
                    return Status.BLEEDING_OUT;
                }

                return Status.DEAD;
            }
        }

        public List<CombatState> CombatStatesList => Enum.GetValues(typeof(CombatState)).Cast<CombatState>().ToList();

        public int Composure => CHA + WIL;

        public int CurrentDamagePenalty => (int) Math.Floor((decimal) CurrentPhysicalDamage / 3) +
                                           (int) Math.Floor((decimal) CurrentStunDamage / 3);

        public int CurrentPhysicalDamage
        {
            get => _currentPhysicalDamage;
            set
            {
                _currentPhysicalDamage = value.Clamp(0, MaxPhysicalHealth + MaxOverflowHealth + 1);
                if (CharStatus != Status.CONSCIOUS)
                {
                    Initiative = 0;
                }

                NotifyPropertyChanged(nameof(CurrentPhysicalDamage));
                NotifyPropertyChanged(nameof(CurrentDamagePenalty));
                NotifyPropertyChanged(nameof(CharStatus));
            }
        }

        public int CurrentStunDamage
        {
            get => _currentStunDamage;
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

                NotifyPropertyChanged(nameof(CurrentStunDamage));
                NotifyPropertyChanged(nameof(CurrentDamagePenalty));
            }
        }

        public int DistanceMoved
        {
            get => _distanceMoved;
            set
            {
                _distanceMoved = value.Clamp(0, MaxMovementThisTurn);
                NotifyPropertyChanged(nameof(DistanceMoved));
                NotifyPropertyChanged(nameof(CanMove));
            }
        }

        public int EDGE
        {
            get => _edge;
            set
            {
                _edge = value;
                NotifyPropertyChanged(nameof(EDGE));
                NotifyPropertyChanged(nameof(Skills));
            }
        }

        public int ESS
        {
            get => _ess;
            set
            {
                _ess = value;
                NotifyPropertyChanged(nameof(ESS));
                NotifyPropertyChanged(nameof(SocialLimit));
                NotifyPropertyChanged(nameof(Skills));
            }
        }

        public int FreeActionsRemaining
        {
            get => _freeActionsRemaining;
            set
            {
                _freeActionsRemaining = value;
                NotifyPropertyChanged(nameof(FreeActionsRemaining));
                NotifyPropertyChanged(nameof(CanFreeAction));
            }
        }

        public int Initiative
        {
            get => _initiative;
            set
            {
                _initiative = CharStatus == Status.CONSCIOUS ? value : 0;

                NotifyPropertyChanged(nameof(Initiative));
                NotifyPropertyChanged(nameof(CanInterrupt));
                NotifyPropertyChanged(nameof(CanFullDefense));
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
            get => _intu;
            set
            {
                _intu = value;
                NotifyPropertyChanged(nameof(INTU));
                NotifyPropertyChanged(nameof(MentalLimit));
                NotifyPropertyChanged(nameof(JudgeIntentions));
                NotifyPropertyChanged(nameof(Skills));
                NotifyPropertyChanged(nameof(BaseDefense));
            }
        }

        public int JudgeIntentions => CHA + INTU;

        public int LiftCarry => BOD + STR;

        public int LOG
        {
            get => _log;
            set
            {
                _log = value;
                NotifyPropertyChanged(nameof(LOG));
                NotifyPropertyChanged(nameof(MentalLimit));
                NotifyPropertyChanged(nameof(Memory));
                NotifyPropertyChanged(nameof(Skills));
            }
        }

        public int MAGRES
        {
            get => _magres;
            set
            {
                _magres = value;
                NotifyPropertyChanged(nameof(MAGRES));
                NotifyPropertyChanged(nameof(Skills));
            }
        }

        public int MaxMovementThisTurn => Running ? RunRate : WalkRate;

        public int MaxOverflowHealth => BOD;

        public int MaxPhysicalHealth => (int) Math.Ceiling((decimal) BOD / 2) + 8;

        public int MaxStunHealth => (int) Math.Ceiling((decimal) WIL / 2) + 8;

        public int Memory => LOG + WIL;

        public int MentalLimit
        {
            get
            {
                int intermediateCalculation = LOG * 2 + INTU + WIL;
                return (int) Math.Ceiling((decimal) intermediateCalculation / 3);
            }
        }

        public int PhysicalLimit
        {
            get
            {
                int intermediateCalculation = STR * 2 + BOD + REA;
                return (int) Math.Ceiling((decimal) intermediateCalculation / 3);
            }
        }

        public string Player
        {
            get => _player;
            set
            {
                _player = value;
                NotifyPropertyChanged(nameof(Player));
            }
        }

        public int REA
        {
            get => _rea;
            set
            {
                _rea = value;
                NotifyPropertyChanged(nameof(REA));
                NotifyPropertyChanged(nameof(PhysicalLimit));
                NotifyPropertyChanged(nameof(Skills));
                NotifyPropertyChanged(nameof(BaseDefense));
            }
        }

        public int ResistDrain
        {
            get
            {
                if (Tradition != null)
                {
                    var total = 0;
                    foreach (Skill.Attributes attr in Tradition.ResistDrainAttributes)
                    {
                        total += ProcessExternalAttributeDefinition(attr);
                    }

                    return total;
                }

                return 0;
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
            get => _running;
            set
            {
                _running = value;
                NotifyPropertyChanged(nameof(Running));
                NotifyPropertyChanged(nameof(MaxMovementThisTurn));
                NotifyPropertyChanged(nameof(CanMove));
            }
        }

        public int RunRate => AGI * 4;

        public CharacterSettings Settings
        {
            get => _settings;
            set
            {
                _settings = value;
                NotifyPropertyChanged(nameof(Settings));
            }
        }

        public CharacterBindingObservableCollection<Skill> Skills
        {
            get => _skills;
            set
            {
                _skills = value;
                NotifyPropertyChanged(nameof(Skills));
            }
        }

        public int SocialLimit
        {
            get
            {
                int intermediateCalculation = CHA * 2 + WIL + ESS;
                return (int) Math.Ceiling((decimal) intermediateCalculation / 3);
            }
        }

        public int STR
        {
            get => _str;
            set
            {
                _str = value;
                NotifyPropertyChanged(nameof(STR));
                NotifyPropertyChanged(nameof(PhysicalLimit));
                NotifyPropertyChanged(nameof(LiftCarry));
                NotifyPropertyChanged(nameof(Skills));
            }
        }

        public MagicTradition Tradition
        {
            get => _tradition;
            set
            {
                _tradition = value;
                NotifyPropertyChanged(nameof(Tradition));
                NotifyPropertyChanged(nameof(ResistDrain));
            }
        }

        public int WalkRate => AGI * 2;

        public int WIL
        {
            get => _wil;
            set
            {
                _wil = value;
                NotifyPropertyChanged(nameof(WIL));
                NotifyPropertyChanged(nameof(MaxStunHealth));
                NotifyPropertyChanged(nameof(MentalLimit));
                NotifyPropertyChanged(nameof(SocialLimit));
                NotifyPropertyChanged(nameof(Composure));
                NotifyPropertyChanged(nameof(Memory));
                NotifyPropertyChanged(nameof(Skills));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

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

        public void HitTheDirt()
        {
            Initiative -= 5;
        }

        public void Intercept()
        {
            Initiative -= 5;
        }

        public void NotifyPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
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
            if (obj?.GetType() != typeof(Character))
            {
                return false;
            }

            var comparer = (Character) obj;
            return CharacterName == comparer.CharacterName;
        }

        public override int GetHashCode()
        {
            var hashbase = 47;
            hashbase = hashbase * 13 + CharacterName.GetHashCode();
            return hashbase;
        }
    }
}