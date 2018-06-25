using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ShadowrunCombatHelper.Models;
using ShadowrunCombatHelper.Views;

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

        public Character CurrentCharacter
        {
            get { return CombatQueue.Aggregate((x1, x2) => x1.Initiative >= x2.Initiative ? x1 : x2); }
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
            CurrentRound = 0;           
        }

        public void AddCombatants(List<Character> combatants)
        {
            foreach(Character c in combatants)
            {
                c.Initiative = roller.Next(1, 20);
                c.PropertyChanged += OnInitiativeChanged;
                CombatQueue.Add(c);
            }
        }

        private void GenerateRandomCombatants()
        {
            Character newChar = new Character();
            for (int i = 0; i < 2; i++)
            {
                newChar = new Character();
                newChar.generateRandomStats($"Player{i}", "Players");
                newChar.PropertyChanged += OnInitiativeChanged;
                CombatQueue.Add(newChar);
            }

            for (int j = 0; j < 2; j++)
            {
                newChar = new Character();
                newChar.generateRandomStats($"Enemy{j}", "Enemies");
                newChar.PropertyChanged += OnInitiativeChanged;
                CombatQueue.Add(newChar);
            }
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
            List<Character> tempQueue = new List<Character>(CombatQueue);
            CombatQueue.Clear();
            foreach (var i in tempQueue)
            {
                i.PropertyChanged -= OnInitiativeChanged;
                if (i.ManuallyRollInitiative)
                {
                    i.Initiative = roller.Next(1, 20);
                }
                else
                {
                    i.Initiative = roller.Next(1, 20);
                }
                i.PropertyChanged += OnInitiativeChanged;
                CombatQueue.Add(i);
            }
            NotifyPropertyChanged("CurrentCharacter");
        }
    }
}
