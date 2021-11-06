Module Logger

    Public Sub LogMessage(message As String)

        IO.File.AppendAllText(ReturnLogFilepath(), $"{Date.Now} : {message}")

    End Sub

End Module
