Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Charts
Imports Newtonsoft.Json
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Media.Animation

Namespace WpfApplication2
	Partial Public Class MainWindow
		Inherits Window

		Public Class Item
			Public Property Name() As String
		End Class

'INSTANT VB NOTE: The field data was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Public data_Renamed As List(Of Item)
		Public ReadOnly Property Data() As List(Of Item)
			Get
				Return data_Renamed
			End Get
		End Property

		Public Sub New()
			InitializeComponent()
			chart.DataContext = Me
			Using r As New StreamReader("..\..\Data\votes.txt")
				Dim json As String = r.ReadToEnd()
				data_Renamed = JsonConvert.DeserializeObject(Of List(Of Item))(json)
			End Using
		End Sub
	End Class
End Namespace