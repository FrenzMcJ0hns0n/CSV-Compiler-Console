Imports System.Text.RegularExpressions

Module CsvHelper

    ''' <summary>
    ''' Return full paths of CSV files found in current working directory
    ''' </summary>
    Public Function GetCsvFiles() As List(Of String)

        Dim csv_files As List(Of String) = New List(Of String)

        For Each file As String In IO.Directory.GetFiles(Environment.CurrentDirectory)
            With New IO.FileInfo(file)
                If .Name = "compiled.csv" Then Continue For
                If .Extension.ToLowerInvariant = ".csv" Then csv_files.Add(.FullName)
            End With
        Next

        Return csv_files

    End Function

    ''' <summary>
    ''' Copy CSV input data into 3 nested lists
    ''' </summary>
    Public Function GetAllContents(csv_files As List(Of String), csv_sep As Char) As List(Of List(Of List(Of String)))

        Dim data As List(Of List(Of List(Of String))) = New List(Of List(Of List(Of String)))

        For Each file In csv_files
            Dim lines As List(Of List(Of String)) = New List(Of List(Of String))
            For Each line As String In IO.File.ReadAllLines(file)
                lines.Add(line.Split(csv_sep).ToList) 'Add all values processed from a line
            Next
            data.Add(lines) 'Add all lines processed from a file
        Next

        Return data

    End Function

    ''' <summary>
    ''' Gather headers of all CSV files
    ''' </summary>
    Public Function GetAllHeaders(csv_data As List(Of List(Of List(Of String)))) As List(Of List(Of String))

        Dim headers = New List(Of List(Of String))

        For Each file In csv_data
            headers.Add(file(0))
        Next

        Return headers

    End Function

    ''' <summary>
    ''' Ensure that CSV headers are the same
    ''' </summary>
    Public Function ValidateHeaders(headers As List(Of List(Of String))) As Boolean

        If headers.Count = 1 Then Return True

        For i = 0 To headers.Count - 2
            Dim hdr1 As String = GetHeader(headers, i, "|") 'Header at index i
            Dim hdr2 As String = GetHeader(headers, i + 1, "|") 'Header at index i+1
            If Not hdr1 = hdr2 Then Return False 'Error : headers are not identical
        Next

        Return True

    End Function

    ''' <summary>
    ''' Return CSV header as String
    ''' </summary>
    Public Function GetHeader(headers As List(Of List(Of String)), index As Integer, csv_sep As Char) As String

        Return String.Join(csv_sep, headers(index))

    End Function

    ''' <summary>
    ''' Remove all CSV headers except the first
    ''' </summary>
    Public Function RemoveExtraHeaders(data As List(Of List(Of List(Of String)))) As List(Of List(Of List(Of String)))

        For file = 0 To data.Count - 1 'Process each file
            For line = 0 To data(file).Count - 1 'Process each line
                If file >= 1 And line = 0 Then data(file).Remove(data(file)(line)) 'Keep only headers from 1st file
            Next
        Next

        Return data

    End Function

    ''' <summary>
    ''' Process each line of the CSV input data
    ''' </summary>
    Public Function ProcessLine(input_values As List(Of String), csv_sep As Char) As String

        Dim output_values As List(Of String) = New List(Of String)

        For Each value As String In input_values
            output_values.Add(ProcessValue(value))
        Next

        Return String.Join(csv_sep, output_values)

    End Function

    ''' <summary>
    ''' Perform operations on values if necessary
    ''' </summary>
    Public Function ProcessValue(input_value As String) As String

        'TODO: Add examples...
        Dim output_value As String = input_value

        Return output_value

    End Function

    ''' <summary>
    ''' Build the content to be written into destination file
    ''' </summary>
    Function BuildCsvOutput(data As List(Of List(Of List(Of String))), csv_sep As Char) As String

        Dim output As String = Nothing

        For Each file In data
            For Each line In file
                output &= ProcessLine(line, csv_sep) & Environment.NewLine
            Next
        Next

        Return output

    End Function


End Module
