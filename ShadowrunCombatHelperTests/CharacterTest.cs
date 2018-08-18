using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShadowrunCombatHelper.Models;

namespace ShadowrunCombatHelperTests
{
    [TestClass]
    public class CharacterTest
    {
        Character testChar = new Character();

        [TestInitialize]
        public void Startup()
        {
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
        }
        

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void Basearmor_CalculatesCorrectlyOnBODChange(int change)
        {
            int priorBase = testChar.BaseArmor;

            testChar.BOD = testChar.BOD + change;

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

            testChar.CHA = testChar.CHA + change;

            Assert.IsTrue(priorBase + change == testChar.Composure);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void Composure_CalculatesCorrectlyOnWILChange(int change)
        {
            int priorBase = testChar.Composure;

            testChar.WIL = testChar.WIL + change;

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

    }
}
