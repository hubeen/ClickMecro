Imports System
Imports System.Runtime.InteropServices



Public Class Form1
    <DllImport("user32")>
    Public Shared Function GetCursorPos(ByRef pt As POINT) As Int32
    End Function
    <DllImport("user32")>
    Public Shared Function SetCursorPos(x As Int32, y As Int32) As Int32
    End Function
    Public Structure POINT
        Public x As Int32
        Public y As Int32
    End Structure

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub


    Dim pt As POINT
    Dim tDate As Date
    Dim strings As String
    Dim strings0 As String

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        tDate = Now()
        GetCursorPos(pt)
        strings = Format(tDate, "yyyy-MM-dd-HH-mm-ss")

        Label2.Text = strings
        Label1.Text = "x: " & pt.x & vbCrLf & "y: " & pt.y
        If String.Equals(strings, TextBox3.Text) Then
            Dim vOut1 As Int32 = Convert.ToInt16(TextBox1.Text)
            Dim vOut2 As Int32 = Convert.ToInt16(TextBox2.Text)
            MouseMove(vOut1, vOut2)
            MouseLeftClick()
        End If

    End Sub
    Public Shared Sub MouseMove(ByVal x As Integer, ByVal y As Integer)
        Dim fx = x * (65535.0# / (GetSystemMetrics(SystemMetric.SM_CXSCREEN) - 1))
        Dim fy = y * (65535.0# / (GetSystemMetrics(SystemMetric.SM_CYSCREEN) - 1))
        Dim it As New INPUT
        it.dwType = INPUT_MOUSE
        it.mkhi.mi.dwFlags = MOUSEEVENTF_MOVE Or MOUSEEVENTF_ABSOLUTE
        it.mkhi.mi.dx = fx
        it.mkhi.mi.dy = fy
        SendInput(1, it, Marshal.SizeOf(it))
    End Sub

    Public Shared Sub MouseLeftClick()
        Dim it As New INPUT
        it.dwType = INPUT_MOUSE
        it.mkhi.mi.dwFlags = MOUSEEVENTF_LEFTDOWN
        SendInput(1, it, Marshal.SizeOf(it))
        Application.DoEvents()
        it = New INPUT
        it.dwType = INPUT_MOUSE
        it.mkhi.mi.dwFlags = MOUSEEVENTF_LEFTUP
        SendInput(1, it, Marshal.SizeOf(it))
    End Sub

    Private Structure INPUT
        Dim dwType As Integer
        Dim mkhi As MOUSEKEYBDHARDWAREINPUT
    End Structure

    <StructLayout(LayoutKind.Explicit)>
    Private Structure MOUSEKEYBDHARDWAREINPUT
        <FieldOffset(0)> Public mi As MOUSEINPUT
        <FieldOffset(0)> Public ki As KEYBDINPUT
        <FieldOffset(0)> Public hi As HARDWAREINPUT
    End Structure

    Private Structure MOUSEINPUT
        Public dx As Integer
        Public dy As Integer
        Public mouseData As Integer
        Public dwFlags As Integer
        Public time As Integer
        Public dwExtraInfo As Integer
    End Structure

    Private Structure KEYBDINPUT
        Public wVk As Short
        Public wScan As Short
        Public dwFlags As Integer
        Public time As Integer
        Public dwExtraInfo As Integer
    End Structure

    Private Structure HARDWAREINPUT
        Public uMsg As Integer
        Public wParamL As Short
        Public wParamH As Short
    End Structure

    Const INPUT_MOUSE As UInt32 = 0
    Const INPUT_KEYBOARD As Integer = 1
    Const INPUT_HARDWARE As Integer = 2
    Const XBUTTON1 As UInt32 = &H1
    Const XBUTTON2 As UInt32 = &H2
    Const MOUSEEVENTF_MOVE As UInt32 = &H1
    Const MOUSEEVENTF_LEFTDOWN As UInt32 = &H2
    Const MOUSEEVENTF_LEFTUP As UInt32 = &H4
    Const MOUSEEVENTF_RIGHTDOWN As UInt32 = &H8
    Const MOUSEEVENTF_RIGHTUP As UInt32 = &H10
    Const MOUSEEVENTF_MIDDLEDOWN As UInt32 = &H20
    Const MOUSEEVENTF_MIDDLEUP As UInt32 = &H40
    Const MOUSEEVENTF_XDOWN As UInt32 = &H80
    Const MOUSEEVENTF_XUP As UInt32 = &H100
    Const MOUSEEVENTF_WHEEL As UInt32 = &H800
    Const MOUSEEVENTF_VIRTUALDESK As UInt32 = &H4000
    Const MOUSEEVENTF_ABSOLUTE As UInt32 = &H8000

    <DllImport("user32.dll")> Private Shared Function GetSystemMetrics(ByVal smIndex As Integer) As Integer
    End Function
    <DllImport("user32.dll")> Private Shared Function SendInput(ByVal nInputs As Integer, ByRef pInputs As INPUT, ByVal cbSize As Integer) As Integer
    End Function

    Public Enum SystemMetric As Integer
        ''' <summary>
        '''  Width of the screen of the primary display monitor in pixels. This is the same values obtained by calling GetDeviceCaps as follows: GetDeviceCaps( hdcPrimaryMonitor HORZRES).
        ''' </summary>
        SM_CXSCREEN = 0
        ''' <summary>
        ''' Height of the screen of the primary display monitor in pixels. This is the same values obtained by calling GetDeviceCaps as follows: GetDeviceCaps( hdcPrimaryMonitor VERTRES).
        ''' </summary>
        SM_CYSCREEN = 1
    End Enum
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Timer1.Enabled = True
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Timer1.Enabled = False
    End Sub
End Class
