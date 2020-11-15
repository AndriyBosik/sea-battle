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
			fieldList.Descriptions.Add(new FieldList.FieldDescription(filename, obj.Rows, obj.Columns));
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
			var fictitious = new FieldList.FieldDescription(filename, 0, 0);
			if (!fieldNames.Descriptions.Contains(fictitious))
				return;
			File.Delete(filepath);
			fieldNames.Descriptions.Remove(fictitious);
			xmlFieldNames.Serialize(fieldNames);
		}
		
	}
}
