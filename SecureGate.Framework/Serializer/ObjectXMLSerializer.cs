using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
// For serialization of an object to an XML Document file.
// For reading/writing data to an XML file.
// For accessing user isolated data.
// For serialization of an object to an XML Binary file.

namespace SecureGate.Framework.Serializer
{
    /// <summary>
    ///     Serialization format types.
    /// </summary>
    public enum SerializedFormat
    {
        /// <summary>
        ///     Binary serialization format.
        /// </summary>
        Binary,

        /// <summary>
        ///     Document serialization format.
        /// </summary>
        Document
    }

    public class ObjectXMLSerializer<T> where T : class
    {
        #region Load methods

        /// <summary>
        ///     Load a entity from a data row
        /// </summary>
        /// <param name="entityRow">data table data row</param>
        /// <returns>entity object</returns>
        public static T Load(DataRow entityRow)
        {
            var doc = new XmlDocument();
            var sb = new StringBuilder();
            DataTable tableToSerialize;

            if (entityRow.Table.Rows.Count > 1)
            {
                tableToSerialize = entityRow.Table.Clone();
                tableToSerialize.ImportRow(entityRow);
                tableToSerialize.TableName = typeof (T).Name;
            }
            else
            {
                tableToSerialize = entityRow.Table;
            }

            tableToSerialize.WriteXml(new StringWriter(sb));
            doc.Load(new StringReader(sb.ToString()));
            var ser = new XmlSerializer(typeof (T));

            return (T) ser.Deserialize(new StringReader(doc.DocumentElement.FirstChild.OuterXml));
        }


        public static T Load(DataRow entityRow, string tblName)
        {
            var doc = new XmlDocument();
            var sb = new StringBuilder();
            DataTable tableToSerialize;

            if (entityRow.Table.Rows.Count > 1)
            {
                tableToSerialize = entityRow.Table.Clone();
                tableToSerialize.ImportRow(entityRow);
                tableToSerialize.TableName = tblName;
            }
            else
            {
                tableToSerialize = entityRow.Table;
            }

            tableToSerialize.WriteXml(new StringWriter(sb));
            doc.Load(new StringReader(sb.ToString()));
            var ser = new XmlSerializer(typeof (T));

            return (T) ser.Deserialize(new StringReader(doc.DocumentElement.FirstChild.OuterXml));
        }

        public static T Load(string xmlString)
        {
            var ser = new XmlSerializer(typeof (T));
            return (T) ser.Deserialize(new StringReader(xmlString));
        }

        public static List<T> LoadMultiList(string xmlString)
        {
            var ser = new XmlSerializer(typeof (List<T>));
            return (List<T>) ser.Deserialize(new StringReader(xmlString));
        }


        /// <summary>
        ///     Loads an object from an XML file in Document format.
        /// </summary>
        /// <example>
        ///     <code>
        /// serializableObject = ObjectXMLSerializer&lt;SerializableObject&gt;.Load(@"C:\XMLObjects.xml");
        /// </code>
        /// </example>
        /// <param name="path">Path of the file to load the object from.</param>
        /// <returns>Object loaded from an XML file in Document format.</returns>
        /// <summary>
        ///     Loads an object from an XML file using a specified serialized format.
        /// </summary>
        /// <example>
        ///     <code>
        /// serializableObject = ObjectXMLSerializer&lt;SerializableObject&gt;.Load(@"C:\XMLObjects.xml", SerializedFormat.Binary);
        /// </code>
        /// </example>
        /// <param name="path">Path of the file to load the object from.</param>
        /// <param name="serializedFormat">XML serialized format used to load the object.</param>
        /// <returns>Object loaded from an XML file using the specified serialized format.</returns>
        public static T Load(string path, SerializedFormat serializedFormat)
        {
            T serializableObject = null;

            switch (serializedFormat)
            {
                case SerializedFormat.Binary:
                    serializableObject = LoadFromBinaryFormat(path, null);
                    break;

                case SerializedFormat.Document:
                default:
                    serializableObject = LoadFromDocumentFormat(null, path, null);
                    break;
            }

            return serializableObject;
        }

        /// <summary>
        ///     Loads an object from an XML file in Document format, supplying extra data types to enable deserialization of custom
        ///     types within the object.
        /// </summary>
        /// <example>
        ///     <code>
        /// serializableObject = ObjectXMLSerializer&lt;SerializableObject&gt;.Load(@"C:\XMLObjects.xml", new Type[] { typeof(MyCustomType) });
        /// </code>
        /// </example>
        /// <param name="path">Path of the file to load the object from.</param>
        /// <param name="extraTypes">Extra data types to enable deserialization of custom types within the object.</param>
        /// <returns>Object loaded from an XML file in Document format.</returns>
        public static T Load(string path, Type[] extraTypes)
        {
            var serializableObject = LoadFromDocumentFormat(extraTypes, path, null);
            return serializableObject;
        }

