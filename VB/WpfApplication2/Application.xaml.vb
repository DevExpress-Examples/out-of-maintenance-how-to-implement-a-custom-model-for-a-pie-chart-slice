﻿Imports DevExpress.Xpf.Core
Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Data
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Windows

Namespace WpfApplication2
	''' <summary>
	''' Interaction logic for App.xaml
	''' </summary>
	Partial Public Class App
		Inherits Application

		Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)
			'ApplicationThemeHelper.ApplicationThemeName = Theme.Office2016WhiteName;
			MyBase.OnStartup(e)
		End Sub
	End Class
End Namespace
