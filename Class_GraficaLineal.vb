Public Class Class_GraficaLineal

    Private _canvasTimeDomain As Bitmap


    Public Sub RenderTimeDomainDoble(ByRef pictureBox As PictureBox, _waveLeft() As Single, _waveRight() As Single)
        Try
            _canvasTimeDomain = New Bitmap(pictureBox.Width, pictureBox.Height)
            Dim offScreenDC As Graphics = Graphics.FromImage(_canvasTimeDomain)
            'Dim brush As SolidBrush = New System.Drawing.SolidBrush(Color.FromArgb(0, 0, 0))
            Dim pen As Pen = New System.Drawing.Pen(Color.WhiteSmoke)
            Dim width As Integer = _canvasTimeDomain.Width
            Dim center As Integer = _canvasTimeDomain.Height / 2
            Dim height As Integer = _canvasTimeDomain.Height
            offScreenDC.DrawLine(pen, 0, center, width, center)
            Dim leftLeft As Integer = 0
            Dim leftTop As Integer = 0
            Dim leftRight As Integer = width
            Dim leftBottom As Integer = center - 1
            Dim rightLeft As Integer = 0
            Dim rightTop As Integer = center + 1
            Dim rightRight As Integer = width
            Dim rightBottom As Integer = height
            Dim yCenterLeft As Double = (leftBottom - leftTop) / 2
            Dim yScaleLeft As Double = 0.5 * (leftBottom - leftTop) / 32768
            Dim xPrevLeft As Integer = 0, yPrevLeft As Integer = 0

            For xAxis As Integer = leftLeft To leftRight - 1
                Try
                    Dim yAxis As Integer = CInt((yCenterLeft + (_waveLeft(_waveLeft.Length / (leftRight - leftLeft) * xAxis) * yScaleLeft)))

                    If xAxis = 0 Then
                        xPrevLeft = 0
                        yPrevLeft = yAxis
                    Else
                        pen.Color = Color.LimeGreen
                        offScreenDC.DrawLine(pen, xPrevLeft, yPrevLeft, xAxis, yAxis)
                        xPrevLeft = xAxis
                        yPrevLeft = yAxis
                    End If
                Catch ex As Exception

                End Try

            Next

            Dim xCenterRight As Integer = rightTop + ((rightBottom - rightTop) / 2)
            Dim yScaleRight As Double = 0.5 * (rightBottom - rightTop) / 32768
            Dim xPrevRight As Integer = 0, yPrevRight As Integer = 0

            For xAxis As Integer = rightLeft To rightRight - 1
                Try
                    Dim yAxis As Integer = CInt((xCenterRight + (_waveRight(_waveRight.Length / (rightRight - rightLeft) * xAxis) * yScaleRight)))

                    If xAxis = 0 Then
                        xPrevRight = 0
                        yPrevRight = yAxis
                    Else
                        pen.Color = Color.LimeGreen
                        offScreenDC.DrawLine(pen, xPrevRight, yPrevRight, xAxis, yAxis)
                        xPrevRight = xAxis
                        yPrevRight = yAxis
                    End If
                Catch ex As Exception

                End Try

            Next

            pictureBox.Image = _canvasTimeDomain
            offScreenDC.Dispose()
            'Application.DoEvents()
        Catch ex As Exception

        End Try

    End Sub




    Public Sub RenderTimeDomaiSimple(ByRef pictureBox As PictureBox, _wave() As Double)
        Try
            _canvasTimeDomain = New Bitmap(pictureBox.Width, pictureBox.Height)
            Dim offScreenDC As Graphics = Graphics.FromImage(_canvasTimeDomain)
            'Dim brush As SolidBrush = New System.Drawing.SolidBrush(Color.FromArgb(0, 0, 0))
            Dim pen As Pen = New System.Drawing.Pen(Color.WhiteSmoke)
            Dim width As Integer = _canvasTimeDomain.Width
            Dim height As Integer = _canvasTimeDomain.Height
            'Dim leftLeft As Integer = 0



            Dim yCenter As Double = (height) / 2
            Dim yScale As Double = height / 2 '0.5 * (height) / 32768
            Dim xPrev As Integer = 0, yPrevLeft As Integer = 0

            'For xAxis As Integer = leftLeft To width - 1
            For xAxis As Integer = 0 To width - 1
                Try
                    'Dim yAxis As Integer = CInt((yCenter + (_wave(_wave.Length / (width - leftLeft) * xAxis) * yScale)))
                    Dim yAxis As Integer = CInt((yCenter + (_wave(_wave.Length / width * xAxis) * yScale)))
                    If xAxis = 0 Then
                        xPrev = 0
                        yPrevLeft = yAxis
                    Else
                        pen.Color = Color.LimeGreen
                        offScreenDC.DrawLine(pen, xPrev, yPrevLeft, xAxis, yAxis)
                        xPrev = xAxis
                        yPrevLeft = yAxis
                    End If
                Catch ex As Exception

                End Try

            Next



            pictureBox.Image = _canvasTimeDomain
            offScreenDC.Dispose()
            'Application.DoEvents()
        Catch ex As Exception

        End Try

    End Sub




    Public Sub ImrimeTimeDomaiSimple(ByRef pictureBox As PictureBox, _wave1() As Double, color1 As Color, _wave2() As Double, color2 As Color)
        Try
            _canvasTimeDomain = New Bitmap(pictureBox.Width, pictureBox.Height)
            Dim offScreenDC As Graphics = Graphics.FromImage(_canvasTimeDomain)
            'Dim brush As SolidBrush = New System.Drawing.SolidBrush(Color.FromArgb(0, 0, 0))
            Dim pen As Pen = New System.Drawing.Pen(Color.WhiteSmoke)
            Dim width As Integer = _canvasTimeDomain.Width
            Dim height As Integer = _canvasTimeDomain.Height
            'Dim leftLeft As Integer = 0



            Dim yCenter As Double = (height) / 2
            Dim yScale As Double = 4 ' height / 2 '0.5 * (height) / 32768
             Dim XScale As Double = 4 
            Dim xPrev As Integer = 0, yPrevLeft As Integer = 0

            'For xAxis As Integer = leftLeft To width - 1
            For xAxis As Integer = 0 To width - 1
                Try
                    'Dim yAxis As Integer = CInt((yCenter + (_wave(_wave.Length / (width - leftLeft) * xAxis) * yScale)))
                    'Dim yAxis As Integer = CInt((yCenter + ((_wave1(_wave1.Length / width) * xAxis)) * yScale))
                    Dim yAxis As Integer = CInt((yCenter + (_wave1((_wave1.Length / width) * xAxis)) * yScale))
                    If xAxis = 0 Then
                        xPrev = 0
                        yPrevLeft = yAxis
                    Else
                        pen.Color = color1 ' Color.LimeGreen
                        offScreenDC.DrawLine(pen, CInt(xPrev * XScale), yPrevLeft, CInt(xAxis * XScale), yAxis)
                        xPrev = xAxis
                        yPrevLeft = yAxis
                    End If
                Catch ex As Exception

                End Try

            Next

            xPrev = 0
            yPrevLeft = 0
            pen = New System.Drawing.Pen(color2)

            'For xAxis As Integer = leftLeft To width - 1
            For xAxis As Integer = 0 To width - 1
                Try
                    'Dim yAxis As Integer = CInt((yCenter + (_wave(_wave.Length / (width - leftLeft) * xAxis) * yScale)))
                    Dim yAxis As Integer = CInt((yCenter + (_wave2((_wave2.Length / width) * xAxis)) * yScale))
                    If xAxis = 0 Then
                        xPrev = 0
                        yPrevLeft = yAxis
                    Else
                        pen.Color = color2 ' Color.LimeGreen
                        offScreenDC.DrawLine(pen, CInt(xPrev * XScale), yPrevLeft, CInt(xAxis * XScale), yAxis)
                        xPrev = xAxis
                        yPrevLeft = yAxis
                    End If
                Catch ex As Exception

                End Try

            Next


            pictureBox.Image = _canvasTimeDomain
            offScreenDC.Dispose()
            'Application.DoEvents()
        Catch ex As Exception

        End Try

    End Sub






End Class
