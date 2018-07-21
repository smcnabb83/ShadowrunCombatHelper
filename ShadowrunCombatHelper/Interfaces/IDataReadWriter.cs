using ShadowrunCombatHelper.Globals;
using System.Collections.Generic;

namespace ShadowrunCombatHelper.Interfaces
{
    public interface IDataReadWriter<T>
    {
        List<T> ReadFileToList(ApplicationXmlFiles.fileType fileType);

        void WriteListToFile(ApplicationXmlFiles.fileType fileType, List<T> ListToWrite);
    }
}