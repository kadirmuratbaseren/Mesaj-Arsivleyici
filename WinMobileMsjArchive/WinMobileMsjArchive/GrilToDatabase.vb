#Region "Imports"
Imports System
Imports System.Data
Imports System.Data.SqlClient
#End Region

Public Class GrilToDatabase

#Region "Local Variables"
    Private _UserName As String
    Private _Password As String
    Private _Password2 As String
    Private _Conn As SqlConnection
    Private _Cmd As SqlCommand
#End Region

#Region "Properties"
    Public Property Conn() As SqlConnection
        Get
            Return _Conn
        End Get
        Set(ByVal value As SqlConnection)
            _Conn = value
        End Set
    End Property
    Public Property Cmd() As SqlCommand
        Get
            Return _Cmd
        End Get
        Set(ByVal value As SqlCommand)
            _Cmd = value
        End Set
    End Property
#End Region

#Region "Methods"
    'UserName ve Password kontrolu..
    Public Function LoginPasswordControl(ByVal UserName As String, ByVal Password As String) As Integer
        Dim result As Integer = 0
        'Sorgu olusturulur..
        _Cmd.CommandText = "SELECT logi_ID,logi_Name,logi_Password FROM Login WHERE logi_Name='" & UserName & "'"
        'Ba�lant� a��k de�ilse,a��l�r..
        If Not Conn.State = ConnectionState.Open Then
            Conn.Open()
        End If
        'DataReader tan�mlan�r ve sorgu sonucu okunur..
        Dim DR As SqlDataReader
        DR = Cmd.ExecuteReader

        Do While DR.Read
            'Eger UserName ve Password do�ru ise result=UserID yap�l�r ve d�ng�den ��k�l�r..
            If DR.Item(1).ToString = UserName AndAlso DR.Item(2).ToString = Password Then
                result = DR.Item(0)
                Exit Do
            End If
        Loop
        'Ba�lant� kapat�l�r..
        If Not Conn.State = ConnectionState.Closed Then
            Conn.Close()
        End If
        'Sonu� bize d�nd�r�l�r..
        Return result
    End Function

    'O An sisteme ba�l� User'�n UserID de�erini al�p o user'�n Password2 de�erini sonduren function.
    Public Function SecondLevelPasswordControl() As String
        Dim result As String = ""
        'Sorgu olusturulur..
        _Cmd.CommandText = "SELECT logi_Password2 FROM Login WHERE logi_ID=" & Module1.UserID
        'Ba�lant� a��k de�ilse,a��l�r..
        If Not Conn.State = ConnectionState.Open Then
            Conn.Open()
        End If
        'DataReader tan�mlan�r ve sorgu sonucu okunur..
        Dim DR As SqlDataReader
        DR = Cmd.ExecuteReader

        Do While DR.Read
            'UserID kullan�larak Password2 elde edilir..
            result = DR.Item(0).ToString
        Loop
        'Ba�lant� kapat�l�r..
        If Not Conn.State = ConnectionState.Closed Then
            Conn.Close()
        End If
        'Sonu� bize d�nd�r�l�r..
        Return result
    End Function

    'Database'den Msjlar� alan function..
    Public Function ReadToMsj(ByVal Command As String) As DataTable
        Dim DT As DataTable = New DataTable
        Cmd.CommandText = Command
        Dim DA As SqlDataAdapter = New SqlDataAdapter(Cmd)
        DA.Fill(DT)
        Return DT
    End Function

    'Database Insert,Update,Delete sorgusu i�in function..
    Public Function DataInsUpdDel(ByVal Command As String) As Integer
        Dim result As Integer = 0
        'Gelen CommandText SqlCommand a atan�r..
        Cmd.CommandText = Command
        'Ba�lant� a��k de�ilse a��l�r..
        If Not Conn.State = ConnectionState.Open Then
            Conn.Open()
        End If
        'Insert,Update,Delete i�lemi ger�ekle�tirilir..
        result = Cmd.ExecuteNonQuery
        Conn.Close()

        'i�lemin ger�ekle�ip ger�ekle�medi�i sonucu d�ndururlur..
        Return result
    End Function
#End Region

#Region "Events"
    Public Sub New()
        _Conn = New SqlConnection("Server=" & My.Computer.Name & ";Database=MobileMsjArchive;Integrated Security=SSPI;")
        _Cmd = New SqlCommand
        _Cmd.Connection = Conn
    End Sub

    Public Sub New(ByVal ChangeDatabase As String)
        _Conn = New SqlConnection("Server=" & My.Computer.Name & ";Database=" & ChangeDatabase & ";Integrated Security=SSPI;")
        _Cmd = New SqlCommand
        _Cmd.Connection = Conn
    End Sub
#End Region
End Class
