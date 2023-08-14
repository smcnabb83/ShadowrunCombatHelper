using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShadowrunCombatHelper.Models;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;

namespace ShadowrunCombatHelperTests
{
    [TestClass]
    public class CharacterTest
    {
        readonly Character testChar = new Character();
        string prop;

        
        [TestInitialize]
        public void Startup()
        {
            testChar.PropertyChanged += CapturePropertyChangesFiring;
            testChar.AGI = 5;
            testChar.STR = 5;
            testChar.INTU = 5;
            testChar.LOG = 5;
            testChar.BOD = 5;
            testChar.REA = 5;
            testChar.CHA = 5;
            testChar.EDGE = 1;
            testChar.ESS = 1;
            testChar.LOG = 5;
            testChar.WIL = 5;
            testChar.ArmorValue = 3;
            testChar.CurrentPhysicalDamage = 0;
            testChar.CurrentStunDamage = 0;
            prop = string.Empty;
        }

        [TestCleanup]
        public void Cleanup()
        {
            testChar.PropertyChanged -= CapturePropertyChangesFiring;
            prop = string.Empty;
        }
        

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void Basearmor_CalculatesCorrectlyOnBODChange(int change)
        {
            int priorBase = testChar.BaseArmor;

            testChar.BOD += change;

            Assert.IsTrue(priorBase + change == testChar.BaseArmor);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void BaseArmor_CalculatesCorrectlyOnArmorValueChange(int change)
        {
            int priorBase = testChar.BaseArmor;
            testChar.ArmorValue += change;
            Assert.IsTrue(priorBase + change == testChar.BaseArmor);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void BaseDefense_CalculatesCorrectlyOnREAChange(int change)
        {
            int priorBase = testChar.BaseDefense;
            testChar.REA += change;
            Assert.IsTrue(priorBase + change == testChar.BaseDefense, $"{priorBase + change} expected, {testChar.BaseDefense} returned");
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void BaseDefense_CalculatesCorrectlyOnINTUChange(int change)
        {
            int priorBase = testChar.BaseDefense;
            testChar.INTU += change;
            Assert.IsTrue(priorBase + change == testChar.BaseDefense);
        }

        [DataTestMethod]
        [DataRow(0, false)]
        [DataRow(1, false)]
        [DataRow(2, true)]
        [DataRow(3, true)]
        public void CanComplexAction(int actionsRemaining, bool expectedResult)
        {
            testChar.ActionsRemaining = actionsRemaining;
            Assert.AreEqual(expectedResult, testChar.CanComplexAction);
        }

        [DataTestMethod]
        [DataRow(0, false)]
        [DataRow(1, true)]
        [DataRow(2, true)]
        public void CanFreeAction(int freeActionsRemaining, bool expectedResult)
        {
            testChar.FreeActionsRemaining = freeActionsRemaining;
            Assert.AreEqual(expectedResult, testChar.CanFreeAction);
        }

        [DataTestMethod]
        [DataRow(9, false)]
        [DataRow(10, true)]
        [DataRow(11, true)]
        public void CanFullDefense(int initiative, bool expectedResult)
        {
            testChar.Initiative = initiative;
            Assert.AreEqual(expectedResult, testChar.CanFullDefense);
        }

        [DataTestMethod]
        [DataRow(4, false)]
        [DataRow(5, true)]
        [DataRow(6, true)]
        public void CanInterrupt(int initiative, bool expectedResult)
        {
            testChar.Initiative = initiative;
            Assert.AreEqual(expectedResult, testChar.CanInterrupt);
        }

        //These values are based on the initialized values in startup. If you change the AGI value, the tests must
        //be changed
        [DataTestMethod]
        [DataRow(9, true)]
        [DataRow(10, false)]
        public void CanMove(int distanceMoved, bool expectedResult)
        {
            testChar.DistanceMoved = distanceMoved;
            Assert.AreEqual(expectedResult, testChar.CanMove);
        }

        [DataTestMethod]
        [DataRow(0, false)]
        [DataRow(1, true)]
        [DataRow(2, true)]
        [DataRow(3, true)]
        public void CanSimpleAction(int actionsRemaining, bool expectedResult)
        {
            testChar.ActionsRemaining = actionsRemaining;
            Assert.AreEqual(expectedResult, testChar.CanSimpleAction);
        }

        [DataTestMethod]
        [DataRow(0, Character.Status.CONSCIOUS)]
        [DataRow(11, Character.Status.CONSCIOUS)]
        [DataRow(12, Character.Status.BLEEDING_OUT)]
        [DataRow(16, Character.Status.BLEEDING_OUT)]
        [DataRow(17, Character.Status.DEAD)]
        public void CharStatus_ReturnsCorrectStatus(int damageTaken, Character.Status expectedStatus)
        {
            testChar.CurrentPhysicalDamage = damageTaken;
            Assert.AreEqual(expectedStatus, testChar.CharStatus);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void Composure_CalculatesCorrectlyOnCHAChange(int change)
        {
            int priorBase = testChar.Composure;

            testChar.CHA += change;

            Assert.IsTrue(priorBase + change == testChar.Composure);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void Composure_CalculatesCorrectlyOnWILChange(int change)
        {
            int priorBase = testChar.Composure;

            testChar.WIL += change;

            Assert.IsTrue(priorBase + change == testChar.Composure);
        }

        [DataTestMethod]
        [DataRow(2,0,0)]
        [DataRow(3,0,1)]
        [DataRow(5,0,1)]
        [DataRow(6,0,2)]
        [DataRow(2, 2, 0)]
        [DataRow(2,3,1)]
        [DataRow(3, 3, 2)]
        [DataRow(5, 2, 1)]
        [DataRow(5,5,2)]
        [DataRow(6, 0, 2)]
        public void CurrentDamagePenalty_CalculatesCorrectly(int physDamage, int stunDamage, int expectedPenlaty)
        {
            testChar.CurrentPhysicalDamage = physDamage;
            testChar.CurrentStunDamage = stunDamage;
            Assert.AreEqual(expectedPenlaty, testChar.CurrentDamagePenalty);
        }
        [DataTestMethod]
        [DataRow(-9999, 0)]
        [DataRow(9999, 17)]
        public void CurrentPhysiclaDamage_ClampsCorrectly(int outOfBoundsValue, int expectedValue)
        {
            testChar.CurrentPhysicalDamage = outOfBoundsValue;
            Assert.AreEqual(expectedValue, testChar.CurrentPhysicalDamage);
        }

        [TestMethod]
        public void CurrentPhysicalDamage_InitiativeZeroOnCharacterUnconscious()
        {
            testChar.CurrentPhysicalDamage = testChar.MaxPhysicalHealth + 1;
            Assert.AreEqual(0, testChar.Initiative);
        }

        [DataTestMethod]
        [DataRow(-9999, 0)]
        [DataRow(9999, 11 )]
        public void CurrentStunDamage_ClampsCorrectly(int stunDamage, int expectedStunDamage)
        {
            testChar.CurrentStunDamage = stunDamage;
            Assert.AreEqual(expectedStunDamage, testChar.CurrentStunDamage);
        }

        [DataTestMethod]
        [DataRow(12,0)]
        [DataRow(13,1)]
        [DataRow(14,1)]
        [DataRow(15,2)]
        public void CurrentStunDamage_OverflowsToPhysicalCorrectly(int stunDamage, int expectedPhysicalDamage)
        {
            testChar.CurrentStunDamage = stunDamage;
            Assert.AreEqual(expectedPhysicalDamage, testChar.CurrentPhysicalDamage);
        }

        [DataTestMethod]
        [DataRow(-9999,0)]
        [DataRow(9999, 10)]
        public void DistanceMoved_ClampsCorrectly(int SetDistance, int ExpectedDistanceMoved)
        {
            testChar.DistanceMoved = SetDistance;
            Assert.AreEqual(ExpectedDistanceMoved, testChar.DistanceMoved);
        }

        [TestMethod]
        public void Initiative_StaysZeroWhenNotconscious()
        {
            testChar.CurrentPhysicalDamage = 9999;
            testChar.Initiative = 9999;
            Assert.AreEqual(0, testChar.Initiative);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void JudgeIntentions_CalculatesCorrectlyWhenCHAChanged(int change)
        {
            int priorBase = testChar.JudgeIntentions;

            testChar.CHA += change;

            Assert.IsTrue(priorBase + change == testChar.JudgeIntentions);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void JudgeIntentions_CalculatesCorrectlyWhenINTUChanged(int change)
        {
            int priorBase = testChar.JudgeIntentions;

            testChar.INTU += change;

            Assert.IsTrue(priorBase + change == testChar.JudgeIntentions);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void LiftCarry_CalculatesCorrectlyOnBODChange(int change)
        {
            int priorBase = testChar.LiftCarry;

            testChar.BOD += change;

            Assert.IsTrue(priorBase + change == testChar.LiftCarry);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void LiftCarry_CalculatesCorrectlyOnSTRChange(int change)
        {
            int priorBase = testChar.LiftCarry;

            testChar.STR += change;

            Assert.IsTrue(priorBase + change == testChar.LiftCarry);
        }

        [DataTestMethod]
        [DataRow(false, 10)]
        [DataRow(true, 20)]
        public void MaxMovementThisTurn_ProcessesRunningCorrectly(bool isRunning, int expectedMovement)
        {
            testChar.Running = isRunning;
            Assert.AreEqual(expectedMovement, testChar.MaxMovementThisTurn);
        }

        [DataTestMethod]
        [DataRow(1,9)]
        [DataRow(2,9)]
        [DataRow(3,10)]
        [DataRow(5, 11)]
        public void MaxPhysicalHealth_CalculatesCorrectly(int setBod, int expectedHealth)
        {
            testChar.BOD = setBod;
            Assert.AreEqual(expectedHealth, testChar.MaxPhysicalHealth);
        }

        [DataTestMethod]
        [DataRow(1, 9)]
        [DataRow(2, 9)]
        [DataRow(3, 10)]
        [DataRow(5, 11)]
        public void MaxStunHealth_CalculatesCorrectly(int setWil, int expectedHealth)
        {
            testChar.WIL = setWil;
            Assert.AreEqual(expectedHealth, testChar.MaxStunHealth);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void Memory_CalculatesCorrectlyOnLOGChange(int change)
        {
            int priorBase = testChar.Memory;

            testChar.LOG += change;

            Assert.IsTrue(priorBase + change == testChar.Memory);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void Memory_CalculatesCorrectlyOnWILChange(int change)
        {
            int priorBase = testChar.Memory;

            testChar.WIL += change;

            Assert.IsTrue(priorBase + change == testChar.Memory);
        }

        [DataTestMethod]
        [DataRow(1,1,1,2)]
        [DataRow(2,2,2,3)]
        [DataRow(3,3,3,4)]

        public void MentalLimit_CalculatesCorrectly(int setLOG, int setINTU, int setWIL, int expected)
        {
            testChar.LOG = setLOG;
            testChar.INTU = setINTU;
            testChar.WIL = setWIL;
            Assert.AreEqual(expected, testChar.MentalLimit);
        }

        [DataTestMethod]
        [DataRow(1, 1, 1, 2)]
        [DataRow(2, 2, 2, 3)]
        [DataRow(3, 3, 3, 4)]

        public void PhysicalLimit_CalculatesCorrectly(int setSTR, int setBOD, int setREA, int expected)
        {
            testChar.STR = setSTR;
            testChar.BOD = setBOD;
            testChar.REA = setREA;
            Assert.AreEqual(expected, testChar.PhysicalLimit);
        }

        [DataTestMethod]
        [DataRow(1, 1, 1, 2)]
        [DataRow(2, 2, 2, 3)]
        [DataRow(3, 3, 3, 4)]
        public void SocialLimit_CalculatesCorrectly(int setCHA, int setWIL, int setESS, int expected)
        {
            testChar.CHA = setCHA;
            testChar.WIL = setWIL;
            testChar.ESS = setESS;
            Assert.AreEqual(expected, testChar.SocialLimit);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void RunRate_CalculatesOnAGIChange(int change)
        {
            testChar.AGI = change;
            Assert.AreEqual(change * 4, testChar.RunRate);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void WalkRate_CalculatesOnAGIChange(int change)
        {
            testChar.AGI = change;
            Assert.AreEqual(change * 2, testChar.WalkRate);
        }

        [TestMethod]
        public void Block_ConsumesInitiativeCorrectly()
        {
            testChar.Initiative = 5;
            testChar.Block();
            Assert.AreEqual(0, testChar.Initiative);
        }

        [TestMethod]
        public void Dodge_ConsumesInitiativeCorrectly()
        {
            testChar.Initiative = 5;
            testChar.Dodge();
            Assert.AreEqual(0, testChar.Initiative);
        }

        [TestMethod]
        public void HitTheDirt_ConsumesInitiativeCorrectly()
        {
            testChar.Initiative = 5;
            testChar.HitTheDirt();
            Assert.AreEqual(0, testChar.Initiative);
        }

        [TestMethod]
        public void Intercept_ConsumesInitiativeCorrectly()
        {
            testChar.Initiative = 5;
            testChar.Intercept();
            Assert.AreEqual(0, testChar.Initiative);
        }

        [TestMethod]
        public void Parry_ConsumesInitiativeCorrectly()
        {
            testChar.Initiative = 5;
            testChar.Parry();
            Assert.AreEqual(0, testChar.Initiative);
        }

        [TestMethod]
        public void FullDefense_ConsumesInitiativeCorrectly()
        {
            testChar.Initiative = 15;
            testChar.FullDefense();
            Assert.AreEqual(5, testChar.Initiative);
        }

        [TestMethod]
        public void ConsumeComplexAction_ConsumesActionsCorrectly()
        {
            testChar.ActionsRemaining = 2;
            testChar.ConsumeComplexAction();
            Assert.AreEqual(0, testChar.ActionsRemaining);
        }

        [TestMethod]
        public void ConsumeFreeAction_ConsumesFreeActionsCorrectly()
        {
            testChar.FreeActionsRemaining = 1;
            testChar.ConsumeFreeAction();
            Assert.AreEqual(0, testChar.FreeActionsRemaining);
        }

        [TestMethod]
        public void ConsumeSimpleAction_ConsumesActionsCorrectly()
        {
            testChar.ActionsRemaining = 2;
            testChar.ConsumeSimpleAction();
            Assert.AreEqual(1, testChar.ActionsRemaining);
        }

        [TestMethod]
        public void EndTurn_SetsCharacterUpCorrectly()
        {
            testChar.Initiative = 15;
            testChar.ActionsRemaining = 0;
            testChar.FreeActionsRemaining = 0;
            testChar.EndTurn();
            Assert.IsTrue(testChar.Initiative == 5 && testChar.ActionsRemaining == 2 && testChar.FreeActionsRemaining == 1);
        }

        [TestMethod]
        public void AffiliationChange_FiresPropertyChangedEvent()
        {
            testChar.Affiliation = new Affiliation() { Name = "test" };
            Assert.AreEqual("Affiliation;", prop);
        }

        [TestMethod]
        public void CharacterName_FiresPropertyChangedEvent()
        {
            testChar.CharacterName = "TestName2";
            Assert.AreEqual("CharacterName;", prop);
        }

        [TestMethod]
        public void CharCombatState_FiresPropertyChangedEvent()
        {
            testChar.CharCombatState = Character.CombatState.AR;
            Assert.AreEqual("CharCombatState;", prop);
        }

        [TestMethod]
        public void MAGRES_FiresPropertyChangedEvents()
        {
            testChar.MAGRES = 5;
            Assert.AreEqual("MAGRES;Skills;", prop);
        }

        [TestMethod]
        public void Player_FiresPropertyChangedEvents()
        {
            testChar.Player = "TEST";
            Assert.AreEqual("Player;", prop);
        }

        [TestMethod]
        public void Tradition_FiresPropertyChangedEvents()
        {
            testChar.Tradition = new MagicTradition("TEST", new List<Skill.Attributes>() { Skill.Attributes.AGI });
            Assert.AreEqual("Tradition;ResistDrain;", prop);
        }

        [TestMethod]
        public void Equals_TestIfComparesCorrectly()
        {
            Character newChar = new Character();
            testChar.CharacterName = "Test";
            newChar.CharacterName = "Test";
            Assert.IsTrue(testChar.Equals(newChar));
        }

        [TestMethod]
        public void EqualSign_TestIfComparesCorrectly()
        {
            Character newChar = new Character();
            testChar.CharacterName = "Test";
            newChar.CharacterName = "Test";
            Assert.IsFalse(testChar == newChar);
        }

        [TestMethod]
        public void HashCode_SameName_GeneratesSameHashCode()
        {
            Character newChar = new Character();
            testChar.CharacterName = "Test";
            newChar.CharacterName = "Test";
            Assert.AreEqual(testChar.GetHashCode(), newChar.GetHashCode());
        }

        [TestMethod]
        public void HashCode_DifferentName_GeneratesDifferenthashCode()
        {
            Character newChar = new Character();
            testChar.CharacterName = "Test";
            newChar.CharacterName = "Jiff";
            Assert.AreNotEqual(testChar.GetHashCode(), newChar.GetHashCode());
        }

        private void CapturePropertyChangesFiring(object o, PropertyChangedEventArgs e)
        {
            prop += e.PropertyName + ";";
        }

    }
}
