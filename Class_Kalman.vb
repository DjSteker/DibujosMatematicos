Public Class Class_Kalman
    'Class SensorKalman


    Private Q_distance As Double
    Private R_measure As Double
    Private distance As Double
    Private P As Double
    Private K As Double
    Private y As Double
    Private S As Double


    Friend Sub New()
        MyBase.New
        Q_distance = 1
        R_measure = 1
        distance = 0
        'reset the distance
        P = 0
        'initial covariance matrix     
    End Sub

    ''' <summary>
    ''' Predice el valor de filtrado
    ''' </summary>
    ''' <param name="newDistance">Entrada del sensor</param>
    ''' <param name="dt">Tiempo desde la ultima lectura en segundos</param>
    ''' <returns>Prediccion del valor de filtrado</returns>
    Friend Function getDistance(ByVal newDistance As Double, ByVal dt As Double) As Double
        'distance=distance; //priori distance
        P = (P + (Q_distance * dt))
        'estimation error covariance
        'Kalman gain
        S = (P + R_measure)
        K = (P / S)
        'Update whith measurement 
        y = (newDistance - distance)
        'Calculate distance
        distance = (distance + (K * y))
        'Update the error covariance
        P = (P * (1 - K))
        Return distance
    End Function

    Friend Sub setDistance(ByVal newDistance As Double)
        distance = newDistance
    End Sub

    Friend Function getQdistance() As Double
        Return Q_distance
    End Function

    Friend Sub setQdistance(ByVal newQ_distance As Double)
        Q_distance = newQ_distance
    End Sub

    Friend Function getRmeasure() As Double
        Return R_measure
    End Function

    Friend Sub setRmeasure(ByVal newR_measure As Double)
        R_measure = newR_measure
    End Sub



End Class
