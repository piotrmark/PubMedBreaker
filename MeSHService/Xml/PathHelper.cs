using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeSHService.Xml
{
    static class PathHelper
    {
        const string ProjectName = "MeSHService";

        public static string GetProjectDirectoryPath()
        {
            string mainDirPath = Path.GetDirectoryName(
                                    Path.GetDirectoryName(
                                        Path.GetDirectoryName(Directory.GetCurrentDirectory())));

            return Path.Combine(mainDirPath, ProjectName);
        }
    }
}
