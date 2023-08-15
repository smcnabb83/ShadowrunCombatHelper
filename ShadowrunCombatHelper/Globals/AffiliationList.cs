using System.Collections.Generic;
using ShadowrunCombatHelper.ExternalData;
using ShadowrunCombatHelper.Interfaces;
using ShadowrunCombatHelper.Models;

namespace ShadowrunCombatHelper.Globals
{
    public sealed class AffiliationList
    {
        private readonly IDataReadWriter<Affiliation> _readWriter = new XMLDataReadWriter<Affiliation>();

        static AffiliationList()
        {
        }

        private AffiliationList()
        {
            Affiliations = _readWriter.ReadFileToList(ApplicationXmlFiles.FileType.AffiliationData);

            if (Affiliations.Count > 0) return; 

            Affiliations.Add(new Affiliation
            {
                Name = "Player", BackgroundColor = new[] {128, 0, 0, 255}, ForegroundColor = new[] {255, 0, 0, 0}
            });
            Affiliations.Add(new Affiliation
                {Name = "Enemy", BackgroundColor = new[] {128, 255, 0, 0}, ForegroundColor = new[] {255, 0, 0, 0}});
        }

        public List<Affiliation> Affiliations { get; set; } = new List<Affiliation>();

        public static AffiliationList Instance { get; } = new AffiliationList();

        public void OverWriteAffiliations(List<Affiliation> newAffiliations)
        {
            Affiliations = newAffiliations;
            _readWriter.WriteListToFile(ApplicationXmlFiles.FileType.AffiliationData, Affiliations);
        }
    }
}