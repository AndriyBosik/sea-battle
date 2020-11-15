/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 11/14/2020
 * Time: 20:56
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Xml.Serialization;
using Config;

namespace Serializators
{
	/// <summary>
	/// Description of XMLFieldListSerializator.
	/// </summary>
	public class XMLFieldListSerializator: Serializator<FieldList>
	{
		private XmlSerializer serializer;
		private string filepath;
		
		public XMLFieldListSerializator()
		{
			filepath = SolutionConfig.XML_FIELDS_LIST_FILENAME;
			serializer = new XmlSerializer(typeof(FieldList));
		}
		
		public void Serialize(FieldList obj)
		{
			File.Delete(filepath);
			using (FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate))
			{
				serializer.Serialize(fs, obj);
			}
		}
		
		public FieldList Deserialize()
		{
			FieldList fieldList;
			if (!File.Exists(filepath))
				Serialize(new FieldList
				          { Descriptions = new System.Collections.Generic.List<FieldList.FieldDescription>() });
			using (FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate))
			{
				fieldList = (FieldList)serializer.Deserialize(fs);
			}
			return fieldList;
		}
		
		public void Remove()
		{
			var fieldList = Deserialize();
			foreach (var description in fieldList.Descriptions)
			{
				var serializator = new XMLSerializator(description.Name);
				serializator.Remove();
			}
		}
		
	}
}
