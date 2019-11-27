Imports DevExpress.Utils.Svg
Imports DevExpress.Xpf.Core.Native
Imports System
Imports System.Globalization
Imports System.IO
Imports System.Windows
Imports System.Windows.Data
Imports System.Windows.Markup

Namespace WpfApplication2
	Public Class ItemToBitmapConverter
		Inherits MarkupExtension
		Implements IValueConverter

		Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
			Dim path As String = "..\..\Images\" & value.ToString() & ".svg"
			Dim svgStream = File.OpenRead(path)
			If svgStream Is Nothing Then
				Return Nothing
			End If
			Dim svgImage = SvgLoader.LoadFromStream(svgStream)
			Dim size = New Size(svgImage.Width * ScreenHelper.ScaleX, svgImage.Height * ScreenHelper.ScaleX)
			Return WpfSvgRenderer.CreateImageSource(svgImage, size, Nothing, Nothing, True)
		End Function
		Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
			Throw New NotImplementedException()
		End Function
		Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
			Return Me
		End Function
	End Class
End Namespace