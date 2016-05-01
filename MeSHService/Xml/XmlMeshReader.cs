using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace MeSHService.Xml
{
    public static class XmlMeshReader
    {
        const string MeshDictionaryFilename = "desc2016.xml";

        public static DescriptorRecordSet Read()
        {
            string filePath = Path.Combine(PathHelper.GetProjectDirectoryPath(), MeshDictionaryFilename);
            FileStream readStrem = new FileStream(filePath, FileMode.Open);

            XmlSerializer meshSerializer = new XmlSerializer(typeof(DescriptorRecordSet));
            DescriptorRecordSet set = meshSerializer.Deserialize(readStrem) as DescriptorRecordSet;

            return set;
        }
    }
}
