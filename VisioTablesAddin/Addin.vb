﻿Imports System.Drawing
Imports System.Windows.Forms


Partial Public Class Addin
    Public Property Application As Microsoft.Office.Interop.Visio.Application

    Public Sub OnAction(commandId As String, commandTag As String)

        Select Case commandId
            Case "btn_newtable" : CreatingTable.Load_dlgNewTable() : Return
            Case "btn_dropdownlist" : LoaddlgSelectFromList() : Return
            Case "btn_lockpicture" : LoaddlgPicture() : Return
            Case "btn_help" : CallHelp() : Return
        End Select

        If Not CheckSelCells() Then Exit Sub

        Select Case commandId
            Case "btn_newcolumnbefore", "btn_newcolumnafter" : AddColumns(commandTag)
            Case "btn_newrowbefore", "btn_newrowafter" : AddRows(commandTag)
            Case "btn_onwidth" : AllAlignOnText(True, False, 0, 0, True, True)
            Case "btn_onheight" : AllAlignOnText(False, True, 0, 0, True, True)
            Case "btn_onwidthheight" : AllAlignOnText(True, True, 0, 0, False, False)
            Case "btn_seltable", "btn_selrange", "btn_selcolumn", "btn_selrow" : SelCell(commandTag)
            Case "btn_seltxt", "btn_selnum", "btn_selnotnum", "btn_seldate", "btn_selempty", "btn_selnotempty", "btn_selinvert" : SelInContent(commandTag)
            Case "btn_text", "btn_date", "btn_time", "btn_comment", "btn_numcol", "btn_numrow" : InsertText(commandTag)
            Case "btn_intdeint" : IntDeIntCells()
            Case "btn_gut" : GutT()
            Case "btn_copy" : CopyT()
            Case "btn_paste" : PasteT()
            Case "btn_delcolumn" : DelColRows(0)
            Case "btn_delrow" : DelColRows(1)
            Case "btn_deltable" : DelTab()
            Case "btn_intellinput" : MessageBox.Show(commandId) ': LoadfrmIntellInput
            Case "btn_sizeonwidth", "btn_sizeonheight" : AlignOnSize(commandTag)
            Case "btn_size" : LoaddlgTableSize(0)
            Case "btn_autosize" : LoaddlgTableSize(1)
            Case "btn_fromfile" : LoaddlgFromFile()
            Case "btn_altlinesrow", "btn_altlinescol" : AlternatLines(commandTag)
            Case "btn_extdata" : LoaddlgLinkData()
            Case "btn_rotatetext" : AllRotateText()
            Case "btn_convert1Shape" : ConvertInto1Shape()
        End Select
    End Sub

#Region "RIBBONFUNCTIONS"

    Public Function IsCommandAltEnabled(commandId As String) As Boolean
        Return Application IsNot Nothing AndAlso Application.ActiveWindow IsNot Nothing
    End Function

    Public Function IsCommandEnabled(commandId As String) As Boolean
        Return Application IsNot Nothing AndAlso Application.ActiveWindow IsNot Nothing
        'If Application.ActiveWindow.Selection.Count > 0 AndAlso _
        '    Application.ActiveWindow.Selection(1).CellExistsU("User.TableName", 0) Then Return True
        'Return False
    End Function

    Sub Startup(app As Object)
        Application = DirectCast(app, Microsoft.Office.Interop.Visio.Application)
        System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(False)
        'AddHandler Application.SelectionChanged, AddressOf Application_SelectionChanged
        AddHandler Application.DocumentCreated, AddressOf Application_DocumentListChanged
        AddHandler Application.DocumentOpened, AddressOf Application_DocumentListChanged
        AddHandler Application.BeforeDocumentClose, AddressOf Application_DocumentListChanged
    End Sub

    Private Sub Application_DocumentListChanged(ByVal doc As Microsoft.Office.Interop.Visio.Document)
        UpdateRibbon()
    End Sub

    'Private Sub Application_SelectionChanged(ByVal window As Microsoft.Office.Interop.Visio.Window)
    '    UpdateRibbon()
    'End Sub

#End Region

End Class