using System.Collections.Generic;
using ShadowrunCombatHelper.Globals;

namespace ShadowrunCombatHelper.Interfaces
{
    public interface IDataReadWriter<T>
    {
        List<T> ReadFileToList(ApplicationXmlFiles.fileType fileType);
        void WriteListToFile(ApplicationXmlFiles.fileType fileType, List<T> ListToWrite);
    }
}