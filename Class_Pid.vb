'Public Class Class_Pid

Imports System
Imports System.Threading

Namespace PIDLibrary


    Public Class Class_Pid
#Region "Fields"
        Private kp As Double
        Private ki As Double
        Private kd As Double
        'Running Values
        'Private lastUpdate As DateTime
        Private lastPV As Double
        Private errSum As Double
        'Reading/Writing Values
        'Private readPV As GetDouble
        'Private readSP As GetDouble
        Private readPV As Double
        Private readSP As Double
        Private writeOV As SetDouble
        'Max/Min Calculation
        'Private pvMax As Double
        'Private pvMin As Double
        'Private outMax As Double
        ' Private outMin As Double
        'Threading and Timing
        'Private computeHz As Double = 1.0!
        'Private runThread As Thread

#End Region
#Region "Properties"

        Public Property PGain As Double
            Get
                Return Me.kp
            End Get
            Set
                Me.kp = Value
            End Set
        End Property

        Public Property IGain As Double
            Get
                Return Me.ki
            End Get
            Set
                Me.ki = Value
            End Set
        End Property

        Public Property DGain As Double
            Get
                Return Me.kd
            End Get
            Set
                Me.kd = Value
            End Set
        End Property

        Public Property PVMin As Double
            Get
                Return Me.PVMin
            End Get
            Set
                Me.PVMin = Value
            End Set
        End Property

        Public Property PVMax As Double
            Get
                Return Me.PVMax
            End Get
            Set
                Me.PVMax = Value
            End Set
        End Property

        Public Property OutMin As Double
            Get
                Return Me.OutMin
            End Get
            Set
                Me.OutMin = Value
            End Set
        End Property

        Public Property OutMax As Double
            Get
                Return Me.OutMax
            End Get
            Set
                Me.OutMax = Value
            End Set
        End Property

        'Public ReadOnly Property PIDOK As Boolean
        '    Get
        '        Return (Not (Me.runThread) Is Nothing)
        '    End Get
        'End Property
#End Region
#Region "Construction / Deconstruction"

        'Public Sub New(ByVal pG As Double, ByVal iG As Double, ByVal dG As Double, ByVal pMax As Double, ByVal pMin As Double, ByVal oMax As Double, ByVal oMin As Double, ByVal pvFunc As GetDouble, ByVal spFunc As GetDouble, ByVal outFunc As SetDouble)
        Public Sub New(ByVal pG As Double, ByVal iG As Double, ByVal dG As Double, ByVal pMax As Double, ByVal pMin As Double, ByVal oMax As Double, ByVal oMin As Double, ByVal outFunc As Double)
            MyBase.New
            Me.kp = pG
            Me.ki = iG
            Me.kd = dG
            Me.PVMax = pMax
            Me.PVMin = pMin
            Me.OutMax = oMax
            Me.OutMin = oMin
            'Me.readPV = pvFunc
            'Me.readSP = spFunc
            'Me.writeOV = outFunc
        End Sub


#End Region