        /// <summary>
        ///     Loads an object from an XML file in Document format, located in a specified isolated storage area.
        /// </summary>
        /// <example>
        ///     <code>
        /// serializableObject = ObjectXMLSerializer&lt;SerializableObject&gt;.Load("XMLObjects.xml", IsolatedStorageFile.GetUserStoreForAssembly());
        /// </code>
        /// </example>
        /// <param name="fileName">Name of the file in the isolated storage area to load the object from.</param>
        /// <param name="isolatedStorageDirectory">Isolated storage area directory containing the XML file to load the object from.</param>
        /// <returns>Object loaded from an XML file in Document format located in a specified isolated storage area.</returns>
        public static T Load(string fileName, IsolatedStorageFile isolatedStorageDirectory)
        {
            var serializableObject = LoadFromDocumentFormat(null, fileName, isolatedStorageDirectory);
            return serializableObject;
        }

        /// <summary>
        ///     Loads an object from an XML file located in a specified isolated storage area, using a specified serialized format.
        /// </summary>
        /// <example>
        ///     <code>
        /// serializableObject = ObjectXMLSerializer&lt;SerializableObject&gt;.Load("XMLObjects.xml", IsolatedStorageFile.GetUserStoreForAssembly(), SerializedFormat.Binary);
        /// </code>
        /// </example>
        /// <param name="fileName">Name of the file in the isolated storage area to load the object from.</param>
        /// <param name="isolatedStorageDirectory">Isolated storage area directory containing the XML file to load the object from.</param>
        /// <param name="serializedFormat">XML serialized format used to load the object.</param>
        /// <returns>
        ///     Object loaded from an XML file located in a specified isolated storage area, using a specified serialized
        ///     format.
        /// </returns>
        public static T Load(string fileName, IsolatedStorageFile isolatedStorageDirectory,
            SerializedFormat serializedFormat)
        {
            T serializableObject = null;

            switch (serializedFormat)
            {
                case SerializedFormat.Binary:
                    serializableObject = LoadFromBinaryFormat(fileName, isolatedStorageDirectory);
                    break;

                case SerializedFormat.Document:
                default:
                    serializableObject = LoadFromDocumentFormat(null, fileName, isolatedStorageDirectory);
                    break;
            }

            return serializableObject;
        }

        /// <summary>
        ///     Loads an object from an XML file in Document format, located in a specified isolated storage area, and supplying
        ///     extra data types to enable deserialization of custom types within the object.
        /// </summary>
        /// <example>
        ///     <code>
        /// serializableObject = ObjectXMLSerializer&lt;SerializableObject&gt;.Load("XMLObjects.xml", IsolatedStorageFile.GetUserStoreForAssembly(), new Type[] { typeof(MyCustomType) });
        /// </code>
        /// </example>
        /// <param name="fileName">Name of the file in the isolated storage area to load the object from.</param>
        /// <param name="isolatedStorageDirectory">Isolated storage area directory containing the XML file to load the object from.</param>
        /// <param name="extraTypes">Extra data types to enable deserialization of custom types within the object.</param>
        /// <returns>
        ///     Object loaded from an XML file located in a specified isolated storage area, using a specified serialized
        ///     format.
        /// </returns>
        public static T Load(string fileName, IsolatedStorageFile isolatedStorageDirectory, Type[] extraTypes)
        {
            var serializableObject = LoadFromDocumentFormat(null, fileName, isolatedStorageDirectory);
            return serializableObject;
        }

        #endregion

        #region Save methods

        /// <summary>
        ///     Save object to a xml string
        /// </summary>
        /// <typeparam name="T">The type of the Business Entity to be serialized.</typeparam>
        /// <param name="entity">The entity object.</param>
        /// <returns>XML string representing the entity.</returns>
        public static string Save(T serializableObject)
        {
            //Create an XML Serializer object by passing to it the type of object that needs to be serialized
            var serializer = new XmlSerializer(typeof (T));
            //This stringbuilder object will hold the serialized data 
            var sb = new StringBuilder();

            var xmlwSettings = new XmlWriterSettings();
            xmlwSettings.OmitXmlDeclaration = true;

            var xw = XmlWriter.Create(sb, xmlwSettings);
            serializer.Serialize(xw, serializableObject);
            return RemoveXmlNilNodes(sb.ToString());
        }

