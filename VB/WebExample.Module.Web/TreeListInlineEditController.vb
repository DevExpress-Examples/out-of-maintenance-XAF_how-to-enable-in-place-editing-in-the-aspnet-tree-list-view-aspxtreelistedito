Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Text

Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.Base.General
Imports DevExpress.ExpressApp.TreeListEditors.Web
Imports DevExpress.Web.ASPxTreeList
Imports DevExpress.ExpressApp.Web.Editors.ASPx
Imports DevExpress.ExpressApp.Web.Editors
Imports System.Web.UI.WebControls
Imports DevExpress.ExpressApp.Web

Namespace WebExample.Module.Web
	Public Class TreeListInlineEditController
		Inherits ViewController(Of ListView)
		Public Sub New()
			TargetObjectType = GetType(ITreeNode)
		End Sub
		Protected Overrides Sub OnViewControlsCreated()
			MyBase.OnViewControlsCreated()
			Dim editor As ASPxTreeListEditor = TryCast(View.Editor, ASPxTreeListEditor)
			If editor IsNot Nothing AndAlso View.Model.AllowEdit Then
				editor.TreeList.SettingsEditing.Mode = DevExpress.Web.ASPxTreeList.TreeListEditMode.Inline
				AddHandler editor.TreeList.NodeUpdating, AddressOf TreeList_NodeUpdating
				AddHandler editor.TreeList.CommandColumnButtonInitialize, AddressOf TreeList_CommandColumnButtonInitialize
				For Each column As TreeListColumn In editor.TreeList.Columns
					If TypeOf column Is TreeListDataColumn AndAlso (CType(column, TreeListDataColumn)).EditCellTemplate IsNot Nothing Then
						Dim propertyEditor As WebPropertyEditor = (CType((CType(column, TreeListDataColumn)).EditCellTemplate, EditModeDataItemTemplate)).PropertyEditor
						AddHandler propertyEditor.ControlCreated, AddressOf propertyEditor_ControlCreated
					End If
				Next column
			End If
		End Sub

		Private Sub TreeList_NodeUpdating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs)
			ObjectSpace.CommitChanges()
		End Sub

		Private Sub TreeList_CommandColumnButtonInitialize(ByVal sender As Object, ByVal e As TreeListCommandColumnButtonEventArgs)
			If e.ButtonType = TreeListCommandColumnButtonType.Edit Then
				e.Visible = DevExpress.Utils.DefaultBoolean.True
				e.CommandColumn.ButtonType = ButtonType.Link
				e.CommandColumn.EditButton.Text = "Inline Edit"
			ElseIf e.ButtonType = TreeListCommandColumnButtonType.Update OrElse e.ButtonType = TreeListCommandColumnButtonType.Cancel Then
				e.Visible = DevExpress.Utils.DefaultBoolean.True
			End If
		End Sub

		Private Sub propertyEditor_ControlCreated(ByVal sender As Object, ByVal e As EventArgs)
			Dim control As WebControl = TryCast((CType(sender, ASPxPropertyEditor)).Editor, WebControl)
			If control IsNot Nothing Then
				control.Attributes("onclick") = RenderHelper.EventCancelBubbleCommand
			End If
		End Sub
	End Class
End Namespace