#Region "Private Methods"

        Private Function ScaleValue(ByVal value As Double, ByVal valuemin As Double, ByVal valuemax As Double, ByVal scalemin As Double, ByVal scalemax As Double) As Double
            Dim vPerc As Double = ((value - valuemin) / (valuemax - valuemin))
            Dim bigSpan As Double = (vPerc * (scalemax - scalemin))
            Dim retVal As Double = (scalemin + bigSpan)
            Return retVal
        End Function

        Private Function Clamp(ByVal value As Double, ByVal min As Double, ByVal max As Double) As Double
            If (value > max) Then
                Return max
            End If

            If (value < min) Then
                Return min
            End If

            Return value
        End Function

        Private Sub Compute()
            'If ((Me.readPV Is Nothing) OrElse ((Me.readSP Is Nothing) OrElse (Me.writeOV Is Nothing))) Then
            '    Return
            'End If

            Dim pv As Double '= readPV
            Dim sp As Double '= readSP

            'We need to scale the pv to +/- 100%, but first clamp it
            pv = Me.Clamp(pv, Me.PVMin, Me.PVMax)
            pv = Me.ScaleValue(pv, Me.PVMin, Me.PVMax, -1.0!, 1.0!)

            'We also need to scale the setpoint
            sp = Me.Clamp(sp, Me.PVMin, Me.PVMax)
            sp = Me.ScaleValue(sp, Me.PVMin, Me.PVMax, -1.0!, 1.0!)

            'Now the error is in percent...
            Dim err As Double = (sp - pv)
            Dim pTerm As Double = (err * Me.kp)
            Dim iTerm As Double = 0!
            Dim dTerm As Double = 0!
            Dim partialSum As Double = 0!
            'Dim nowTime As DateTime = DateTime.Now
            'If (Not (Me.lastUpdate) Is Nothing) Then
            'If (Not (Me.lastUpdate) = Nothing) Then
            'Dim dT As Double = (nowTime - Me.lastUpdate).TotalSeconds
            Dim dT As Double = 1 ' La suma de los bias anteriores . a cambio del tiempo
            'Compute the integral if we have to...

            If ((pv >= Me.PVMin) AndAlso (pv <= Me.PVMax)) Then
                    partialSum = (Me.errSum + (dT * err))
                    iTerm = (Me.ki * partialSum)
                End If

                If (dT <> 0!) Then
                    dTerm = (Me.kd * ((pv - Me.lastPV) / dT))
                End If

            'End If

            'Me.lastUpdate = nowTime
            Me.errSum = partialSum
            Me.lastPV = pv
            'Now we have to scale the output value to match the requested scale
            Dim outReal As Double = (pTerm + (iTerm + dTerm))
            outReal = Me.Clamp(outReal, -1.0!, 1.0!)
            outReal = Me.ScaleValue(outReal, -1.0!, 1.0!, Me.OutMin, Me.OutMax)
            'Write it out to the world
            writeOV(outReal)
        End Sub

#End Region

    End Class





    'Public Delegate double GetDouble();
    'Public Delegate void SetDouble(Double value);
    Public Delegate Function GetDouble() As Double
    Public Delegate Sub SetDouble(value As Double)


    Public Class PID
#Region "Fields"
        Private kp As Double
        Private ki As Double
        Private kd As Double
        'Running Values
        Private lastUpdate As DateTime
        Private lastPV As Double
        Private errSum As Double
        'Reading/Writing Values
        'Private readPV As GetDouble
        'Private readSP As GetDouble
        Private readPV As Double
        Private readSP As Double
        Private writeOV As SetDouble
        'Max/Min Calculation
        'Private pvMax As Double
        'Private pvMin As Double
        'Private outMax As Double
        ' Private outMin As Double
        'Threading and Timing
        Private computeHz As Double = 1.0!
        Private runThread As Thread

#End Region
#Region "Properties"

        Public Property PGain As Double
            Get
                Return Me.kp
            End Get
            Set
                Me.kp = Value
            End Set
        End Property

        Public Property IGain As Double
            Get
                Return Me.ki
            End Get
            Set
                Me.ki = Value
            End Set
        End Property

        Public Property DGain As Double
            Get
                Return Me.kd
            End Get
            Set
                Me.kd = Value
            End Set
        End Property

        Public Property PVMin As Double
            Get
                Return Me.pvMin
            End Get
            Set
                Me.pvMin = Value
            End Set
        End Property

        Public Property PVMax As Double
            Get
                Return Me.pvMax
            End Get
            Set
                Me.pvMax = Value
            End Set
        End Property

        Public Property OutMin As Double
            Get
                Return Me.outMin
            End Get
            Set
                Me.outMin = Value
            End Set
        End Property

        Public Property OutMax As Double
            Get
                Return Me.outMax
            End Get
            Set
                Me.outMax = Value
            End Set
        End Property

        Public ReadOnly Property PIDOK As Boolean
            Get
                Return (Not (Me.runThread) Is Nothing)
            End Get
        End Property
#End Region
#Region "Construction / Deconstruction"

        'Public Sub New(ByVal pG As Double, ByVal iG As Double, ByVal dG As Double, ByVal pMax As Double, ByVal pMin As Double, ByVal oMax As Double, ByVal oMin As Double, ByVal pvFunc As GetDouble, ByVal spFunc As GetDouble, ByVal outFunc As SetDouble)
        Public Sub New(ByVal pG As Double, ByVal iG As Double, ByVal dG As Double, ByVal pMax As Double, ByVal pMin As Double, ByVal oMax As Double, ByVal oMin As Double, ByVal outFunc As SetDouble)
            MyBase.New
            Me.kp = pG
            Me.ki = iG
            Me.kd = dG
            Me.PVMax = pMax
            Me.PVMin = pMin
            Me.OutMax = oMax
            Me.OutMin = oMin
            'Me.readPV = pvFunc
            'Me.readSP = spFunc
            Me.writeOV = outFunc
        End Sub

        Private Sub New()
            MyBase.New
            Me.Disable()
            Me.readPV = Nothing
            Me.readSP = Nothing
            Me.writeOV = Nothing
        End Sub