        /// <summary>
        ///     Saves an object to an XML file in Document format.
        /// </summary>
        /// <example>
        ///     <code>        
        /// SerializableObject serializableObject = new SerializableObject();
        /// 
        /// ObjectXMLSerializer&lt;SerializableObject&gt;.Save(serializableObject, @"C:\XMLObjects.xml");
        /// </code>
        /// </example>
        /// <param name="serializableObject">Serializable object to be saved to file.</param>
        /// <param name="path">Path of the file to save the object to.</param>
        public static void Save(T serializableObject, string path)
        {
            SaveToDocumentFormat(serializableObject, null, path, null);
        }

        /// <summary>
        ///     Saves an object to an XML file using a specified serialized format.
        /// </summary>
        /// <example>
        ///     <code>
        /// SerializableObject serializableObject = new SerializableObject();
        /// 
        /// ObjectXMLSerializer&lt;SerializableObject&gt;.Save(serializableObject, @"C:\XMLObjects.xml", SerializedFormat.Binary);
        /// </code>
        /// </example>
        /// <param name="serializableObject">Serializable object to be saved to file.</param>
        /// <param name="path">Path of the file to save the object to.</param>
        /// <param name="serializedFormat">XML serialized format used to save the object.</param>
        public static void Save(T serializableObject, string path, SerializedFormat serializedFormat)
        {
            switch (serializedFormat)
            {
                case SerializedFormat.Binary:
                    SaveToBinaryFormat(serializableObject, path, null);
                    break;

                case SerializedFormat.Document:
                default:
                    SaveToDocumentFormat(serializableObject, null, path, null);
                    break;
            }
        }

        /// <summary>
        ///     Saves an object to an XML file in Document format, supplying extra data types to enable serialization of custom
        ///     types within the object.
        /// </summary>
        /// <example>
        ///     <code>        
        /// SerializableObject serializableObject = new SerializableObject();
        /// 
        /// ObjectXMLSerializer&lt;SerializableObject&gt;.Save(serializableObject, @"C:\XMLObjects.xml", new Type[] { typeof(MyCustomType) });
        /// </code>
        /// </example>
        /// <param name="serializableObject">Serializable object to be saved to file.</param>
        /// <param name="path">Path of the file to save the object to.</param>
        /// <param name="extraTypes">Extra data types to enable serialization of custom types within the object.</param>
        public static void Save(T serializableObject, string path, Type[] extraTypes)
        {
            SaveToDocumentFormat(serializableObject, extraTypes, path, null);
        }

        /// <summary>
        ///     Saves an object to an XML file in Document format, located in a specified isolated storage area.
        /// </summary>
        /// <example>
        ///     <code>        
        /// SerializableObject serializableObject = new SerializableObject();
        /// 
        /// ObjectXMLSerializer&lt;SerializableObject&gt;.Save(serializableObject, "XMLObjects.xml", IsolatedStorageFile.GetUserStoreForAssembly());
        /// </code>
        /// </example>
        /// <param name="serializableObject">Serializable object to be saved to file.</param>
        /// <param name="fileName">Name of the file in the isolated storage area to save the object to.</param>
        /// <param name="isolatedStorageDirectory">Isolated storage area directory containing the XML file to save the object to.</param>
        public static void Save(T serializableObject, string fileName, IsolatedStorageFile isolatedStorageDirectory)
        {
            SaveToDocumentFormat(serializableObject, null, fileName, isolatedStorageDirectory);
        }

        /// <summary>
        ///     Saves an object to an XML file located in a specified isolated storage area, using a specified serialized format.
        /// </summary>
        /// <example>
        ///     <code>        
        /// SerializableObject serializableObject = new SerializableObject();
        /// 
        /// ObjectXMLSerializer&lt;SerializableObject&gt;.Save(serializableObject, "XMLObjects.xml", IsolatedStorageFile.GetUserStoreForAssembly(), SerializedFormat.Binary);
        /// </code>
        /// </example>
        /// <param name="serializableObject">Serializable object to be saved to file.</param>
        /// <param name="fileName">Name of the file in the isolated storage area to save the object to.</param>
        /// <param name="isolatedStorageDirectory">Isolated storage area directory containing the XML file to save the object to.</param>
        /// <param name="serializedFormat">XML serialized format used to save the object.</param>
        public static void Save(T serializableObject, string fileName, IsolatedStorageFile isolatedStorageDirectory,
            SerializedFormat serializedFormat)
        {
            switch (serializedFormat)
            {
                case SerializedFormat.Binary:
                    SaveToBinaryFormat(serializableObject, fileName, isolatedStorageDirectory);
                    break;

                case SerializedFormat.Document:
                default:
                    SaveToDocumentFormat(serializableObject, null, fileName, isolatedStorageDirectory);
                    break;
            }
        }

