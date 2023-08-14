using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using ShadowrunCombatHelper.Interfaces;
using ShadowrunCombatHelper.Models;
using ShadowrunCombatHelper.Objects;
using ShadowrunCombatHelper.UserControls;

namespace ShadowrunCombatHelper.ViewModels
{
    public class InitiativeTracker_ViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Character> _combatQueue = new ObservableCollection<Character>();
        private int _currentRound;
        private bool _windowVisible;

        public InitiativeTracker_ViewModel()
        {
            CurrentRound = 1;
            WindowVisible = true;
        }

        public ObservableCollection<Character> CombatQueue
        {
            get => _combatQueue;
            set
            {
                _combatQueue = value;
                NotifyPropertyChanged(nameof(CombatQueue));
                NotifyPropertyChanged(nameof(CurrentCharacter));
            }
        }

        public Character CurrentCharacter()  {

            if(CombatQueue.Count == 0)
            {
                return null;
            }
            return CombatQueue.Aggregate(CustomAggregator);

        }

        public int CurrentRound
        {
            get => _currentRound;
            set
            {
                _currentRound = value;
                NotifyPropertyChanged(nameof(CurrentRound));
            }
        }

        public bool WindowVisible
        {
            get => _windowVisible;
            set
            {
                _windowVisible = value;
                NotifyPropertyChanged(nameof(WindowVisible));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void AddCombatants(List<Character> combatants)
        {
            WindowVisible = false;
            NotifyPropertyChanged(nameof(WindowVisible));
            foreach (Character c in combatants)
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
            NotifyPropertyChanged(nameof(WindowVisible));
            NotifyPropertyChanged(nameof(CurrentCharacter));
        }

        public void NotifyPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public void OnInitiativeChanged(object source, PropertyChangedEventArgs e)
        {
            //TODO: Replace hard-coded Initiative with something a little less brittle
            if (e.PropertyName == "Initiative")
            {
                NotifyPropertyChanged(nameof(CurrentCharacter));
                var c = (Character) source;
                CombatQueue.Remove(c);
                CombatQueue.Add(c);

                if (IsRoundOver())
                {
                    MoveToNextRound();
                }
            }
        }

        private static void RollInitiative(Character i)
        {
            if (i.CharStatus == Character.Status.CONSCIOUS)
            {
                if (i.Settings.ManuallyRollInitiative)
                {
                    try
                    {
                        var getInitiative = new InitiativeDialog(i);
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

        private static Character CustomAggregator(Character x1, Character x2)
        {
            if (x1.Initiative > x2.Initiative)
            {
                return x1;
            }

            if (x1.Initiative < x2.Initiative)
            {
                return x2;
            }

            if (string.Compare(x1.Affiliation.Name, x2.Affiliation.Name) > 0)
            {
                return x1;
            }

            if (string.Compare(x1.Affiliation.Name, x2.Affiliation.Name) < 0)
            {
                return x2;
            }

            if (string.Compare(x1.CharacterName, x2.CharacterName) > 0)
            {
                return x1;
            }

            return x2;
        }

        public void EndCombat()
        {
            for (var i = 0; i < CombatQueue.Count; i++)
            {
                if (!CombatQueue[i].Settings.PreserveDamageAcrossEncounters)
                {
                    CombatQueue[i].CurrentPhysicalDamage = 0;
                    CombatQueue[i].CurrentStunDamage = 0;
                }
            }
        }

        private bool IsRoundOver()
        {
            return !CombatQueue.Any(x => x.Initiative > 0);
        }

        private void MoveToNextRound()
        {
            CurrentRound++;
            WindowVisible = false;
            NotifyPropertyChanged(nameof(WindowVisible));
            var tempQueue = new List<Character>(CombatQueue);
            CombatQueue.Clear();
            foreach (Character i in tempQueue)
            {
                i.PropertyChanged -= OnInitiativeChanged;
                RollInitiative(i);
                i.StartRound();
                i.PropertyChanged += OnInitiativeChanged;
                CombatQueue.Add(i);
            }

            WindowVisible = true;
            NotifyPropertyChanged(nameof(WindowVisible));
            NotifyPropertyChanged(nameof(CurrentCharacter));
        }
    }
}