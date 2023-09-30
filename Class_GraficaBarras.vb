Public Class Class_GraficaBarras
    Implements IDisposable

    ' Private Const CUSTOM_SCALE_FACTOR As Integer = 15
    Private NUMBER_OF_BARS As Integer = 40
    Private DECREASING_BAR_VALUE As Double = 2.1 '2.5#
    Private DECREASING_PEAK_VALUE As Double = 0.75 ' 0.5#
    Private WAIT_DECREASING_PEAK_VALUE As Double = 3.2 '3.1#

    Friend PeakOldValorminimo As Integer
    Friend OldValorminimo As Integer

    Private Structure VisualizationBar
        Public nIndex As Int32
        Public Valor As Double
        Public dOldValue As Double
        Public dPeakOldValue As Double
        Public dPeakTimeValue As Double
    End Structure

    Friend ReadOnly Property IndicedOldValue() As Integer
        Get
            PeakOldValorminimo = Integer.MaxValue
            Dim nIndex As Integer = -1
            For indice As Integer = bars.Count - 1 To 0 Step -1
                If bars(indice).dPeakOldValue < PeakOldValorminimo Then
                    'Valorminimo = bars(indice).dPeakTimeValue
                    PeakOldValorminimo = bars(indice).dPeakOldValue
                    nIndex = bars(indice).nIndex
                End If
            Next

            Return nIndex
        End Get
    End Property
    Friend ReadOnly Property IndicedPeakOldValue() As Integer
        Get
            OldValorminimo = Integer.MaxValue
            Dim nIndex As Integer = -1
            For indice As Integer = bars.Count - 1 To 0 Step -1
                If bars(indice).dOldValue < OldValorminimo Then
                    'Valorminimo = bars(indice).dPeakTimeValue
                    OldValorminimo = bars(indice).dOldValue
                    nIndex = bars(indice).nIndex
                End If
            Next

            Return nIndex
        End Get
    End Property


    Private bars(NUMBER_OF_BARS - 1) As VisualizationBar

    Private pPictueBox As PictureBox = Nothing
    Private bit As Bitmap
    Private gc As Graphics
    Private WhitePen As Brush


    Public Sub New(ByRef PictureBox_ByRef As PictureBox, ByRef NumeroBarras As Integer)
        Try
            pPictueBox = PictureBox_ByRef
            'Init bars
            NUMBER_OF_BARS = NumeroBarras
            bars = New VisualizationBar(NUMBER_OF_BARS - 1) {}
            For i As Integer = 0 To NUMBER_OF_BARS - 1
                bars(i).nIndex = (i)
                bars(i).Valor = 0
                bars(i).dOldValue = 0
                bars(i).dPeakOldValue = 0
            Next

            'Create graphics
            bit = New Bitmap(pPictueBox.Width, pPictueBox.Height)
            gc = Graphics.FromImage(bit)
            'render = pPictueBox.CreateGraphics
            WhitePen = New SolidBrush(Color.DarkOliveGreen) ' TODO: Dynamic color

            'Set rendering
            gc.SmoothingMode = Drawing2D.SmoothingMode.HighSpeed

            'Clear with black background
            gc.Clear(Color.Transparent)



        Catch ex As Exception

        End Try

    End Sub

    Public Sub CalculaBarras(ByRef data() As Double)



        Dim Barras() As Double = data

        For i As Integer = 0 To NUMBER_OF_BARS - 1


            Try

                bars(i).Valor = Barras(i)

                ' Multiply by a custom factor to scale input to picturebox height
                'value = value / 35 * pPictueBox.Height
                'value = (value / (Math.Log10(value) + 2048 * (1 / pPictueBox.Height))) / 4
                'bars(i).Valor *= 254  'pPictueBox.Height
                Dim value As Double = bars(i).Valor * pPictueBox.Height

                ' Clip values to fit in the picture box
                If value > pPictueBox.Height - 1 Then
                    value = pPictueBox.Height - 1
                ElseIf value < 1 Then
                    value = 1
                End If

                If value < 0 Then
                    value = 0
                End If

                ' if value > of stored value update value, otherwise decrease value
                If value > bars(i).dOldValue Then
                    bars(i).dOldValue = value
                Else
                    If bars(i).dOldValue > 0 Then
                        bars(i).dOldValue -= DECREASING_BAR_VALUE
                    End If

                End If

                ' if value > of stored value update value, otherwise
                ' wait WAIT_DECREASING_PEAK_VALUE value then decrease
                ' peak value
                If value > bars(i).dPeakOldValue Then
                    bars(i).dPeakOldValue = value
                    bars(i).dPeakTimeValue = 0.0#
                Else
                    If bars(i).dPeakTimeValue >= WAIT_DECREASING_PEAK_VALUE Then
                        If bars(i).dPeakOldValue > 0 Then
                            bars(i).dPeakOldValue -= DECREASING_PEAK_VALUE
                        End If

                    Else
                        bars(i).dPeakTimeValue += 0.1#
                    End If
                End If

            Catch ex As Exception

            End Try

        Next
    End Sub
    Public Sub PintaBarras()

        Try
            bit = New Bitmap(pPictueBox.Width, pPictueBox.Height)
            gc = Graphics.FromImage(bit)

            Dim rect As Rectangle
            Dim peak As Rectangle
            Dim value As Double

            ' Clear background
            gc.Clear(Color.Transparent)


            Dim EscalaColor As Double = (255 / pPictueBox.Height)

            'Draw bars
            For i As Integer = 0 To NUMBER_OF_BARS - 1

                Try

                    value = bars(i).Valor * pPictueBox.Height


                    If value > pPictueBox.Height - 1 Then
                        value = pPictueBox.Height - 1
                    ElseIf value < 1 Then
                        value = 1
                    End If

                    If value < 0 Then
                        value = 0
                    End If



                    Dim ColorR As Int16 = 100
                    Dim ColorG As Int16 = 100
                    Dim ColorB As Int16 = 100
                    Dim color As Color = Color.FromArgb(ColorR, ColorG, ColorB) 'New Color((255 / 2000) * value,
                    Dim pen As Pen = New System.Drawing.Pen(color)

                    Try
                        ColorR = EscalaColor * value
                        ColorG = 255 - (EscalaColor * CInt(bars(i).dOldValue))
                        ColorB = 255 - (EscalaColor * CInt(bars(i).dPeakOldValue))
                        If ColorR > 255 Then
                            ColorR = 255
                        End If
                        If ColorG > 255 Then
                            ColorG = 255
                        End If
                        If ColorB > 255 Then
                            ColorB = 255
                        End If

                        If ColorR < 0 Then
                            ColorR = 0
                        End If
                        If ColorG < 0 Then
                            ColorG = 0
                        End If
                        If ColorB < 0 Then
                            ColorB = 0
                        End If

                        color = Color.FromArgb(ColorR, ColorG, ColorB) 'New Color((255 / 2000) * value,
                        pen = New System.Drawing.Pen(color)

                        peak.X = (160 / NUMBER_OF_BARS) + i * (pPictueBox.Width / NUMBER_OF_BARS)
                        peak.Width = ((pPictueBox.Width - (NUMBER_OF_BARS * 2)) / NUMBER_OF_BARS)
                        peak.Y = pPictueBox.Height - CInt(bars(i).dPeakOldValue)
                        peak.Height = 1
                        ' TODO: dynamic color
                        gc.DrawLine(pen, peak.X, peak.Y, peak.X + peak.Width - 1, peak.Y)

                        rect.X = (80 / NUMBER_OF_BARS) + i * (pPictueBox.Width / NUMBER_OF_BARS)
                        rect.Width = ((pPictueBox.Width - (NUMBER_OF_BARS * 2)) / NUMBER_OF_BARS)
                        rect.Y = pPictueBox.Height - CInt(bars(i).dOldValue)
                        rect.Height = CInt(bars(i).dOldValue)

                    Catch ex As Exception

                    End Try

                    Try
                        ColorR = EscalaColor * value
                        ColorG = 255 - EscalaColor * value
                        ColorB = EscalaColor * CInt(bars(i).dOldValue)
                        If ColorR > 255 Then
                            ColorR = 255
                        End If
                        If ColorG > 255 Then
                            ColorG = 255
                        End If
                        If ColorB > 255 Then
                            ColorB = 255
                        End If

                        If ColorR < 0 Then
                            ColorR = 0
                        End If
                        If ColorG < 0 Then
                            ColorG = 0
                        End If
                        If ColorB < 0 Then
                            ColorB = 0
                        End If

                        color = Color.FromArgb(ColorR, ColorG, ColorB) 'New Color((255 / 2000) * value,
                        WhitePen = New SolidBrush(color)
                        gc.FillRectangle(WhitePen, rect)

                        peak.X = (160 / NUMBER_OF_BARS) + i * (pPictueBox.Width / NUMBER_OF_BARS)
                        peak.Width = ((pPictueBox.Width - (NUMBER_OF_BARS * 2)) / NUMBER_OF_BARS)
                        peak.Y = pPictueBox.Height - CInt(value)
                        peak.Height = 1
                    Catch ex As Exception

                    End Try



                    ' TODO: dynamic color
                    Try
                        ColorR = 255 - EscalaColor * value
                        ColorG = EscalaColor * CInt(bars(i).dOldValue)
                        ColorB = 255 - EscalaColor * CInt(bars(i).dPeakOldValue)

                        If ColorR > 255 Then
                            ColorR = 255
                        End If
                        If ColorG > 255 Then
                            ColorG = 255
                        End If
                        If ColorB > 255 Then
                            ColorB = 255
                        End If

                        If ColorR < 0 Then
                            ColorR = 0
                        End If
                        If ColorG < 0 Then
                            ColorG = 0
                        End If
                        If ColorB < 0 Then
                            ColorB = 0
                        End If

                        color = Color.FromArgb(ColorR, ColorG, ColorB) 'New Color((255 / 2000) * value,
                        pen = New System.Drawing.Pen(color)
                        gc.DrawLine(pen, peak.X + 5, peak.Y, peak.X + peak.Width - 6, peak.Y)

                        gc.DrawLine(pen, CInt(peak.X + (peak.Width / 2)), pPictueBox.Height - CInt(bars(i).dPeakOldValue), CInt(peak.X + (peak.Width / 2)), peak.Y)
                        If NUMBER_OF_BARS - 1 = i Then
                            pen = New System.Drawing.Pen(Color.FromArgb(150, 150, 150))
                            Dim asdf As Boolean = False
                            gc.DrawLine(pen, CInt(peak.X + (peak.Width / 2)), pPictueBox.Height - CInt(100), CInt(peak.X + (peak.Width / 2)), peak.Y)
                        End If
                    Catch ex As Exception

                    End Try

                Catch ex As Exception

                End Try

            Next

            pPictueBox.Image = bit
            gc.Flush()
            gc.Dispose()

        Catch ex As Exception

        End Try



    End Sub

    Public Sub DrawBarras(ByRef data() As Double)

        '_canvasTimeDomain = New Bitmap(PictureBox.Width, PictureBox.Height)
        'Dim offScreenDC As Graphics = Graphics.FromImage(_canvasTimeDomain)
        Try
            bit = New Bitmap(pPictueBox.Width, pPictueBox.Height)
            'gc = Graphics.FromImage(bit)
            gc = Graphics.FromImage(bit)
            '_canvasFrequencyDomain = New Bitmap(pPictueBox.Width, pPictueBox.Height)
            Dim rect As Rectangle
            Dim peak As Rectangle
            Dim value As Double
            Dim prevIndex As Integer = 0

            ' Clear background
            gc.Clear(Color.Transparent)


            Dim Barras() As Double = data
            Dim EscalaColor As Double = (255 / pPictueBox.Height)

            'Draw bars
            For i As Integer = 0 To NUMBER_OF_BARS - 1


                Try
                    ' calculate averange of frequency and return abs value
                    'value = AvgFrequency(Datos, prevIndex, bars(i).nIndex)

                    value = Barras(i)

                    prevIndex = bars(i).nIndex

                    ' Multiply by a custom factor to scale input to picturebox height
                    'value = value / 35 * pPictueBox.Height
                    'value = (value / (Math.Log10(value) + 2048 * (1 / pPictueBox.Height))) / 4
                    value *= 128 '254  'pPictueBox.Height
                    ' Clip values to fit in the picture box
                    If value > pPictueBox.Height - 1 Then
                        value = pPictueBox.Height - 1
                    ElseIf value < 1 Then
                        value = 1
                    End If

                    If value < 0 Then
                        value = 0
                    End If

                    ' if value > of stored value update value, otherwise decrease value
                    If value > bars(i).dOldValue Then
                        bars(i).dOldValue = value
                    Else
                        If bars(i).dOldValue > 0 Then
                            bars(i).dOldValue -= DECREASING_BAR_VALUE
                        End If

                    End If

                    ' if value > of stored value update value, otherwise
                    ' wait WAIT_DECREASING_PEAK_VALUE value then decrease
                    ' peak value
                    If value > bars(i).dPeakOldValue Then
                        bars(i).dPeakOldValue = value
                        bars(i).dPeakTimeValue = 0.0#
                    Else
                        If bars(i).dPeakTimeValue >= WAIT_DECREASING_PEAK_VALUE Then
                            If bars(i).dPeakOldValue > 0 Then
                                bars(i).dPeakOldValue -= DECREASING_PEAK_VALUE
                            End If

                        Else
                            bars(i).dPeakTimeValue += 0.1#
                        End If
                    End If

                    ''Peak coord
                    'peak.X = 4 + i * 5
                    'peak.Width = 3
                    'peak.Y = pPictueBox.Height - CInt(bars(i).dPeakOldValue)
                    'peak.Height = 1

                    '' TODO: dynamic color
                    'gc.DrawLine(Pens.White, peak.X, peak.Y, peak.X + peak.Width - 1, peak.Y)

                    '' Bars
                    'rect.X = 2 + i * 5
                    'rect.Width = 3
                    'rect.Y = pPictueBox.Height - CInt(bars(i).dOldValue)
                    'rect.Height = CInt(bars(i).dOldValue)


                    Dim ColorR As Int16 = 100
                    Dim ColorG As Int16 = 100
                    Dim ColorB As Int16 = 100
                    Dim color As Color = Color.FromArgb(ColorR, ColorG, ColorB) 'New Color((255 / 2000) * value,
                    Dim pen As Pen = New System.Drawing.Pen(color)

                    Try
                        ColorR = EscalaColor * value
                        ColorG = 255 - (EscalaColor * CInt(bars(i).dOldValue))
                        ColorB = 255 - (EscalaColor * CInt(bars(i).dPeakOldValue))
                        If ColorR > 255 Then
                            ColorR = 255
                        End If
                        If ColorG > 255 Then
                            ColorG = 255
                        End If
                        If ColorB > 255 Then
                            ColorB = 255
                        End If

                        If ColorR < 0 Then
                            ColorR = 0
                        End If
                        If ColorG < 0 Then
                            ColorG = 0
                        End If
                        If ColorB < 0 Then
                            ColorB = 0
                        End If

                        color = Color.FromArgb(ColorR, ColorG, ColorB) 'New Color((255 / 2000) * value,
                        pen = New System.Drawing.Pen(color)



                        'Peak coord
                        'peak.X = 8 + i * 39
                        'peak.Width = 35
                        'peak.Y = pPictueBox.Height - CInt(bars(i).dPeakOldValue)
                        'peak.Height = 1
                        peak.X = (160 / NUMBER_OF_BARS) + i * (pPictueBox.Width / NUMBER_OF_BARS)
                        peak.Width = ((pPictueBox.Width - (NUMBER_OF_BARS * 2)) / NUMBER_OF_BARS)
                        peak.Y = pPictueBox.Height - CInt(bars(i).dPeakOldValue)
                        peak.Height = 1
                        ' TODO: dynamic color
                        gc.DrawLine(pen, peak.X, peak.Y, peak.X + peak.Width - 1, peak.Y)

                        rect.X = (80 / NUMBER_OF_BARS) + i * (pPictueBox.Width / NUMBER_OF_BARS)
                        rect.Width = ((pPictueBox.Width - (NUMBER_OF_BARS * 2)) / NUMBER_OF_BARS)
                        rect.Y = pPictueBox.Height - CInt(bars(i).dOldValue)
                        rect.Height = CInt(bars(i).dOldValue)

                    Catch ex As Exception

                    End Try



                    ' Bars
                    'rect.X = 4 + i * 39
                    'rect.Width = 35
                    'rect.Y = pPictueBox.Height - CInt(bars(i).dOldValue)
                    'rect.Height = CInt(bars(i).dOldValue)

                    Try
                        ColorR = EscalaColor * value
                        ColorG = 255 - EscalaColor * value
                        ColorB = EscalaColor * CInt(bars(i).dOldValue)
                        If ColorR > 255 Then
                            ColorR = 255
                        End If
                        If ColorG > 255 Then
                            ColorG = 255
                        End If
                        If ColorB > 255 Then
                            ColorB = 255
                        End If

                        If ColorR < 0 Then
                            ColorR = 0
                        End If
                        If ColorG < 0 Then
                            ColorG = 0
                        End If
                        If ColorB < 0 Then
                            ColorB = 0
                        End If

                        color = Color.FromArgb(ColorR, ColorG, ColorB) 'New Color((255 / 2000) * value,
                        WhitePen = New SolidBrush(color)
                        gc.FillRectangle(WhitePen, rect)


                        'time coord
                        'peak.X = 8 + i * 39
                        'peak.Width = 35
                        'peak.Y = pPictueBox.Height - CInt(value)
                        'peak.Height = 1
                        peak.X = (160 / NUMBER_OF_BARS) + i * (pPictueBox.Width / NUMBER_OF_BARS)
                        peak.Width = ((pPictueBox.Width - (NUMBER_OF_BARS * 2)) / NUMBER_OF_BARS)
                        peak.Y = pPictueBox.Height - CInt(value)
                        peak.Height = 1
                    Catch ex As Exception

                    End Try



                    ' TODO: dynamic color
                    Try
                        ColorR = 255 - EscalaColor * value
                        ColorG = EscalaColor * CInt(bars(i).dOldValue)
                        ColorB = 255 - EscalaColor * CInt(bars(i).dPeakOldValue)

                        If ColorR > 255 Then
                            ColorR = 255
                        End If
                        If ColorG > 255 Then
                            ColorG = 255
                        End If
                        If ColorB > 255 Then
                            ColorB = 255
                        End If

                        If ColorR < 0 Then
                            ColorR = 0
                        End If
                        If ColorG < 0 Then
                            ColorG = 0
                        End If
                        If ColorB < 0 Then
                            ColorB = 0
                        End If

                        color = Color.FromArgb(ColorR, ColorG, ColorB) 'New Color((255 / 2000) * value,
                        pen = New System.Drawing.Pen(color)
                        gc.DrawLine(pen, peak.X + 5, peak.Y, peak.X + peak.Width - 6, peak.Y)

                        gc.DrawLine(pen, CInt(peak.X + (peak.Width / 2)), pPictueBox.Height - CInt(bars(i).dPeakOldValue), CInt(peak.X + (peak.Width / 2)), peak.Y)
                        If NUMBER_OF_BARS - 1 = i Then
                            pen = New System.Drawing.Pen(Color.FromArgb(150, 150, 150))
                            Dim asdf As Boolean = False
                            gc.DrawLine(pen, CInt(peak.X + (peak.Width / 2)), pPictueBox.Height - CInt(100), CInt(peak.X + (peak.Width / 2)), peak.Y)
                        End If
                    Catch ex As Exception

                    End Try

                Catch ex As Exception

                End Try

            Next

            ' draw double buffer image
            'render.DrawImage(bit, New Point(0, 0))
            pPictueBox.Image = bit
            gc.Flush()
            'bit.Dispose()
            gc.Dispose()


            'pPictueBox.Image = _canvasTimeDomain
            'offScreenDC.Dispose()

        Catch ex As Exception

        End Try



    End Sub


    Public Sub DrawBarras0(ByRef data() As Double)

        '_canvasTimeDomain = New Bitmap(PictureBox.Width, PictureBox.Height)
        'Dim offScreenDC As Graphics = Graphics.FromImage(_canvasTimeDomain)
        Try
            bit = New Bitmap(pPictueBox.Width, pPictueBox.Height)
            'gc = Graphics.FromImage(bit)
            gc = Graphics.FromImage(bit)
            '_canvasFrequencyDomain = New Bitmap(pPictueBox.Width, pPictueBox.Height)
            Dim rect As Rectangle
            Dim peak As Rectangle
            Dim value As Double
            Dim prevIndex As Integer = 0

            ' Clear background
            gc.Clear(Color.Transparent)


            Dim Barras() As Double = data
            Dim EscalaColor As Double = (255 / pPictueBox.Height)

            'Draw bars
            For i As Integer = 0 To NUMBER_OF_BARS - 1


                Try
                    ' calculate averange of frequency and return abs value
                    'value = AvgFrequency(Datos, prevIndex, bars(i).nIndex)

                    value = Barras(i)

                    prevIndex = bars(i).nIndex

                    ' Multiply by a custom factor to scale input to picturebox height
                    'value = value / 35 * pPictueBox.Height
                    'value = (value / (Math.Log10(value) + 2048 * (1 / pPictueBox.Height))) / 4
                    value *= 254  'pPictueBox.Height
                    ' Clip values to fit in the picture box
                    If value > pPictueBox.Height - 1 Then
                        value = pPictueBox.Height - 1
                    ElseIf value < 1 Then
                        value = 1
                    End If

                    If value < 0 Then
                        value = 0
                    End If

                    ' if value > of stored value update value, otherwise decrease value
                    If value > bars(i).dOldValue Then
                        bars(i).dOldValue = value
                    Else
                        If bars(i).dOldValue > 0 Then
                            bars(i).dOldValue -= DECREASING_BAR_VALUE
                        End If

                    End If

                    ' if value > of stored value update value, otherwise
                    ' wait WAIT_DECREASING_PEAK_VALUE value then decrease
                    ' peak value
                    If value > bars(i).dPeakOldValue Then
                        bars(i).dPeakOldValue = value
                        bars(i).dPeakTimeValue = 0.0#
                    Else
                        If bars(i).dPeakTimeValue >= WAIT_DECREASING_PEAK_VALUE Then
                            If bars(i).dPeakOldValue > 0 Then
                                bars(i).dPeakOldValue -= DECREASING_PEAK_VALUE
                            End If

                        Else
                            bars(i).dPeakTimeValue += 0.1#
                        End If
                    End If

                    ''Peak coord
                    'peak.X = 4 + i * 5
                    'peak.Width = 3
                    'peak.Y = pPictueBox.Height - CInt(bars(i).dPeakOldValue)
                    'peak.Height = 1

                    '' TODO: dynamic color
                    'gc.DrawLine(Pens.White, peak.X, peak.Y, peak.X + peak.Width - 1, peak.Y)

                    '' Bars
                    'rect.X = 2 + i * 5
                    'rect.Width = 3
                    'rect.Y = pPictueBox.Height - CInt(bars(i).dOldValue)
                    'rect.Height = CInt(bars(i).dOldValue)


                    Dim ColorR As Int16 = 100
                    Dim ColorG As Int16 = 100
                    Dim ColorB As Int16 = 100
                    Dim color As Color = Color.FromArgb(ColorR, ColorG, ColorB) 'New Color((255 / 2000) * value,
                    Dim pen As Pen = New System.Drawing.Pen(color)

                    Try
                        ColorR = EscalaColor * value
                        ColorG = 255 - (EscalaColor * CInt(bars(i).dOldValue))
                        ColorB = 255 - (EscalaColor * CInt(bars(i).dPeakOldValue))
                        If ColorR > 255 Then
                            ColorR = 255
                        End If
                        If ColorG > 255 Then
                            ColorG = 255
                        End If
                        If ColorB > 255 Then
                            ColorB = 255
                        End If

                        If ColorR < 0 Then
                            ColorR = 0
                        End If
                        If ColorG < 0 Then
                            ColorG = 0
                        End If
                        If ColorB < 0 Then
                            ColorB = 0
                        End If

                        color = Color.FromArgb(ColorR, ColorG, ColorB) 'New Color((255 / 2000) * value,
                        pen = New System.Drawing.Pen(color)



                        'Peak coord
                        'peak.X = 8 + i * 39
                        'peak.Width = 35
                        'peak.Y = pPictueBox.Height - CInt(bars(i).dPeakOldValue)
                        'peak.Height = 1
                        peak.X = (160 / NUMBER_OF_BARS) + i * (780 / NUMBER_OF_BARS)
                        peak.Width = (700 / NUMBER_OF_BARS)
                        peak.Y = pPictueBox.Height - CInt(bars(i).dPeakOldValue)
                        peak.Height = 1
                        ' TODO: dynamic color
                        gc.DrawLine(pen, peak.X, peak.Y, peak.X + peak.Width - 1, peak.Y)

                        rect.X = (80 / NUMBER_OF_BARS) + i * (780 / NUMBER_OF_BARS)
                        rect.Width = (700 / NUMBER_OF_BARS)
                        rect.Y = pPictueBox.Height - CInt(bars(i).dOldValue)
                        rect.Height = CInt(bars(i).dOldValue)

                    Catch ex As Exception

                    End Try



                    ' Bars
                    'rect.X = 4 + i * 39
                    'rect.Width = 35
                    'rect.Y = pPictueBox.Height - CInt(bars(i).dOldValue)
                    'rect.Height = CInt(bars(i).dOldValue)

                    Try
                        ColorR = EscalaColor * value
                        ColorG = 255 - EscalaColor * value
                        ColorB = EscalaColor * CInt(bars(i).dOldValue)
                        If ColorR > 255 Then
                            ColorR = 255
                        End If
                        If ColorG > 255 Then
                            ColorG = 255
                        End If
                        If ColorB > 255 Then
                            ColorB = 255
                        End If

                        If ColorR < 0 Then
                            ColorR = 0
                        End If
                        If ColorG < 0 Then
                            ColorG = 0
                        End If
                        If ColorB < 0 Then
                            ColorB = 0
                        End If

                        color = Color.FromArgb(ColorR, ColorG, ColorB) 'New Color((255 / 2000) * value,
                        WhitePen = New SolidBrush(color)
                        gc.FillRectangle(WhitePen, rect)


                        'time coord
                        'peak.X = 8 + i * 39
                        'peak.Width = 35
                        'peak.Y = pPictueBox.Height - CInt(value)
                        'peak.Height = 1
                        peak.X = (160 / NUMBER_OF_BARS) + i * (780 / NUMBER_OF_BARS)
                        peak.Width = (700 / NUMBER_OF_BARS)
                        peak.Y = pPictueBox.Height - CInt(value)
                        peak.Height = 1
                    Catch ex As Exception

                    End Try



                    ' TODO: dynamic color
                    Try
                        ColorR = 255 - EscalaColor * value
                        ColorG = EscalaColor * CInt(bars(i).dOldValue)
                        ColorB = 255 - EscalaColor * CInt(bars(i).dPeakOldValue)

                        If ColorR > 255 Then
                            ColorR = 255
                        End If
                        If ColorG > 255 Then
                            ColorG = 255
                        End If
                        If ColorB > 255 Then
                            ColorB = 255
                        End If

                        If ColorR < 0 Then
                            ColorR = 0
                        End If
                        If ColorG < 0 Then
                            ColorG = 0
                        End If
                        If ColorB < 0 Then
                            ColorB = 0
                        End If

                        color = Color.FromArgb(ColorR, ColorG, ColorB) 'New Color((255 / 2000) * value,
                        pen = New System.Drawing.Pen(color)
                        gc.DrawLine(pen, peak.X + 5, peak.Y, peak.X + peak.Width - 6, peak.Y)

                        gc.DrawLine(pen, CInt(peak.X + (peak.Width / 2)), pPictueBox.Height - CInt(bars(i).dPeakOldValue), CInt(peak.X + (peak.Width / 2)), peak.Y)
                        If NUMBER_OF_BARS - 1 = i Then
                            pen = New System.Drawing.Pen(Color.FromArgb(150, 150, 150))
                            Dim asdf As Boolean = False
                            gc.DrawLine(pen, CInt(peak.X + (peak.Width / 2)), pPictueBox.Height - CInt(100), CInt(peak.X + (peak.Width / 2)), peak.Y)
                        End If
                    Catch ex As Exception

                    End Try

                Catch ex As Exception

                End Try

            Next

            ' draw double buffer image
            'render.DrawImage(bit, New Point(0, 0))
            pPictueBox.Image = bit
            gc.Flush()
            'bit.Dispose()
            gc.Dispose()


            'pPictueBox.Image = _canvasTimeDomain
            'offScreenDC.Dispose()

        Catch ex As Exception

        End Try



    End Sub


#Region "IDisposable Support"
    Private disposedValue As Boolean

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

                'Free resource
                If gc IsNot Nothing Then
                    gc.Dispose()
                    gc = Nothing
                End If

                'If render IsNot Nothing Then
                '    render.Dispose()
                '    render = Nothing
                'End If
            End If
        End If

        disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
    End Sub
#End Region

End Class
