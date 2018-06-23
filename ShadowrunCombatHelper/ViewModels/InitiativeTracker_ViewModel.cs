using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ShadowrunCombatHelper.Models;

namespace ShadowrunCombatHelper.ViewModels
{
    public class InitiativeTracker_ViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<Character> _combatQueue = new ObservableCollection<Character>();

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



        public InitiativeTracker_ViewModel()
        {
            GenerateRandomCombatants();
        }

        private void GenerateRandomCombatants()
        {
            Character newChar = new Character();
            for (int i = 0; i < 4; i++)
            {
                newChar = new Character();
                newChar.generateRandomStats($"Player{i}", "Players");
                newChar.PropertyChanged += OnInitiativeChanged;
                CombatQueue.Add(newChar);
            }

            for (int j = 0; j < 5; j++)
            {
                newChar = new Character();
                newChar.generateRandomStats($"Enemy{j}", "Enemies");
                newChar.PropertyChanged += OnInitiativeChanged;
                CombatQueue.Add(newChar);
            }
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
                Character c = (Character)source;
                CombatQueue.Remove(c);
                CombatQueue.Add(c);
            }
        }
    }
}
