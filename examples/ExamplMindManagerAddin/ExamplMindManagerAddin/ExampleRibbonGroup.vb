Imports System
Imports System.Collections.Generic
Imports System.Text
Imports mm = Mindjet.MindManager.Interop
Imports System.Text.RegularExpressions
Imports Microsoft.Win32

Namespace MindManagerRibbon

    Class ExampleRibbonGroup

        Implements IDisposable

#Region "Variables"

        Private m_app As mm.Application
        Private WithEvents myCommand As mm.Command

#End Region

        Public Sub New(ByVal app As Mindjet.MindManager.Interop.Application)

            Try

                m_app = app

                'Creates the Ribbon
                Dim newRibbontab As Mindjet.MindManager.Interop.ribbonTab = Ribbons.CreateRibbon(m_app, "Example Ribbon", "urn:EG.Tab")

                'Creates the Ribbon Group
                Dim newribbongroup As Mindjet.MindManager.Interop.RibbonGroup = Ribbons.CreateGroupTab(newRibbontab, "Example Group", "urn:EG.Group")

                'Creates the Ribbon Group Command
                myCommand = m_app.Commands.Add("ExamplMindManagerAddin.Connect", "ExampleCommand")

                'Change "Environ("ProgramFiles") & "\OPTi-Suite\Common Files\TopicTextLock.png" to target your own url
                '***
                myCommand.ImagePath = Environ("ProgramFiles") & "\OPTi-Suite\Common Files\TopicTextLock.png"
                '***

                myCommand.BasicCommand = True
                myCommand.ToolTip = ("Example ToolTip Text" + (Chr(10)) + "Example Caption")
                myCommand.Caption = "Example Caption"
                newribbongroup.GroupControls.AddButton(myCommand, 0)

            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try

        End Sub

        Sub tLock_UpdateState(ByRef pEnabled As Boolean, ByRef pChecked As Boolean) Handles myCommand.UpdateState

            Try

                pChecked = False
                pEnabled = True

            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try

        End Sub

        Sub tLock_Click() Handles myCommand.Click

            Try

                MsgBox("Now I can write some more code to do something with")

            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try

        End Sub

        Public Overloads Sub Dispose() Implements IDisposable.Dispose

            System.Runtime.InteropServices.Marshal.ReleaseComObject(myCommand)
            System.Runtime.InteropServices.Marshal.ReleaseComObject(m_app)

        End Sub

    End Class

End Namespace


