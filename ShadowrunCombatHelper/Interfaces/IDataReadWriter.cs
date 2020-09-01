using ShadowrunCombatHelper.Globals;
using System.Collections.Generic;

namespace ShadowrunCombatHelper.Interfaces
{
    public interface IDataReadWriter<T>
    {
        List<T> ReadFileToList(ApplicationXmlFiles.FileType fileType);

        void WriteListToFile(ApplicationXmlFiles.FileType fileType, List<T> listToWrite);
    }
}