/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 09/20/2020
 * Time: 14:52
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using Config;

using System;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Processors
{
	/// <summary>
	/// Description of ImageProcessor.
	/// </summary>
	public static class ImageProcessor
	{
		public static ImageBrush GetBackground(string name)
		{
			string path = SolutionConfig.IMAGES_DIRECTORY + name + ".png";
			BitmapImage btm = new BitmapImage();
			btm.BeginInit();
			btm.UriSource = new Uri(path, UriKind.Relative);
			btm.EndInit();
			return new ImageBrush(btm);
		}
		
		public static Image GetImage(string imageName, int width, int height)
		{
			Image im = new Image();
			BitmapImage btm = new BitmapImage();
			
			btm.BeginInit();
			btm.UriSource = new Uri(SolutionConfig.IMAGES_DIRECTORY + imageName + ".png");
			btm.EndInit();
			
			im.Height = height;
			im.Width = height;
			im.Source = btm;
			im.Stretch = Stretch.Fill;
			
			return im;		}
		
		public static BitmapFrame GetIcon(string name)
		{
			Uri iconUri = new Uri(SolutionConfig.IMAGES_DIRECTORY + name + ".ico");
			return BitmapFrame.Create(iconUri);
		}
	}
}