        /// <summary>
        ///     Saves an object to an XML file in Document format, located in a specified isolated storage area, and supplying
        ///     extra data types to enable serialization of custom types within the object.
        /// </summary>
        /// <example>
        ///     <code>
        /// SerializableObject serializableObject = new SerializableObject();
        /// 
        /// ObjectXMLSerializer&lt;SerializableObject&gt;.Save(serializableObject, "XMLObjects.xml", IsolatedStorageFile.GetUserStoreForAssembly(), new Type[] { typeof(MyCustomType) });
        /// </code>
        /// </example>
        /// <param name="serializableObject">Serializable object to be saved to file.</param>
        /// <param name="fileName">Name of the file in the isolated storage area to save the object to.</param>
        /// <param name="isolatedStorageDirectory">Isolated storage area directory containing the XML file to save the object to.</param>
        /// <param name="extraTypes">Extra data types to enable serialization of custom types within the object.</param>
        public static void Save(T serializableObject, string fileName, IsolatedStorageFile isolatedStorageDirectory,
            Type[] extraTypes)
        {
            SaveToDocumentFormat(serializableObject, null, fileName, isolatedStorageDirectory);
        }

        #endregion

        #region Private

        private static FileStream CreateFileStream(IsolatedStorageFile isolatedStorageFolder, string path)
        {
            FileStream fileStream = null;

            if (isolatedStorageFolder == null)
                fileStream = new FileStream(path, FileMode.OpenOrCreate);
            else
                fileStream = new IsolatedStorageFileStream(path, FileMode.OpenOrCreate, isolatedStorageFolder);

            return fileStream;
        }

        private static T LoadFromBinaryFormat(string path, IsolatedStorageFile isolatedStorageFolder)
        {
            T serializableObject = null;

            using (var fileStream = new IsolatedStorageFileStream(path, FileMode.Open, isolatedStorageFolder))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                serializableObject = (T)serializer.ReadObject(fileStream);
            }

            return serializableObject;
        }

 

        private static T LoadFromDocumentFormat(Type[] extraTypes, string path,
            IsolatedStorageFile isolatedStorageFolder)
        {
            T serializableObject = null;

            using (var textReader = CreateTextReader(isolatedStorageFolder, path))
            {
                var xmlSerializer = CreateXmlSerializer(extraTypes);
                serializableObject = xmlSerializer.Deserialize(textReader) as T;
            }

            return serializableObject;
        }

        private static TextReader CreateTextReader(IsolatedStorageFile isolatedStorageFolder, string path)
        {
            TextReader textReader = null;

            if (isolatedStorageFolder == null)
                textReader = new StreamReader(path);
            else
                textReader = new StreamReader(new IsolatedStorageFileStream(path, FileMode.Open, isolatedStorageFolder));

            return textReader;
        }

        private static TextWriter CreateTextWriter(IsolatedStorageFile isolatedStorageFolder, string path)
        {
            TextWriter textWriter = null;

            if (isolatedStorageFolder == null)
                textWriter = new StreamWriter(path);
            else
                textWriter =
                    new StreamWriter(new IsolatedStorageFileStream(path, FileMode.OpenOrCreate, isolatedStorageFolder));

            return textWriter;
        }

        private static XmlSerializer CreateXmlSerializer(Type[] extraTypes)
        {
            var ObjectType = typeof (T);

            XmlSerializer xmlSerializer = null;

            if (extraTypes != null)
                xmlSerializer = new XmlSerializer(ObjectType, extraTypes);
            else
                xmlSerializer = new XmlSerializer(ObjectType);

            return xmlSerializer;
        }

        private static void SaveToDocumentFormat(T serializableObject, Type[] extraTypes, string path,
            IsolatedStorageFile isolatedStorageFolder)
        {
            using (var textWriter = CreateTextWriter(isolatedStorageFolder, path))
            {
                var xmlSerializer = CreateXmlSerializer(extraTypes);
                xmlSerializer.Serialize(textWriter, serializableObject);
            }
        }

        private static void SaveToBinaryFormat(T serializableObject, string path,
           IsolatedStorageFile isolatedStorageFolder)
        {
            using (var fileStream = new IsolatedStorageFileStream(path, FileMode.Create, isolatedStorageFolder))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                serializer.WriteObject(fileStream, serializableObject);
            }
        }


        private static string RemoveXmlNilNodes(string xml)
        {
            XmlNameTable nt = new NameTable();
            var xdoc = new XmlDocument(nt);
            var ns = new XmlNamespaceManager(nt);
            ns.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");

            xdoc.Load(new StringReader(xml));
            var nodesToRemove = xdoc.SelectNodes("//*[@xsi:nil=\"true\"]", ns);
            foreach (XmlNode node in nodesToRemove)
            {
                node.ParentNode.RemoveChild(node);
            }

            return xdoc.OuterXml;
        }

        #endregion
    }
}