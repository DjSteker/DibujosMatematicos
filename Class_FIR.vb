



Public Class Class_FIR

    Friend Property FiltroFactor As Double
    Friend Property ValorAnterior As Double

    Friend Property ValorAnterior2 As Double
    Friend Property ValorAnterior3 As Double


    Friend Sub New(Optional ByVal ValorAnterior As Double = 0)
        MyBase.New
        FiltroFactor = 0.5
        ValorAnterior = ValorAnterior
    End Sub

    Friend Function ObtenerValor(ByRef NuevoValor As Double) As Double
        Dim Valor As Double = FiltroFactor * (NuevoValor) + (ValorAnterior * (1 - FiltroFactor))
        ValorAnterior = NuevoValor
        Return Valor
    End Function

    Friend Function ObtenerValor2(ByRef NuevoValor As Double) As Double
        Dim Valor As Double = FiltroFactor * (NuevoValor) + (((ValorAnterior + ValorAnterior2) / 2) * (1 - FiltroFactor))
        ValorAnterior2 = ValorAnterior
        ValorAnterior = NuevoValor
        Return Valor
    End Function

    Friend Function ObtenerValor3(ByRef NuevoValor As Double) As Double
        Dim Valor As Double = FiltroFactor * (NuevoValor) + (((ValorAnterior3 + ValorAnterior + ValorAnterior2) / 3) * (1 - FiltroFactor))
        ValorAnterior3 = ValorAnterior2
        ValorAnterior2 = ValorAnterior
        ValorAnterior = NuevoValor
        Return Valor
    End Function


End Class
