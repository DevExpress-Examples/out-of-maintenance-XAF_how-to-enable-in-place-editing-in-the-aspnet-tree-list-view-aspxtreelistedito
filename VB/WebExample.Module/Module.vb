Imports System
Imports System.Collections.Generic

Imports DevExpress.ExpressApp
Imports System.Reflection


Namespace WebExample.Module
    Public NotInheritable Partial Class WebExampleModule
        Inherits ModuleBase

        Public Sub New()
            ModelDifferenceResourceName = "WebExample.Module.Model.DesignedDiffs"
            InitializeComponent()
        End Sub
    End Class
End Namespace
