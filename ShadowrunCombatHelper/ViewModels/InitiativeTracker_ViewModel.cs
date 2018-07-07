using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ShadowrunCombatHelper.Models;
using ShadowrunCombatHelper.Views;
using ShadowrunCombatHelper.UserControls;

namespace ShadowrunCombatHelper.ViewModels
{
    public class InitiativeTracker_ViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<Character> _combatQueue = new ObservableCollection<Character>();
        private static Random roller = new Random();

        public ObservableCollection<Character> CombatQueue
        {
            get { return _combatQueue; }
            set
            {
                _combatQueue = value;
                NotifyPropertyChanged("CombatQueue");
                NotifyPropertyChanged("CurrentCharacter");
            }
        }

        private bool _windowVisible;

        public bool WindowVisible
        {
            get { return _windowVisible; }
            set { _windowVisible = value;
                NotifyPropertyChanged("WindowVisible");
            }
        }


        private Character CustomAggregator(Character x1, Character x2)
        {
            if(x1.Initiative > x2.Initiative)
            {
                return x1;
            }
            else if (x1.Initiative < x2.Initiative)
            {
                return x2;
            }
            else
            {
                if(string.Compare(x1.Affiliation.Name, x2.Affiliation.Name) > 0)
                {
                    return x1;
                }

                else if (string.Compare(x1.Affiliation.Name, x2.Affiliation.Name) < 0)
                {
                    return x2;
                }
                else
                {
                    if(string.Compare(x1.CharacterName, x2.CharacterName) > 0)
                    {
                        return x1;
                    }
                    else
                    {
                        return x2;
                    }
                }
            }
        }

        public Character CurrentCharacter
        {
            get { return CombatQueue.Aggregate(CustomAggregator); }
        }

        private int _currentRound;

        public int CurrentRound
        {
            get { return _currentRound; }
            set { _currentRound = value;
                NotifyPropertyChanged("CurrentRound");
            }
        }




        public InitiativeTracker_ViewModel()
        {
            CurrentRound = 1;
            WindowVisible = true;
        }

        public void AddCombatants(List<Character> combatants)
        {
            WindowVisible = false;
            NotifyPropertyChanged("WindowVisible");
            foreach(Character c in combatants)
            {
                if (!c.Settings.PreserveDamageAcrossEncounters)
                {
                    c.CurrentPhysicalDamage = 0;
                    c.CurrentStunDamage = 0;
                }
                RollInitiative(c);
                c.StartRound();
                c.PropertyChanged += OnInitiativeChanged;
                CombatQueue.Add(c);
            }
            WindowVisible = true;
            NotifyPropertyChanged("WindowVisible");
            NotifyPropertyChanged("CurrentCharacter");
        }

        private bool IsRoundOver()
        {
            return CombatQueue.Where(x => x.Initiative > 0).Count() == 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string property)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public void OnInitiativeChanged(object source, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Initiative")
            {
                NotifyPropertyChanged("CurrentCharacter");
                Character c = (Character)source;
                CombatQueue.Remove(c);
                CombatQueue.Add(c);

                if (IsRoundOver())
                {
                    MoveToNextRound();
                }

            }
        }
        private void MoveToNextRound()
        {
            CurrentRound++;
            WindowVisible = false;
            NotifyPropertyChanged("WindowVisible");
            List<Character> tempQueue = new List<Character>(CombatQueue);
            CombatQueue.Clear();
            foreach (var i in tempQueue)
            {
                i.PropertyChanged -= OnInitiativeChanged;
                RollInitiative(i);
                i.StartRound();
                i.PropertyChanged += OnInitiativeChanged;
                CombatQueue.Add(i);
            }
            WindowVisible = true;
            NotifyPropertyChanged("WindowVisible");
            NotifyPropertyChanged("CurrentCharacter");
        }

        private static void RollInitiative(Character i)
        {
            if (i.CharStatus == Character.Status.CONSCIOUS)
            {
                if (i.ManuallyRollInitiative)
                {
                    try
                    {
                        InitiativeDialog getInitiative = new InitiativeDialog(i);
                        bool? result = false;
                        while ((result ?? false) == false)
                        {
                            result = getInitiative.ShowDialog();
                        }

                        i.Initiative = getInitiative.InitiativeRolledValue;
                    }
                    catch
                    {
                        RollInitiative(i);
                    }
                }
                else
                {
                    i.Initiative = i.RollInitiative;
                }
            }
        }
    }
}