#End Region
#Region "Public Methods"

        Public Sub Enable()
            If (Not (Me.runThread) Is Nothing) Then
                Return
            End If

            Me.Reset()
            'Me.runThread = New Thread(New ThreadStart(Run))
            Me.runThread = New Thread(AddressOf Run)
            Me.runThread.IsBackground = True
            Me.runThread.Name = "PID Processor"
            Me.runThread.Start()
        End Sub

        Public Sub Disable()
            If (Me.runThread Is Nothing) Then
                Return
            End If

            Me.runThread.Abort()
            Me.runThread = Nothing
        End Sub

        Public Sub Reset()
            Me.errSum = 0!
            Me.lastUpdate = DateTime.Now
        End Sub
#End Region
#Region "Private Methods"

        Private Function ScaleValue(ByVal value As Double, ByVal valuemin As Double, ByVal valuemax As Double, ByVal scalemin As Double, ByVal scalemax As Double) As Double
            Dim vPerc As Double = ((value - valuemin) / (valuemax - valuemin))
            Dim bigSpan As Double = (vPerc * (scalemax - scalemin))
            Dim retVal As Double = (scalemin + bigSpan)
            Return retVal
        End Function

        Private Function Clamp(ByVal value As Double, ByVal min As Double, ByVal max As Double) As Double
            If (value > max) Then
                Return max
            End If

            If (value < min) Then
                Return min
            End If

            Return value
        End Function

        Private Sub Compute()
            'If ((Me.readPV Is Nothing) OrElse ((Me.readSP Is Nothing) OrElse (Me.writeOV Is Nothing))) Then
            '    Return
            'End If

            Dim pv As Double '= readPV
            Dim sp As Double '= readSP

            'We need to scale the pv to +/- 100%, but first clamp it
            pv = Me.Clamp(pv, Me.pvMin, Me.pvMax)
            pv = Me.ScaleValue(pv, Me.PVMin, Me.PVMax, -1.0!, 1.0!)

            'We also need to scale the setpoint
            sp = Me.Clamp(sp, Me.pvMin, Me.pvMax)
            sp = Me.ScaleValue(sp, Me.PVMin, Me.PVMax, -1.0!, 1.0!)

            'Now the error is in percent...
            Dim err As Double = (sp - pv)
            Dim pTerm As Double = (err * Me.kp)
            Dim iTerm As Double = 0!
            Dim dTerm As Double = 0!
            Dim partialSum As Double = 0!
            Dim nowTime As DateTime = DateTime.Now
            'If (Not (Me.lastUpdate) Is Nothing) Then
            If (Not (Me.lastUpdate) = Nothing) Then
                Dim dT As Double = (nowTime - Me.lastUpdate).TotalSeconds
                'Compute the integral if we have to...
                If ((pv >= Me.PVMin) AndAlso (pv <= Me.PVMax)) Then
                    partialSum = (Me.errSum + (dT * err))
                    iTerm = (Me.ki * partialSum)
                End If

                If (dT <> 0!) Then
                    dTerm = (Me.kd * ((pv - Me.lastPV) / dT))
                End If

            End If

            Me.lastUpdate = nowTime
            Me.errSum = partialSum
            Me.lastPV = pv
            'Now we have to scale the output value to match the requested scale
            Dim outReal As Double = (pTerm + (iTerm + dTerm))
            outReal = Me.Clamp(outReal, -1.0!, 1.0!)
            outReal = Me.ScaleValue(outReal, -1.0!, 1.0!, Me.outMin, Me.outMax)
            'Write it out to the world
            writeOV(outReal)
        End Sub

#End Region
#Region "Threading"

        Private Sub Run()

            While True
                Try
                    Dim sleepTime As Integer = CType((1000 / Me.computeHz), Integer)
                    Thread.Sleep(sleepTime)
                    Me.Compute()
                Catch e As Exception

                End Try


            End While

        End Sub

#End Region
    End Class




End Namespace