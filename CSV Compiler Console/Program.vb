Module Program

    Sub Main()

        ' -----
        ' Debug
        ' -----
        'Dim target_file = ReturnDestinationFilePath()
        'If IO.File.Exists(target_file) Then IO.File.Delete(target_file)


        ' --------
        ' Settings
        ' --------
        ' TODO? Load from external file
        Const CSV_SEPARATOR As Char = "|"


        ' -----------------
        ' Manage input data
        ' -----------------
        Dim input_files As List(Of String) = GetCsvFiles()

        Dim input_filepaths As String = Nothing
        For Each filepath In input_files
            input_filepaths &= "- " & filepath & Environment.NewLine
        Next
        Console.WriteLine("Input files :")
        Console.WriteLine(input_filepaths)

        If input_files.Count <= 1 Then
            Console.WriteLine($"Data error : {input_files.Count} CSV file in current directory. Minimum files required = 2")
            Console.WriteLine("Press any key to exit ...")
            Console.ReadKey()
            Environment.Exit(0)
        End If

        Console.WriteLine($"Processing {input_files.Count} CSV files. Please wait ...{Environment.NewLine}")
        Dim input_data As List(Of List(Of List(Of String))) = GetAllContents(input_files, CSV_SEPARATOR)


        ' --------------
        ' Handle headers
        ' --------------
        Dim csv_headers As List(Of List(Of String)) = GetAllHeaders(input_data)

        If csv_headers.Count > 1 Then
            If Not ValidateHeaders(csv_headers) Then
                Console.WriteLine("Data error : Not all CSV files headers are the same")
                Console.WriteLine("Press any key to exit ...")
                Console.ReadKey()
                Environment.Exit(0)
            End If
            input_data = RemoveExtraHeaders(input_data)
        End If


        ' ------------------
        ' Manage output data
        ' ------------------
        Dim output_file As String = ReturnOutputFilepath()
        Dim output_data As String = BuildCsvOutput(input_data, CSV_SEPARATOR)

        IO.File.WriteAllText(output_file, output_data)
        Environment.Exit(0)

    End Sub

End Module
