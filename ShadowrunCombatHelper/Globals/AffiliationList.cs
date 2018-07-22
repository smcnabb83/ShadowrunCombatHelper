using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowrunCombatHelper.Models;
using ShadowrunCombatHelper.ExternalData;
using ShadowrunCombatHelper.Interfaces;
using System.Windows.Media;

namespace ShadowrunCombatHelper.Globals
{
    public sealed class AffiliationList
    {

        private List<Affiliation> _affiliations = new List<Affiliation>();

        private IDataReadWriter<Affiliation> readWriter = new XMLDataReadWriter<Affiliation>();

        public List<Affiliation> Affiliations
        {
            get
            {
                return _affiliations;
            }
            set
            {
                _affiliations = value;
            }
        }

        static AffiliationList() { }

        private AffiliationList()
        {
            Affiliations = readWriter.ReadFileToList(ApplicationXmlFiles.fileType.AFFILIATIONDATA);

            if(Affiliations.Count <= 0)
            {
                Affiliations.Add(new Affiliation() { Name = "Player", BackgroundColor = new int[] { 128, 0, 0, 255 }, ForegroundColor = new int[] { 255, 0, 0, 0 } });
                Affiliations.Add(new Affiliation() { Name = "Enemy", BackgroundColor = new int[] { 128, 255, 0, 0 }, ForegroundColor = new int[] { 255, 0, 0, 0 } });
            }
        }

        public void OverWriteAffiliations(List<Affiliation> newAffiliations)
        {
            Affiliations = newAffiliations;
            readWriter.WriteListToFile(ApplicationXmlFiles.fileType.AFFILIATIONDATA, Affiliations);
        }

        public static AffiliationList Instance { get; } = new AffiliationList();

        
    }
}
