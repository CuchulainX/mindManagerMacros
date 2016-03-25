Imports System
Imports System.Collections.Generic
Imports System.Text
Imports mm = Mindjet.MindManager.Interop

Public Class Ribbons

    Public Shared Function CreateRibbon(ByVal m_app As mm.Application, ByVal ribbonDisplayName As String, ByVal ribbonURI As String) As mm.ribbonTab

        Dim rb As mm.RibbonTabs
        rb = m_app.Ribbon.Tabs
        Dim addinRibbonTab As mm.ribbonTab = Nothing
        Dim found As Boolean = False
        For Each atab As mm.ribbonTab In rb
            If atab.Uri = ribbonURI Then
                found = True
                addinRibbonTab = atab
                ' Break
            End If
        Next
        If found Then
            Return addinRibbonTab
        Else
            addinRibbonTab = rb.Add(rb.Count, ribbonDisplayName, ribbonURI)
        End If
        Return addinRibbonTab

    End Function

    Public Shared Function CreateGroupTab(ByVal ribbontab As mm.ribbonTab, ByVal Displayname As String, ByVal URI As String) As mm.RibbonGroup

        For Each aRg As mm.RibbonGroup In ribbontab.Groups
            If aRg.Uri = URI Then
                Return aRg
            End If
        Next
        Return ribbontab.Groups.Add(ribbontab.Groups.Count, Displayname, URI, String.Empty)

    End Function

End Class



