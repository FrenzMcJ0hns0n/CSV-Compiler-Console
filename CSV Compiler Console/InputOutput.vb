Module InputOutput

    ''' <summary>
    ''' Build the path of the destination file
    ''' </summary>
    Public Function ReturnOutputFilepath() As String

        Return IO.Path.Combine(Environment.CurrentDirectory, "compiled.csv")

    End Function

    ''' <summary>
    ''' Build the path of the file to feed with all CSV contents
    ''' </summary>
    Public Function ReturnLogFilepath() As String

        Return IO.Path.Combine(Environment.CurrentDirectory, "CSV Compiler - log.txt")

    End Function

End Module
