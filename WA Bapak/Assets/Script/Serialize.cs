using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
using System;
using System.Reflection;

public static class ByteArraySerializer
{
    public static byte[] Serialize<T>(this T m)
    {
        using (var ms = new MemoryStream())
        {
            new BinaryFormatter().Serialize(ms, m);
            return ms.ToArray();
        }
    }

    public static T Deserialize<T>(this byte[] byteArray)
    {
        using (var ms = new MemoryStream(byteArray))
        {
            return (T)new BinaryFormatter().Deserialize(ms);
        }
    }
}

sealed class CustomizedBinder : SerializationBinder
{
    public override Type BindToType(string assemblyName, string typeName)
    {
        Type returntype = null;
        string sharedAssemblyName = "SharedAssembly, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        assemblyName = Assembly.GetExecutingAssembly().FullName;
        typeName = typeName.Replace(sharedAssemblyName, assemblyName);
        returntype =
                Type.GetType(String.Format("{0}, {1}",
                typeName, assemblyName));

        return returntype;
    }

    public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
    {
        base.BindToName(serializedType, out assemblyName, out typeName);
        assemblyName = "SharedAssembly, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
    }
}