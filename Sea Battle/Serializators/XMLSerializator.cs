/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 14.11.2020
 * Time: 19:50
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Xml.Serialization;

using Config;

using Entities;

namespace Serializators
{
	/// <summary>
	/// Description of XMLSerializator.
	/// </summary>
	public class XMLSerializator: Serializator<SerializableField>
	{
		private XmlSerializer serializer;
		private string filepath;
		private string filename;
		
		public XMLSerializator(string filename)
		{
			this.filename = filename;
			this.filepath = SolutionConfig.FIELDS_DIRECTORY + filename + ".xml";
			serializer = new XmlSerializer(typeof(SerializableField));
		}
		
		public void Serialize(SerializableField obj)
		{
			var xmlFieldList = new XMLFieldListSerializator();
			var fieldList = xmlFieldList.Deserialize();
			fieldList.List.Add(filename);
			xmlFieldList.Serialize(fieldList);
			using (FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate))
			{
				serializer.Serialize(fs, obj);
			}
		}
		
		public SerializableField Deserialize()
		{
			SerializableField field;
			if (!File.Exists(filepath))
				return null;
			using (FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate))
			{
				field = (SerializableField)serializer.Deserialize(fs);
			}
			return field;
		}
		
		public void Remove()
		{
			var xmlFieldNames = new XMLFieldListSerializator();
			var fieldNames = xmlFieldNames.Deserialize();
			if (!fieldNames.List.Contains(filename))
				return;
			File.Delete(filepath);
			fieldNames.List.Remove(filename);
			xmlFieldNames.Serialize(fieldNames);
		}
		
	}
}
