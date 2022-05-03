using System.IO;
using System.Xml.Serialization;

public static class Helper
{
    //serialize the saved options
    public static string Serialize<T>(this T toSerialize)
    {
        XmlSerializer xml = new XmlSerializer(typeof(T));
        StringWriter writer = new StringWriter();
        //turning data into single string
        xml.Serialize(writer, toSerialize);
        return writer.ToString();
    }

    //deserialize 
    public static T Deserialize<T>(this string toDeserialize)
    {
        XmlSerializer xml = new XmlSerializer(typeof(T));
        //reader instead
        StringReader reader = new StringReader(toDeserialize);
        return (T)xml.Deserialize(reader);
    }
}