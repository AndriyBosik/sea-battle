﻿/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 12.11.2020
 * Time: 12:23
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using Serializators;

namespace SeaBattle
{
	/// <summary>
	/// Interaction logic for GameLoader.xaml
	/// </summary>
	public partial class FieldLoader : Window
	{
		public string Filename
		{ get; private set; }
		
		public FieldLoader()
		{
			InitializeComponent();
			RefreshListBox();
		}
		
		private void RefreshListBox()
		{
			var xmlFieldNames = new XMLFieldListSerializator();
			var fieldList = xmlFieldNames.Deserialize();
			lbFieldNames.ItemsSource = fieldList.Descriptions;
		}
		
		private void LoadField(object sender, RoutedEventArgs e)
		{
			Filename = ((FieldList.FieldDescription)lbFieldNames.SelectedItem).Name;
			DialogResult = true;
			this.Close();
		}
		
		private void RemoveLoad(object sender, RoutedEventArgs e)
		{
			var filename = ((FieldList.FieldDescription)lbFieldNames.SelectedItem).Name;
			var xmlField = new XMLSerializator(filename);
			xmlField.Remove();
			RefreshListBox();
		}
		
		private void RemoveAll(object sender, EventArgs e)
		{
			var xmlFieldList = new XMLFieldListSerializator();
			xmlFieldList.Remove();
			RefreshListBox();
		}
		
		private void CancelLoading(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
			this.Close();
		}
	}
}