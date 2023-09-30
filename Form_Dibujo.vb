Public Class Form_Dibujo

    Private Canvas1 As Bitmap
    Private Grafica As New Class_GraficaLineal

    Dim NumerosAleatoriosOrigen() As Double




    Private Sub Form_Dibujo_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub




#Region "funciones dibujo basicas"

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try

            Me.Canvas1 = New Bitmap(PictureBox1.Width, PictureBox1.Height)
            Dim offScreenDC As Graphics = Graphics.FromImage(Canvas1)

            Dim percents = {10, 20, 70}
            Dim colors = {Color.Red, Color.CadetBlue, Color.Khaki}
            Using graphics = Me.Canvas1 ' Me.CreateGraphics()
                Dim location As New Point(100, 100)
                Dim size As New Size(150, 150)
                DrawPieChart(percents, colors, offScreenDC, location, size)
            End Using
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Draws a pie chart.
    ''' </summary>
    ''' <param name="percents"></param>
    ''' <param name="colors"></param>
    ''' <param name="surface"></param>
    ''' <param name="location"></param>
    ''' <param name="pieSize"></param>
    Public Sub DrawPieChart(ByVal percents() As Integer, ByVal colors() As Color, ByVal surface As Graphics, ByVal location As Point, ByVal pieSize As Size)

        Me.Canvas1 = New Bitmap(PictureBox1.Width, PictureBox1.Height)
        Dim offScreenDC As Graphics = Graphics.FromImage(Canvas1)
        ' Check if sections add up to 100.
        Dim sum = 0
        For Each percent In percents
            sum += percent
        Next

        If sum <> 100 Then
            Throw New ArgumentException("Percentages do not add up to 100.")
        End If

        If percents.Length <> colors.Length Then
            Throw New ArgumentException("There must be the same number of percents and colors.")
        End If

        Dim percentTotal = 0
        For percent = 0 To percents.Length() - 1
            Using brush As New SolidBrush(colors(percent))
                Try
                    offScreenDC.FillPie(brush, New Rectangle(location, pieSize), CSng(percentTotal * 360 / 100), CSng(percents(percent) * 360 / 100))
                Catch ex As Exception

                End Try
            End Using

            percentTotal += percents(percent)
        Next

        PictureBox1.Image = Me.Canvas1
        'offScreenDC.Dispose()




        Return

    End Sub

    Public Sub CirculosConcentricos(ByVal Coordenadas() As Integer, ByVal location As Point, ByVal colors() As Color, ByVal Graficos As Graphics)

        ' Create pen.
        Dim blackPen As New Pen(Color.Black, 3)

        ' Create array of points that define lines to draw.
        Dim points As PointF() = {New PointF(10.0F, 10.0F), New PointF(10.0F, 100.0F), New PointF(200.0F, 50.0F), New PointF(250.0F, 300.0F)}

        'Draw lines to screen.
        Graficos.DrawLines(blackPen, points)
        Dim CoordenadasAnteriores As New PointF(location.X, location.Y)

        Dim percentTotal = 0
        For percent = 0 To Coordenadas.Length() - 1


            'Using brush As New SolidBrush(colors(percent))
            '    Graficos.FillPie(brush, New Rectangle(location, pieSize), CSng(percentTotal * 360 / 100), CSng(Coordenadas(percent) * 360 / 100))
            'End Using
            'percentTotal += Coordenadas(percent)





        Next
        Return

    End Sub



#End Region

#Region "funciones matematicas 1"

#End Region

#Region "funciones matemáticas 2"


    Private Sub Button_Lemniscata_Click(sender As Object, e As EventArgs) Handles Button_Lemniscata.Click
        Try
            'Lemniscata de Gerono 
            'y^2 - x^2 * ( a^2 - x^2)

            'Lemniscata de Bernoulli
            '(x^2 + Y^2)^2 - 2 * a^2 * (x^2 - y^2)

            'Lemniscata de Booth
            '(x^2 + y^2)^2 - c * x^2 - d * y^2


            'Try


            '    Me.Canvas1 = New Bitmap(PictureBox1.Width, PictureBox1.Height)
            '    Dim offScreenDC As Graphics = Graphics.FromImage(Canvas1)

            '    Dim pen As Pen = New System.Drawing.Pen(Color.WhiteSmoke)
            '    Dim width As Integer = Me.Canvas1.Width
            '    Dim center As Integer = Me.Canvas1.Height / 2
            '    Dim height As Integer = Me.Canvas1.Height
            '    offScreenDC.DrawLine(pen, 0, center, width, center)

            '    Dim leftLeft As Integer = 0
            '    Dim leftTop As Integer = 0
            '    Dim leftRight As Integer = width
            '    Dim leftBottom As Integer = center - 1
            '    Dim rightLeft As Integer = 0
            '    Dim rightTop As Integer = center + 1
            '    Dim rightRight As Integer = width
            '    Dim rightBottom As Integer = height
            '    Dim yCenterLeft As Double = (leftBottom - leftTop) / 2
            '    Dim yScaleLeft As Double = 0.5 * (leftBottom - leftTop) / 32768
            '    Dim xPrevLeft As Integer = 0, yPrevLeft As Integer = 0

            '    Dim xCenterRight As Integer = rightTop + ((rightBottom - rightTop) / 2)
            '    Dim yScaleRight As Double = 0.5 * (rightBottom - rightTop) / 32768
            '    Dim xPrevRight As Integer = 0, yPrevRight As Integer = 0
            '    Dim yAxis As Integer = 0
            '    For n As Int32 = 1 To 180
            '        'y^ 2 - x ^ 2 * (a ^ 2 - x ^ 2) = 0
            '        For xAxis As Integer = rightLeft + 1 To rightRight - 1

            '            Dim a As Double = 45 / n
            '            yAxis = center + (a * xAxis * Math.Sqrt(Math.Pow(xAxis, 2) + 1))
            '            'y = ±(X^2 * Y^3 - (x2-1)3)^(1/2)
            '            If xAxis = 0 Then
            '                xPrevRight = 0
            '                yPrevRight = yAxis
            '            Else
            '                pen.Color = Color.LimeGreen


            '                Dim laPatata As Double = 1 - 4 * Math.Pow(yAxis, 2)
            '                If laPatata < 0 Then
            '                    laPatata *= -1
            '                End If
            '                Dim x2 As Integer = Math.Sqrt(2 * Math.Sqrt(laPatata) - 2) / 2
            '                offScreenDC.DrawLine(pen, x2, yAxis + 1, xAxis, yAxis)
            '                xPrevRight = xAxis
            '                yPrevRight = yAxis
            '            End If

            '        Next

            '    Next

            '    PictureBox1.Image = Me.Canvas1
            '    offScreenDC.Dispose()
            'Catch ex As Exception

            'End Try

            Me.Canvas1 = New Bitmap(PictureBox1.Width, PictureBox1.Height)
            Dim offScreenDC As Graphics = Graphics.FromImage(Canvas1)
            'Dim g As Graphics = Me.CreateGraphics()
            Dim pen As New Pen(Color.Black, 2)
            Dim points As New List(Of PointF)

            For i As Integer = 0 To 360 Step 1
                Dim x As Single = 100 + (100 * CSng((Math.Sqrt(2) * Math.Cos(i)) / (Math.Sin(i) ^ 2 + 1)))
                Dim y As Single = 100 + (100 * CSng((Math.Sqrt(2) * Math.Cos(i) * Math.Sin(i)) / (Math.Sin(i) ^ 2 + 1)))
                points.Add(New PointF(x, y))
            Next

            offScreenDC.DrawCurve(pen, points.ToArray())
            PictureBox1.Image = Me.Canvas1
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button_Amor_Click(sender As Object, e As EventArgs) Handles Button_Amor.Click

        ' (x^2+y^2-1)^3 - X^2 * Y^3 = 0 
        'y = ±(X^2 * Y^3 - (x2-1)3)^(1/2)


        Try


                    Me.Canvas1 = New Bitmap(PictureBox1.Width, PictureBox1.Height)
                    Dim offScreenDC As Graphics = Graphics.FromImage(Canvas1)

                    Dim pen As Pen = New System.Drawing.Pen(Color.WhiteSmoke)
                    Dim width As Integer = Me.Canvas1.Width
                    Dim center As Integer = Me.Canvas1.Height / 2
                    Dim height As Integer = Me.Canvas1.Height
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

                    Dim xCenterRight As Integer = rightTop + ((rightBottom - rightTop) / 2)
                    Dim yScaleRight As Double = 0.5 * (rightBottom - rightTop) / 32768
                    Dim xPrevRight As Integer = 0, yPrevRight As Integer = 0
                    Dim yAxis As Integer = 0
                    For xAxis As Integer = rightLeft + 1 To rightRight - 1

                        Dim a As Double = xAxis / rightRight
                yAxis = center + (Math.Pow(a, 2) * Math.Sin(1 / a)) / 2 ' Math.Sqrt(a)
                'y = ±(X^2 * Y^3 - (x2-1)3)^(1/2)
                If xAxis = 0 Then
                    xPrevRight = 0
                    yPrevRight = yAxis
                Else
                    pen.Color = Color.LimeGreen
                            offScreenDC.DrawLine(pen, xPrevRight, yPrevRight, xAxis, yAxis)
                            xPrevRight = xAxis
                            yPrevRight = yAxis
                        End If
                    Next

                    PictureBox1.Image = Me.Canvas1
                    offScreenDC.Dispose()
                Catch ex As Exception

                End Try


    End Sub



#End Region



#Region "funciones matemáticas 3"


    'doble obalo diagonal
    'sim(x^2+y^2) - cos(x*y) = 0
    'y * sin(x*y)+2*x*cos(x^2+y^2) 



    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        'circulos concentricos
        'sin(x^2 + y^2) = 0
        Try
            Me.Canvas1 = New Bitmap(PictureBox1.Width, PictureBox1.Height)
            Dim offScreenDC As Graphics = Graphics.FromImage(Canvas1)

            Dim pen As Pen = New System.Drawing.Pen(Color.WhiteSmoke)
            Dim width As Integer = Me.Canvas1.Width
            Dim center As Integer = Me.Canvas1.Height / 2
            Dim height As Integer = Me.Canvas1.Height
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

            'For xAxis As Integer = leftLeft To leftRight - 1
            '    Dim yAxis As Integer = CInt((yCenterLeft + (_waveLeft(_waveLeft.Length / (leftRight - leftLeft) * xAxis) * yScaleLeft)))

            '    If xAxis = 0 Then
            '        xPrevLeft = 0
            '        yPrevLeft = yAxis
            '    Else
            '        pen.Color = Color.LimeGreen
            '        offScreenDC.DrawLine(pen, xPrevLeft, yPrevLeft, xAxis, yAxis)
            '        xPrevLeft = xAxis
            '        yPrevLeft = yAxis
            '    End If
            'Next

            Dim xCenterRight As Integer = rightTop + ((rightBottom - rightTop) / 2)
            Dim yScaleRight As Double = 0.5 * (rightBottom - rightTop) / 32768
            Dim xPrevRight As Integer = 0, yPrevRight As Integer = 0
            Dim yAxis As Integer = 0
            For xAxis As Integer = rightLeft + 1 To rightRight - 1
                For indice As Int16 = 1 To 180


                    Dim lol As Int16 = 1
                    Dim a As Double = (Math.Pow(indice, 2) / Math.PI) * Math.PI - Math.Pow(xAxis, 2)

                    If a < 0 Then
                        yAxis = Math.Sqrt(a * -1)
                    Else
                        yAxis = Math.Sqrt(a)
                    End If

                    'Dim b As Double = Math.Pow(xAxis, 2)
                    ' Math.Sin(Math.Pow(yAxis, 2)) '= 0 CInt((xCenterRight + (_waveRight(_waveRight.Length / (rightRight - rightLeft) * xAxis) * yScaleRight)))

                    If xAxis = 0 Then
                        xPrevRight = 0
                        yPrevRight = yAxis
                    Else
                        pen.Color = Color.LimeGreen
                        offScreenDC.DrawLine(pen, xAxis + 1, yAxis + 1, xAxis, yAxis)
                        xPrevRight = xAxis
                        yPrevRight = yAxis
                    End If
                Next
            Next
            PictureBox1.Image = Me.Canvas1
            offScreenDC.Dispose()



            '' Create pen.
            'Dim blackPen As New Pen(Color.Black, 3)
            '' Create array of points that define lines to draw.
            'Dim points As PointF() = {New PointF(10.0F, 10.0F), New PointF(10.0F, 100.0F), New PointF(200.0F, 50.0F), New PointF(250.0F, 300.0F)}
            ''Draw lines to screen.
            'Graficos.DrawLines(blackPen, points)
            'Dim CoordenadasAnteriores As New PointF(Location.X, Location.Y)
            'Dim percentTotal = 0
            'For percent = 0 To Coordenadas.Length() - 1
            'Next

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button_CirculosConcentricos_Click(sender As Object, e As EventArgs) Handles Button_CirculosConcentricos.Click
        'circulos concentricos
        'sin(x^2 + y^2) = 0
        Try
            Me.Canvas1 = New Bitmap(PictureBox1.Width, PictureBox1.Height)
            Dim offScreenDC As Graphics = Graphics.FromImage(Canvas1)

            Dim pen As Pen = New System.Drawing.Pen(Color.WhiteSmoke)
            Dim width As Integer = Me.Canvas1.Width
            Dim center As Integer = Me.Canvas1.Height / 2
            Dim height As Integer = Me.Canvas1.Height
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

            'For xAxis As Integer = leftLeft To leftRight - 1
            '    Dim yAxis As Integer = CInt((yCenterLeft + (_waveLeft(_waveLeft.Length / (leftRight - leftLeft) * xAxis) * yScaleLeft)))

            '    If xAxis = 0 Then
            '        xPrevLeft = 0
            '        yPrevLeft = yAxis
            '    Else
            '        pen.Color = Color.LimeGreen
            '        offScreenDC.DrawLine(pen, xPrevLeft, yPrevLeft, xAxis, yAxis)
            '        xPrevLeft = xAxis
            '        yPrevLeft = yAxis
            '    End If
            'Next

            Dim xCenterRight As Integer = rightTop + ((rightBottom - rightTop) / 2)
            Dim yScaleRight As Double = 0.5 * (rightBottom - rightTop) / 32768
            Dim xPrevRight As Integer = 0, yPrevRight As Integer = 0
            Dim yAxis As Integer = 0
            For xAxis As Integer = rightLeft + 1 To rightRight - 1

                Dim lol As Int16 = 1
                Dim a As Double = xAxis * Math.PI - Math.Pow(lol, 2)

                If a < 0 Then
                    yAxis = Math.Sqrt(a * -1)
                Else
                    yAxis = Math.Sqrt(a)
                End If

                'Dim b As Double = Math.Pow(xAxis, 2)
                ' Math.Sin(Math.Pow(yAxis, 2)) '= 0 CInt((xCenterRight + (_waveRight(_waveRight.Length / (rightRight - rightLeft) * xAxis) * yScaleRight)))

                If xAxis = 0 Then
                    xPrevRight = 0
                    yPrevRight = yAxis
                Else
                    pen.Color = Color.LimeGreen
                    offScreenDC.DrawLine(pen, xPrevRight, yPrevRight, xAxis, yAxis)
                    xPrevRight = xAxis
                    yPrevRight = yAxis
                End If
            Next

            PictureBox1.Image = Me.Canvas1
            offScreenDC.Dispose()



            '' Create pen.
            'Dim blackPen As New Pen(Color.Black, 3)
            '' Create array of points that define lines to draw.
            'Dim points As PointF() = {New PointF(10.0F, 10.0F), New PointF(10.0F, 100.0F), New PointF(200.0F, 50.0F), New PointF(250.0F, 300.0F)}
            ''Draw lines to screen.
            'Graficos.DrawLines(blackPen, points)
            'Dim CoordenadasAnteriores As New PointF(Location.X, Location.Y)
            'Dim percentTotal = 0
            'For percent = 0 To Coordenadas.Length() - 1
            'Next

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'circulos concentricos equiespaciados
        'sin(sqr(x^2 + y^2)) = 0
        'y = ±√(nπ - x^2)
        Try


            Me.Canvas1 = New Bitmap(PictureBox1.Width, PictureBox1.Height)
            Dim offScreenDC As Graphics = Graphics.FromImage(Canvas1)

            Dim pen As Pen = New System.Drawing.Pen(Color.WhiteSmoke)
            Dim width As Integer = Me.Canvas1.Width
            Dim center As Integer = Me.Canvas1.Height / 2
            Dim height As Integer = Me.Canvas1.Height
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

            Dim xCenterRight As Integer = rightTop + ((rightBottom - rightTop) / 2)
            Dim yScaleRight As Double = 0.5 * (rightBottom - rightTop) / 32768
            Dim xPrevRight As Integer = 0, yPrevRight As Integer = 0
            Dim yAxis As Integer = 0



            For indice As Int16 = 1 To 20


                For xAxis As Integer = rightLeft + 1 To rightRight - 1

                    Dim n As Double = (Math.Pow(xAxis, 2) / Math.PI) * 1000
                    Dim lol2 As Double = n / rightRight
                    'y = ±√(nπ - x^2)

                    Dim a As Double = n * Math.PI - Math.Pow(lol2, 2)


                    'sin(sqr(x ^ 2 + y ^ 2)) = 0
                    'y = ±√(nπ - x^2)

                    Try
                        If a > 0 Then
                            yAxis = Math.Sqrt(a)
                        Else
                            yAxis = Math.Sqrt(a * -1)
                        End If
                    Catch ex As Exception

                    End Try

                    'Dim b As Double = Math.Pow(xAxis, 2)
                    ' Math.Sin(Math.Pow(yAxis, 2)) '= 0 CInt((xCenterRight + (_waveRight(_waveRight.Length / (rightRight - rightLeft) * xAxis) * yScaleRight)))

                    If xAxis = 0 Then
                        xPrevRight = 0
                        yPrevRight = yAxis
                    Else
                        pen.Color = Color.LimeGreen
                        offScreenDC.DrawLine(pen, xAxis + 1, CInt(yAxis / 1000), xAxis, CInt(yAxis / 1000))
                        xPrevRight = xAxis
                        yPrevRight = yAxis
                    End If
                Next
            Next
            PictureBox1.Image = Me.Canvas1
            offScreenDC.Dispose()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'oscilador infinito
        'y = x * sin(1/x)



        Try


                Me.Canvas1 = New Bitmap(PictureBox1.Width, PictureBox1.Height)
                Dim offScreenDC As Graphics = Graphics.FromImage(Canvas1)

                Dim pen As Pen = New System.Drawing.Pen(Color.WhiteSmoke)
                Dim width As Integer = Me.Canvas1.Width
                Dim center As Integer = Me.Canvas1.Height / 2
                Dim height As Integer = Me.Canvas1.Height
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

                Dim xCenterRight As Integer = rightTop + ((rightBottom - rightTop) / 2)
                Dim yScaleRight As Double = 0.5 * (rightBottom - rightTop) / 32768
                Dim xPrevRight As Integer = 0, yPrevRight As Integer = 0
                Dim yAxis As Integer = 0
                For xAxis As Integer = rightLeft + 1 To rightRight - 1

                    Dim a As Double = xAxis / rightRight
                yAxis = center + (xAxis * Math.Sin(1 / a))  ' Math.Sqrt(a)


                'Dim b As Double = Math.Pow(xAxis, 2)
                ' Math.Sin(Math.Pow(yAxis, 2)) '= 0 CInt((xCenterRight + (_waveRight(_waveRight.Length / (rightRight - rightLeft) * xAxis) * yScaleRight)))

                If xAxis = 0 Then
                        xPrevRight = 0
                        yPrevRight = yAxis
                    Else
                        pen.Color = Color.LimeGreen
                        offScreenDC.DrawLine(pen, xPrevRight, yPrevRight, xAxis, yAxis)
                        xPrevRight = xAxis
                        yPrevRight = yAxis
                    End If
                Next

                PictureBox1.Image = Me.Canvas1
                offScreenDC.Dispose()
            Catch ex As Exception

            End Try


    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button_rotangulo.Click
        Try


            Me.Canvas1 = New Bitmap(PictureBox1.Width, PictureBox1.Height)
            Dim offScreenDC As Graphics = Graphics.FromImage(Canvas1)

            Dim pen As Pen = New System.Drawing.Pen(Color.WhiteSmoke)
            Dim width As Integer = Me.Canvas1.Width
            Dim center As Integer = Me.Canvas1.Height / 2
            Dim height As Integer = Me.Canvas1.Height
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

            Dim xCenterRight As Integer = rightTop + ((rightBottom - rightTop) / 2)
            Dim yScaleRight As Double = 0.5 * (rightBottom - rightTop) / 32768
            Dim xPrevRight As Integer = 0, yPrevRight As Integer = 0
            Dim yAxis As Integer = 0
            For n As Int16 = 1 To 90
                For xAxis As Integer = rightLeft + 1 To rightRight - 1

                    Dim a As Double = xAxis / rightRight
                    yAxis = center + (xAxis + Math.PI * n) ' Math.Sqrt(a)

                    If xAxis = 0 Then
                        xPrevRight = 0
                        yPrevRight = yAxis
                    Else
                        pen.Color = Color.LimeGreen
                        'TextBox_rotangulo.Text
                        Dim xIto As Int32 = (xAxis * Math.Cos(180 / TextBox_rotangulo.Text)) - (yAxis * Math.Sin(180 / TextBox_rotangulo.Text))
                        Dim yIto As Int32 = (xAxis * Math.Sin(180 / TextBox_rotangulo.Text)) + (yAxis * Math.Cos(180 / TextBox_rotangulo.Text))
                        offScreenDC.DrawLine(pen, xIto + 1, yIto + 1, xIto, yIto)
                        xPrevRight = xAxis
                        yPrevRight = yAxis
                    End If
                Next
            Next
            PictureBox1.Image = Me.Canvas1
            offScreenDC.Dispose()
        Catch ex As Exception

        End Try
    End Sub







#End Region

    Private Sub Button_Circulo3Puntos_Click(sender As Object, e As EventArgs) Handles Button_Circulo3Puntos.Click
        'y = a · sen(b·x+c)+d
        'a = (y - d) / sen(b·x+c)


        'Para calcular el centro de un círculo a partir de tres puntos en su perímetro, puedes utilizar la fórmula de la circunferencia circunscrita a un triángulo.
        'Supongamos que los tres puntos son A (x1, y1), B (x2, y2) y C (x3, y3). Primero, calcula las longitudes de los lados del triángulo formado por estos tres puntos utilizando la fórmula de distancia entre dos puntos.
        'Llamemos a estas longitudes a, b y c.     Distancia = sqrt((x2 - x1)^2 + (y2 - y1)^2)
        'Luego, calcula el semiperímetro del triángulo, que se define como s = (a + b + c) / 2. A continuación, calcula el área del triángulo utilizando la fórmula de Herón: Area = sqrt(s * (s - a) * (s - b) * (s - c)).
        'Una vez que se conoce el área del triángulo, se puede calcular el radio del círculo circunscrito utilizando la fórmula R = (a * b * c) / (4 * Area). Finalmente, se puede calcular el centro del círculo utilizando las fórmulas
        'Cx = (a ^ 2 * (b ^ 2 + c ^ 2 - a ^ 2) * x1 + b ^ 2 * (c ^ 2 + a ^ 2 - b ^ 2) * x2 + c ^ 2 * (a ^ 2 + b ^ 2 - c ^ 2) * x3) / (16 * Area ^ 2)
        'Cy = (a ^ 2 * (b ^ 2 + c ^ 2 - a ^ 2) * y1 + b ^ 2 * (c ^ 2 + a ^ 2 - b ^ 2) * y2 + c ^ 2 * (a ^ 2 + b ^ 2 - c ^ 2) * y3) / (16 * Area ^ 2)
        'Donde(Cx, Cy) son las coordenadas del centro del círculo. Espero que esto te ayude. ¿Hay algo más en lo que pueda ayudarte?



        'Ecuación de un círculo en forma estándar
        '(x−95)^2+(y−115)^2=249.988
        'Ecuación de un círculo en forma general
        'x^2−190x+y^2−230y+22000.012=0
        'Ecuaciones paramétricas de un círculo
        'x = 15.811cosθ+95
        'y = 15.811sinθ+115



        Me.Canvas1 = New Bitmap(PictureBox1.Width, PictureBox1.Height)
        Dim offScreenDC As Graphics = Graphics.FromImage(Canvas1)
        Dim pen As Pen = New System.Drawing.Pen(Color.Aqua)
        Dim penTriangulo As Pen = New System.Drawing.Pen(Color.DarkOrange)
        Dim width As Integer = Me.Canvas1.Width
        Dim center As Integer = Me.Canvas1.Height / 2
        Dim height As Integer = Me.Canvas1.Height
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
        Dim xCenterRight As Integer = rightTop + ((rightBottom - rightTop) / 2)
        Dim yScaleRight As Double = 0.5 * (rightBottom - rightTop) / 32768
        Dim xPrevRight As Integer = 0, yPrevRight As Integer = 0
        Dim yAxis As Integer = 0



        Dim punto1 As New Point(100, 100)
        Dim punto2 As New Point(110, 110)
        Dim punto3 As New Point(100, 120)

        offScreenDC.DrawLine(penTriangulo, punto1.X, punto1.Y, punto2.X, punto2.Y)
        offScreenDC.DrawLine(penTriangulo, punto2.X, punto2.Y, punto3.X, punto3.Y)
        offScreenDC.DrawLine(penTriangulo, punto3.X, punto3.Y, punto1.X, punto1.Y)

        Try

            Dim Trianbulo1A As Integer = 0
            Dim Trianbulo1B As Integer = 0
            Dim Trianbulo1C As Integer = 0



            Trianbulo1A = Math.Sqrt(Math.Pow((punto2.X - punto1.X), 2) + Math.Pow((punto2.Y - punto1.Y), 2))


            Trianbulo1B = Math.Sqrt(Math.Pow((punto3.X - punto2.X), 2) + Math.Pow((punto3.Y - punto2.Y), 2))

            Trianbulo1C = Math.Sqrt(Math.Pow((punto1.X - punto3.X), 2) + Math.Pow((punto1.Y - punto3.Y), 2))



            'Trianbulo1A = Math.Pow((punto1.X - punto2.X) + (punto1.Y - punto2.Y), 0.5)
            'Dim Trianbulo1A As Integer = Math.Pow((punto1.X - punto2.X) + (punto1.Y - punto2.Y), 0.5) 'Math.Sqrt((punto1.X - punto2.X) + (punto1.Y - punto2.Y))
            'Dim Trianbulo1B As Integer = Math.Pow((punto2.X - punto3.X) + (punto2.Y - punto3.Y), 0.5) 'Math.Sqrt((punto2.X - punto3.X) + (punto2.Y - punto3.Y))
            'Dim Trianbulo1C As Integer = Math.Pow((punto3.X - punto1.X) + (punto3.Y - punto1.Y), 0.5) ' Math.Sqrt((punto3.X - punto1.X) + (punto3.Y - punto1.Y))


            Dim PerimetroS As Double = (Trianbulo1C + Trianbulo1B + Trianbulo1C) / 2

            'Dim CatetoH As Double = Math.Sqrt(Math.Pow(Trianbulo1C / 2, 2) + Math.Pow((Trianbulo1B + Trianbulo1A) / 2, 2))
            Dim CatetoH As Double = Math.Sqrt(Math.Pow(Trianbulo1B, 2) - Math.Pow(Trianbulo1C / 2, 2))
            Dim AreaReall As Double = (Trianbulo1C * CatetoH) / 2

            Dim Restando As Double = ((PerimetroS - Trianbulo1A) * (PerimetroS - Trianbulo1B) * (PerimetroS - Trianbulo1C))
            Dim Area As Double = Math.Sqrt(PerimetroS * Restando)
            'Cx =               (a ^ 2 * (b ^ 2 + c ^ 2 - a ^ 2)                                                                              *        x1 + b ^ 2                   * (c ^ 2 + a ^ 2 - b ^ 2)                                                          * x2 + c ^ 2                          * (a ^ 2 + b ^ 2 - c ^ 2)                                                          * x3)       / (16 * Area ^ 2)
            Dim Cx As Double = ((Math.Pow(Trianbulo1A, 2) * (Math.Pow(Trianbulo1B, 2) + Math.Pow(Trianbulo1C, 2) - Math.Pow(Trianbulo1A, 2))) * punto1.X + Math.Pow(Trianbulo1B, 2) * (Math.Pow(Trianbulo1C, 2) + Math.Pow(Trianbulo1A, 2) - Math.Pow(Trianbulo1B, 2)) * punto2.X + Math.Pow(Trianbulo1C, 2) * (Math.Pow(Trianbulo1A, 2) + Math.Pow(Trianbulo1B, 2) - Math.Pow(Trianbulo1C, 2)) * punto3.X) / (16 * Math.Pow(AreaReall, 2))
            'Cy =               (a ^ 2 * (b ^ 2 + c ^ 2 - a ^ 2)                                                                              * y1 + b ^ 2                          * (c ^ 2 + a ^ 2 - b ^ 2)                                                          * y2 + c ^ 2                          * (a ^ 2 + b ^ 2 - c ^ 2)                                                          * y3)       / (16 * Area ^ 2)
            Dim Cy As Double = ((Math.Pow(Trianbulo1A, 2) * (Math.Pow(Trianbulo1B, 2) + Math.Pow(Trianbulo1C, 2) - Math.Pow(Trianbulo1A, 2))) * punto1.Y + Math.Pow(Trianbulo1B, 2) * (Math.Pow(Trianbulo1C, 2) + Math.Pow(Trianbulo1A, 2) - Math.Pow(Trianbulo1B, 2)) * punto2.Y + Math.Pow(Trianbulo1C, 2) * (Math.Pow(Trianbulo1A, 2) + Math.Pow(Trianbulo1B, 2) - Math.Pow(Trianbulo1C, 2)) * punto3.Y) / (16 * Math.Pow(AreaReall, 2))


            Dim radio As Double = Math.Sqrt(Math.Pow(punto3.X, 2) + Math.Pow(punto3.Y, 2) - Trianbulo1C)
            'R = (a * b * c) / (4 * Area)
            Dim radio0 As Double = (Trianbulo1C * Trianbulo1B * Trianbulo1A) / (4 * Area)


            Dim rectangulo As New Rectangle(Cx - 2, Cy - 2, 4, 4)
            offScreenDC.DrawArc(pen, rectangulo, 0, 360)
            offScreenDC.DrawRectangle(pen, rectangulo)

            Dim penCentorTrian As Pen = New System.Drawing.Pen(Color.Black)
            Dim CentroTriangulo As New Point((punto1.X + punto2.X + punto3.X) / 3, (punto1.Y + punto2.Y + punto3.Y) / 3)
            rectangulo = New Rectangle(CentroTriangulo.X - 1, CentroTriangulo.Y - 1, 2, 2)
            offScreenDC.DrawArc(penCentorTrian, rectangulo, 0, 360)









            Dim ma As Double = (punto2.Y - punto1.Y) / (punto2.X - punto1.X)
            Dim mb As Double = (punto3.Y - punto2.Y) / (punto3.X - punto2.X)

            Dim centerX As Double = (ma * mb * (punto1.Y - punto3.Y) + mb * (punto1.X + punto2.X) - ma * (punto2.X + punto3.X)) / (2 * (mb - ma))
            Dim centerY As Double = (-1 / ma) * (centerX - (punto1.X + punto2.X) / 2) + (punto1.Y + punto2.Y) / 2




            Dim penCentorTrian1 As Pen = New System.Drawing.Pen(Color.Red)
            Dim CentroTriangulo1 As New Point((punto1.X + punto2.X + punto3.X) / 3, (punto1.Y + punto2.Y + punto3.Y) / 3)
            rectangulo = New Rectangle(centerX - 1, centerY - 1, 2, 2)
            offScreenDC.DrawArc(penCentorTrian1, rectangulo, 0, 360)
            rectangulo = New Rectangle(centerX - 2, centerY - 2, 4, 4)
            offScreenDC.DrawArc(penCentorTrian1, rectangulo, 0, 360)




            PictureBox1.Image = Me.Canvas1
            offScreenDC.Dispose()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button_fourier_Click(sender As Object, e As EventArgs) Handles Button_fourier.Click
        Try
            Dim Waves() As Double = New Double() {}

            Dim N As Integer = Waves.Length

            'Dim n As Integer

        Catch ex As Exception

        End Try








        '        import numpy as np

        '# REFERENCES
        '#https://towardsdatascience.com/fast-fourier-transform-937926E591cb

        '#https://pythonnumericalmethods.berkeley.edu/notebooks/chapter24.03-Fast-Fourier-Transform.html
        '#https://jakevdp.github.io/blog/2013/08/28/understanding-the-fft/


        '        def show_M(N):
        '    """
        '    N: int 
        '    """

        '    n = np.arange(N)
        '        k = n.reshape((N, 1))

        '        M = k * n
        '        Print("M:", M)


        '        def get_data(Len): 
        '    """
        '    len: int 
        '        lenght of data 
        '    """
        '    Data = np.random.random(Len)
        '        Return Data

        '        def get_circular_terms(N): 
        '    """
        '    N: int 
        '    """

        '    terms = np.exp(-1j *2*np.pi * np.arange(N) / N)

        '    Return terms

        '        def discrete_fourier_transform(Data): 
        '    """
        '    data: np.array 
        '        1 dimensional array
        '    """
        '    #len Of data
        '    N = Data.shape[0] 

        '    n = np.arange(N)
        '        k = n.reshape((N, 1))
        '        M = np.exp(-1j * 2*np.pi * k * n/N)

        '        Return np.dot(M, Data)

        '        def fast_fourier_transform(Data): 
        '    """
        '    data: np.array  
        '        data as 1D array
        '    return discrete fourier transform of data
        '    """

        '    # len Of data
        '    N = Data.shape[0]

        '    # Must be a power Of 2
        '    Assert   N % 2 == 0, 'len of data: {} must be a power of 2'.format(N)

        '    If N <= 2 Then
        '            Return discrete_fourier_transform(Data)

        '        Else
        '            data_even = fast_fourier_transform(Data[:: 2])
        '        data_odd = fast_fourier_transform(Data[1:: 2])
        '        terms = get_circular_terms(N)

        '            Return np.concatenate(
        '            [
        '            data_even +terms[: N// 2] * data_odd,
        '            data_even + terms[N//2: ] * data_odd 
        '            ])


        'N = 4

        '            X = get_data(N)
        '            Print("Data: ", X)

        '            dt = discrete_fourier_transform(X)
        '            fdft = fast_fourier_transform(X)
        '            dtnp = np.fft.fft(X)

        '            Print('DFT:',fdft)

        '            Print(np.allclose(dt, dtnp),
        '    np.allclose(fdft, dtnp))

        '            Print("")
        '            show_M(N)

    End Sub

    Private Sub ButtonCirculo3Puntos_V2_Click(sender As Object, e As EventArgs) Handles ButtonCirculo3Puntos_V2.Click
        'y = a · sen(b·x+c)+d
        'a = (y - d) / sen(b·x+c)


        'Para calcular el centro de un círculo a partir de tres puntos en su perímetro, puedes utilizar la fórmula de la circunferencia circunscrita a un triángulo.
        'Supongamos que los tres puntos son A (x1, y1), B (x2, y2) y C (x3, y3). Primero, calcula las longitudes de los lados del triángulo formado por estos tres puntos utilizando la fórmula de distancia entre dos puntos.
        'Llamemos a estas longitudes a, b y c.     Distancia = sqrt((x2 - x1)^2 + (y2 - y1)^2)
        'Luego, calcula el semiperímetro del triángulo, que se define como s = (a + b + c) / 2. A continuación, calcula el área del triángulo utilizando la fórmula de Herón: Area = sqrt(s * (s - a) * (s - b) * (s - c)).
        'Una vez que se conoce el área del triángulo, se puede calcular el radio del círculo circunscrito utilizando la fórmula R = (a * b * c) / (4 * Area). Finalmente, se puede calcular el centro del círculo utilizando las fórmulas
        'Cx = (a ^ 2 * (b ^ 2 + c ^ 2 - a ^ 2) * x1 + b ^ 2 * (c ^ 2 + a ^ 2 - b ^ 2) * x2 + c ^ 2 * (a ^ 2 + b ^ 2 - c ^ 2) * x3) / (16 * Area ^ 2)
        'Cy = (a ^ 2 * (b ^ 2 + c ^ 2 - a ^ 2) * y1 + b ^ 2 * (c ^ 2 + a ^ 2 - b ^ 2) * y2 + c ^ 2 * (a ^ 2 + b ^ 2 - c ^ 2) * y3) / (16 * Area ^ 2)
        'Donde(Cx, Cy) son las coordenadas del centro del círculo. Espero que esto te ayude. ¿Hay algo más en lo que pueda ayudarte?



        'Ecuación de un círculo en forma estándar
        '(x−95)^2+(y−115)^2=249.988
        'Ecuación de un círculo en forma general
        'x^2−190x+y^2−230y+22000.012=0
        'Ecuaciones paramétricas de un círculo
        'x = 15.811cosθ+95
        'y = 15.811sinθ+115



        Me.Canvas1 = New Bitmap(PictureBox1.Width, PictureBox1.Height)
        Dim offScreenDC As Graphics = Graphics.FromImage(Canvas1)
        Dim pen As Pen = New System.Drawing.Pen(Color.Aqua)
        Dim penTriangulo As Pen = New System.Drawing.Pen(Color.DarkOrange)
        Dim width As Integer = Me.Canvas1.Width
        Dim center As Integer = Me.Canvas1.Height / 2
        Dim height As Integer = Me.Canvas1.Height
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
        Dim xCenterRight As Integer = rightTop + ((rightBottom - rightTop) / 2)
        Dim yScaleRight As Double = 0.5 * (rightBottom - rightTop) / 32768
        Dim xPrevRight As Integer = 0, yPrevRight As Integer = 0
        Dim yAxis As Integer = 0



        Dim punto1 As New Point(100, 100)
        Dim punto2 As New Point(110, 110)
        Dim punto3 As New Point(100, 120)

        offScreenDC.DrawLine(penTriangulo, punto1.X, punto1.Y, punto2.X, punto2.Y)
        offScreenDC.DrawLine(penTriangulo, punto2.X, punto2.Y, punto3.X, punto3.Y)
        offScreenDC.DrawLine(penTriangulo, punto3.X, punto3.Y, punto1.X, punto1.Y)

        Try

            Dim Trianbulo1A As Integer = 0
            Dim Trianbulo1B As Integer = 0
            Dim Trianbulo1C As Integer = 0


            Dim mayor As Int16 = 0
            If punto1.X > mayor Then
                mayor = 1
            End If
            If punto2.X > mayor Then
                mayor = 2
            End If
            If punto3.X > mayor Then
                mayor = 3
            End If


            Dim resta_x As Double = Math.Pow(punto1.X - punto2.X, 2)
            Dim resta_y As Double = Math.Pow(punto1.Y - punto2.Y, 2)
            If (resta_x + resta_y) > 0 Then
                Trianbulo1A = Math.Sqrt((resta_x) + (resta_y))
            Else
                Trianbulo1A = Math.Sqrt(Math.Pow((punto2.X - punto1.X), 2) + Math.Pow((punto2.Y - punto1.Y), 2))
            End If
            'Trianbulo1A = Math.Sqrt((punto1.X - punto2.X) + (punto1.Y - punto2.Y))
            'Trianbulo1B = Math.Sqrt((punto2.X - punto3.X) + (punto2.Y - punto3.Y))
            'Trianbulo1C = Math.Sqrt((punto3.X - punto1.X) + (punto3.Y - punto1.Y))

            resta_x = Math.Pow(punto2.X - punto3.X, 2)
            resta_y = Math.Pow(punto2.Y - punto3.Y, 2)
            If (resta_x + resta_y) > 0 Then
                Trianbulo1B = Math.Sqrt((resta_x) + (resta_y))
            Else
                Trianbulo1B = Math.Sqrt(Math.Pow((punto3.X - punto2.X), 2) + Math.Pow((punto3.Y - punto2.Y), 2))
            End If


            resta_x = Math.Pow(punto3.X - punto1.X, 2)
            resta_y = Math.Pow(punto3.Y - punto1.Y, 2)
            If (resta_x + resta_y) > 0 Then
                Trianbulo1C = Math.Sqrt((resta_x) + (resta_y))
            Else
                Trianbulo1C = Math.Sqrt(Math.Pow((punto1.X - punto3.X), 2) + Math.Pow((punto1.Y - punto3.Y), 2))
            End If


            'Trianbulo1A = Math.Pow((punto1.X - punto2.X) + (punto1.Y - punto2.Y), 0.5)
            'Dim Trianbulo1A As Integer = Math.Pow((punto1.X - punto2.X) + (punto1.Y - punto2.Y), 0.5) 'Math.Sqrt((punto1.X - punto2.X) + (punto1.Y - punto2.Y))
            'Dim Trianbulo1B As Integer = Math.Pow((punto2.X - punto3.X) + (punto2.Y - punto3.Y), 0.5) 'Math.Sqrt((punto2.X - punto3.X) + (punto2.Y - punto3.Y))
            'Dim Trianbulo1C As Integer = Math.Pow((punto3.X - punto1.X) + (punto3.Y - punto1.Y), 0.5) ' Math.Sqrt((punto3.X - punto1.X) + (punto3.Y - punto1.Y))


            Dim PerimetroS As Double = (Trianbulo1A + Trianbulo1B + Trianbulo1C) / 2



            Dim Restando As Double = ((PerimetroS - Trianbulo1A) * (PerimetroS - Trianbulo1B) * (PerimetroS - Trianbulo1C))
            Dim Area As Double = Math.Sqrt(PerimetroS * Restando)
            'Cx =               (a ^ 2 * (b ^ 2 + c ^ 2 - a ^ 2)                                                                              *        x1 + b ^ 2                   * (c ^ 2 + a ^ 2 - b ^ 2)                                                          * x2 + c ^ 2                          * (a ^ 2 + b ^ 2 - c ^ 2)                                                          * x3)       / (16 * Area ^ 2)
            Dim Cx As Double = ((Math.Pow(Trianbulo1A, 2) * (Math.Pow(Trianbulo1B, 2) + Math.Pow(Trianbulo1C, 2) - Math.Pow(Trianbulo1A, 2))) * punto1.X + Math.Pow(Trianbulo1B, 2) * (Math.Pow(Trianbulo1C, 2) + Math.Pow(Trianbulo1A, 2) - Math.Pow(Trianbulo1B, 2)) * punto2.X + Math.Pow(Trianbulo1C, 2) * (Math.Pow(Trianbulo1A, 2) + Math.Pow(Trianbulo1B, 2) - Math.Pow(Trianbulo1C, 2)) * punto3.X) / (16 * Math.Pow(Area, 2))
            'Cy =               (a ^ 2 * (b ^ 2 + c ^ 2 - a ^ 2)                                                                              * y1 + b ^ 2                          * (c ^ 2 + a ^ 2 - b ^ 2)                                                          * y2 + c ^ 2                          * (a ^ 2 + b ^ 2 - c ^ 2)                                                          * y3)       / (16 * Area ^ 2)
            Dim Cy As Double = ((Math.Pow(Trianbulo1A, 2) * (Math.Pow(Trianbulo1B, 2) + Math.Pow(Trianbulo1C, 2) - Math.Pow(Trianbulo1A, 2))) * punto1.Y + Math.Pow(Trianbulo1B, 2) * (Math.Pow(Trianbulo1C, 2) + Math.Pow(Trianbulo1A, 2) - Math.Pow(Trianbulo1B, 2)) * punto2.Y + Math.Pow(Trianbulo1C, 2) * (Math.Pow(Trianbulo1A, 2) + Math.Pow(Trianbulo1B, 2) - Math.Pow(Trianbulo1C, 2)) * punto3.Y) / (16 * Math.Pow(Area, 2))


            Dim radio As Double = Math.Sqrt(Math.Pow(punto3.X, 2) + Math.Pow(punto3.Y, 2) - Trianbulo1C)

            Dim rectangulo As New Rectangle(Cx - 2, Cy - 2, 4, 4)
            offScreenDC.DrawArc(pen, rectangulo, 0, 360)
            offScreenDC.DrawRectangle(pen, rectangulo)

            Dim penCentorTrian As Pen = New System.Drawing.Pen(Color.Black)
            Dim CentroTriangulo As New Point((punto1.X + punto2.X + punto3.X) / 3, (punto1.Y + punto2.Y + punto3.Y) / 3)
            rectangulo = New Rectangle(CentroTriangulo.X - 2, CentroTriangulo.Y - 2, 4, 4)
            offScreenDC.DrawArc(pen, rectangulo, 0, 360)


            PictureBox1.Image = Me.Canvas1
            offScreenDC.Dispose()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button_Circulo3PuntosOK_Click(sender As Object, e As EventArgs) Handles Button_Circulo3PuntosOK.Click
        'y = a · sen(b·x+c)+d
        'a = (y - d) / sen(b·x+c)


        'Para calcular el centro de un círculo a partir de tres puntos en su perímetro, puedes utilizar la fórmula de la circunferencia circunscrita a un triángulo.
        'Supongamos que los tres puntos son A (x1, y1), B (x2, y2) y C (x3, y3). Primero, calcula las longitudes de los lados del triángulo formado por estos tres puntos utilizando la fórmula de distancia entre dos puntos.
        'Llamemos a estas longitudes a, b y c.     Distancia = sqrt((x2 - x1)^2 + (y2 - y1)^2)
        'Luego, calcula el semiperímetro del triángulo, que se define como s = (a + b + c) / 2. A continuación, calcula el área del triángulo utilizando la fórmula de Herón: Area = sqrt(s * (s - a) * (s - b) * (s - c)).
        'Una vez que se conoce el área del triángulo, se puede calcular el radio del círculo circunscrito utilizando la fórmula R = (a * b * c) / (4 * Area). Finalmente, se puede calcular el centro del círculo utilizando las fórmulas
        'Cx = (a ^ 2 * (b ^ 2 + c ^ 2 - a ^ 2) * x1 + b ^ 2 * (c ^ 2 + a ^ 2 - b ^ 2) * x2 + c ^ 2 * (a ^ 2 + b ^ 2 - c ^ 2) * x3) / (16 * Area ^ 2)
        'Cy = (a ^ 2 * (b ^ 2 + c ^ 2 - a ^ 2) * y1 + b ^ 2 * (c ^ 2 + a ^ 2 - b ^ 2) * y2 + c ^ 2 * (a ^ 2 + b ^ 2 - c ^ 2) * y3) / (16 * Area ^ 2)
        'Donde(Cx, Cy) son las coordenadas del centro del círculo. Espero que esto te ayude. ¿Hay algo más en lo que pueda ayudarte?



        'Ecuación de un círculo en forma estándar
        '(x−95)^2+(y−115)^2=249.988
        'Ecuación de un círculo en forma general
        'x^2−190x+y^2−230y+22000.012=0
        'Ecuaciones paramétricas de un círculo
        'x = 15.811cosθ+95
        'y = 15.811sinθ+115



        Me.Canvas1 = New Bitmap(PictureBox1.Width, PictureBox1.Height)
        Dim offScreenDC As Graphics = Graphics.FromImage(Canvas1)
        Dim pen As Pen = New System.Drawing.Pen(Color.Gray)
        Dim penTriangulo As Pen = New System.Drawing.Pen(Color.DarkOrange)
        Dim width As Integer = Me.Canvas1.Width
        Dim center As Integer = Me.Canvas1.Height / 2
        Dim height As Integer = Me.Canvas1.Height
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
        Dim xCenterRight As Integer = rightTop + ((rightBottom - rightTop) / 2)
        Dim yScaleRight As Double = 0.5 * (rightBottom - rightTop) / 32768
        Dim xPrevRight As Integer = 0, yPrevRight As Integer = 0
        Dim yAxis As Integer = 0



        Dim punto1 As New Point(100, 50)
        Dim punto2 As New Point(90, 130)
        Dim punto3 As New Point(100, 200)

        offScreenDC.DrawLine(penTriangulo, punto1.X, punto1.Y, punto2.X, punto2.Y)
        offScreenDC.DrawLine(penTriangulo, punto2.X, punto2.Y, punto3.X, punto3.Y)
        offScreenDC.DrawLine(penTriangulo, punto3.X, punto3.Y, punto1.X, punto1.Y)

        Try

            Dim Trianbulo1A As Integer = 0
            Dim Trianbulo1B As Integer = 0
            Dim Trianbulo1C As Integer = 0

            Trianbulo1A = Math.Sqrt(Math.Pow((punto2.X - punto1.X), 2) + Math.Pow((punto2.Y - punto1.Y), 2))
            Trianbulo1B = Math.Sqrt(Math.Pow((punto3.X - punto2.X), 2) + Math.Pow((punto3.Y - punto2.Y), 2))
            Trianbulo1C = Math.Sqrt(Math.Pow((punto1.X - punto3.X), 2) + Math.Pow((punto1.Y - punto3.Y), 2))

            Dim PerimetroS As Double = (Trianbulo1C + Trianbulo1B + Trianbulo1C) / 2

            Dim Restando As Double = ((PerimetroS - Trianbulo1A) * (PerimetroS - Trianbulo1B) * (PerimetroS - Trianbulo1C))
            Dim Area As Double = Math.Sqrt(PerimetroS * Restando)



            Dim ma As Double = (punto2.Y - punto1.Y) / (punto2.X - punto1.X)
            Dim mb As Double = (punto3.Y - punto2.Y) / (punto3.X - punto2.X)

            Dim centerX As Double = (ma * mb * (punto1.Y - punto3.Y) + mb * (punto1.X + punto2.X) - ma * (punto2.X + punto3.X)) / (2 * (mb - ma))
            Dim centerY As Double = (-1 / ma) * (centerX - (punto1.X + punto2.X) / 2) + (punto1.Y + punto2.Y) / 2


            Dim penCentorTrian1 As Pen = New System.Drawing.Pen(Color.Red)
            Dim CentroTriangulo1 As New Point((punto1.X + punto2.X + punto3.X) / 3, (punto1.Y + punto2.Y + punto3.Y) / 3)
            Dim rectangulo As Rectangle = New Rectangle(centerX - 1, centerY - 1, 2, 2)
            offScreenDC.DrawArc(penCentorTrian1, rectangulo, 0, 360)
            rectangulo = New Rectangle(centerX - 2, centerY - 2, 4, 4)
            offScreenDC.DrawArc(penCentorTrian1, rectangulo, 0, 360)

            Dim radioReal1 As Double = Math.Sqrt(Math.Pow(punto3.X - centerX, 2) + Math.Pow(punto3.Y - centerY, 2))
            Dim radioReal2 As Double = Math.Sqrt(Math.Pow(punto2.X - centerX, 2) + Math.Pow(punto2.Y - centerY, 2))
            Dim radioReal3 As Double = Math.Sqrt(Math.Pow(punto1.X - centerX, 2) + Math.Pow(punto1.Y - centerY, 2))

            If radioReal1 = radioReal2 And radioReal2 = radioReal3 Then
                rectangulo = New Rectangle(centerX - radioReal1, centerY - radioReal1, radioReal1 * 2, radioReal1 * 2)
                penCentorTrian1 = New Pen(Color.Indigo)
                offScreenDC.DrawArc(penCentorTrian1, rectangulo, 0, 360)
            Else
                MsgBox("", MsgBoxStyle.Exclamation)
            End If

            Dim circuloEstandar As Double = Math.Pow(punto1.X - centerX, 2) + Math.Pow(punto1.Y - centerY, 2)

            Dim circuloX As Double = radioReal1 * Math.Cos(0.5) + centerX
            Dim circuloY As Double = radioReal1 * Math.Sin(0.5) + centerY

            rectangulo = New Rectangle(circuloX - 2, circuloY - 2, circuloY + 2, circuloY + 2)
            penCentorTrian1 = New Pen(Color.Blue)
            offScreenDC.DrawArc(penCentorTrian1, rectangulo, 0, 360)


            PictureBox1.Image = Me.Canvas1
            offScreenDC.Dispose()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button4_Click_1(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            Dim g As Graphics = PictureBox1.CreateGraphics '.Graphics
            Dim myPen As New Pen(Color.Black)
            Dim x, y As Integer
            Dim scale As Integer = 10
            Dim DistanciaCentrado As Int16 = 100
            ' Definimos las constantes para la gravedad y la masa
            Dim g1 As Double = 6.674 * Math.Pow(10, -11) ' Constante gravitacional
            Dim m1 As Double = 5.972 * Math.Pow(10, 24) ' Masa de la Tierra en kg
            Dim m2 As Double = 1 ' Masa del objeto en kg
            Dim r1 As Double = 6371000 ' Distancia al centro de la Tierra en metros

            ' Calculamos la fuerza gravitacional
            Dim F As Double = g1 * (m1 * m2) / Math.Pow(r1, 2)

            ' Calculamos la aceleración
            Dim a As Double = F / m2

            For t = 0 To 1000 Step 0.01
                x = scale * t
                y = DistanciaCentrado + (scale * a * Math.Sin(t))
                g.DrawEllipse(myPen, x, y, 1, 1)
            Next

            myPen.Dispose()
            g.Dispose()
        Catch ex As Exception

        End Try
        Try
            Dim graphics As Graphics = Me.CreateGraphics
            Dim pen As New Pen(Color.Black)
            Dim radius As Double = 20
            Dim center As New Point(Me.ClientSize.Width / 8, Me.ClientSize.Height / 8)

            Dim points As New List(Of Point)
            For angle As Double = 0 To 2 * Math.PI Step 0.01
                Dim x As Double = center.X + radius * Math.Cos(angle)
                Dim y As Double = center.Y + radius * Math.Sin(angle)
                points.Add(New Point(CInt(x), CInt(y)))
            Next

            If points.Count > 1 Then
                graphics.DrawPolygon(pen, points.ToArray())

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button_Smith_Click(sender As Object, e As EventArgs) Handles Button_Smith.Click
        ' Para z=r+jx, G=jx/(jx+2) representa el círculo superior, y G=-jx/(-jx+2) representa el círculo inferior2.
        Try

            Dim penCentorTrian1 As Pen = New System.Drawing.Pen(Color.Black)
            Me.Canvas1 = New Bitmap(PictureBox1.Width, PictureBox1.Height)
            Dim offScreenDC As Graphics = Graphics.FromImage(Canvas1)
            offScreenDC.Clear(Color.White)
            Dim CentroX As Int16 = PictureBox1.Width / 2
            dim CentroY as Int16 = PictureBox1.Height/2
            ' Crear un objeto Graphics a partir del objeto e
            Dim g As Graphics = Me.PictureBox1.CreateGraphics()

            ' Definir los valores de r y x
            Dim r As Double = 0.001 ' Resistencia
            Dim x As Double = 0.001 ' Reactancia

            ' Calcular los valores de G para el círculo superior e inferior
            Dim G_upper As Double = (r * x) / (r * x + 2)
            Dim G_lower As Double = -(r * x) / (-r * x + 2)

            ' Definir los rectángulos que contienen los círculos
            Dim rect_upper As New RectangleF(CSng(G_upper) + CentroX, CSng(G_upper) + CentroY, CSng(2 * G_upper) + CentroX, CSng(2 * G_upper) + CentroY)
            Dim rect_lower As New RectangleF(CSng(G_lower) + CentroX, CSng(G_lower) + CentroY, CSng(2 * G_lower) + CentroX, CSng(2 * G_lower) + CentroY)

            ' Dibujar los círculos
            offScreenDC.DrawEllipse(penCentorTrian1, rect_upper)
            offScreenDC.DrawEllipse(Pens.Black, rect_lower)




            'PictureBox1.Image = offScreenDC
            PictureBox1.Image = Me.Canvas1
            offScreenDC.Dispose()
        Catch ex As Exception

        End Try
    End Sub

#Region "Filtros de señales"



    Private Sub Button_Kalman_Click(sender As Object, e As EventArgs) Handles Button_Kalman.Click
        Try

            Dim KalmanInstancias As Class_Kalman
            KalmanInstancias = New Class_Kalman

            KalmanInstancias.setQdistance(Replace(TextBox_Qdistance.Text, ".", ","))
            KalmanInstancias.setRmeasure(Replace(TextBox_KalmanGanancia.Text, ".", ","))


            'Dim NumerosAleatoriosOrigen(PictureBox1.Width - 1) As Double
            Dim NumerosAleatoriosFiltro((PictureBox1.Width - 1) / 2) As Double


            If IsNothing(NumerosAleatoriosOrigen) Then
                Dim Aleatorio As New Random
                NumerosAleatoriosOrigen = New Double((PictureBox1.Width - 1) / 2) {}
                NumerosAleatoriosFiltro = New Double((PictureBox1.Width - 1) / 2) {}
                For indice As Integer = 0 To (PictureBox1.Width - 1) / 2
                    Dim NumeroAleatorio As Integer = Aleatorio.Next(-PictureBox1.Height / 10, PictureBox1.Height / 10) ' Aleatorio.Next(-1 * PictureBox1.Height / 4, PictureBox1.Height / 4)
                    NumerosAleatoriosOrigen(indice) = NumeroAleatorio
                    NumerosAleatoriosFiltro(indice) = KalmanInstancias.getDistance(NumeroAleatorio, 1)
                Next

            Else
                Try
                    For indice As Integer = 0 To (NumerosAleatoriosOrigen.Length - 1)

                        NumerosAleatoriosFiltro(indice) = KalmanInstancias.getDistance(NumerosAleatoriosOrigen(indice), 1) ' * -1

                    Next
                Catch ex As Exception

                End Try


            End If


            Grafica.ImrimeTimeDomaiSimple(PictureBox1, NumerosAleatoriosOrigen, Color.LimeGreen, NumerosAleatoriosFiltro, Color.Fuchsia)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button_FIR_Click(sender As Object, e As EventArgs) Handles Button_FIR.Click
        Try
            Dim FiltroInstancias As Class_FIR
            FiltroInstancias = New Class_FIR
            FiltroInstancias.FiltroFactor = Replace(TextBox_FactorFIR.Text, ".", ",")

            Dim NumerosAleatoriosFiltro((PictureBox1.Width - 1) / 2) As Double



            If IsNothing(NumerosAleatoriosOrigen) Then
                Dim Aleatorio As New Random
                NumerosAleatoriosOrigen = New Double((PictureBox1.Width - 1) / 2) {}
                NumerosAleatoriosFiltro = New Double((PictureBox1.Width - 1) / 2) {}
                For indice As Integer = 0 To (PictureBox1.Width - 1) / 4
                    Dim NumeroAleatorio As Integer = Aleatorio.Next(-PictureBox1.Height / 10, PictureBox1.Height / 10) ' Aleatorio.Next(-1 * PictureBox1.Height / 4, PictureBox1.Height / 4)
                    NumerosAleatoriosOrigen(indice) = NumeroAleatorio
                    NumerosAleatoriosFiltro(indice) = FiltroInstancias.ObtenerValor(NumeroAleatorio)
                Next
                ' Grafica.ImrimeTimeDomaiSimple(PictureBox1, NumerosAleatoriosOrigen, Color.LimeGreen, NumerosAleatoriosFiltro, Color.Fuchsia)
            Else
                ' NumerosAleatoriosOrigen = New Double((PictureBox1.Width - 1) / 4) {}

                For indice As Integer = 0 To (NumerosAleatoriosOrigen.Length - 1) / 2

                    NumerosAleatoriosFiltro(indice) = FiltroInstancias.ObtenerValor(NumerosAleatoriosOrigen(indice)) '* -1

                Next


            End If
            Grafica.ImrimeTimeDomaiSimple(PictureBox1, NumerosAleatoriosOrigen, Color.LimeGreen, NumerosAleatoriosFiltro, Color.Fuchsia)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button_FIR2_Click(sender As Object, e As EventArgs) Handles Button_FIR2.Click
        Try
            Dim FiltroInstancias As Class_FIR
            FiltroInstancias = New Class_FIR
            FiltroInstancias.FiltroFactor = Replace(TextBox_FactorFIR.Text, ".", ",")

            Dim NumerosAleatoriosFiltro((PictureBox1.Width - 1) / 2) As Double



            If IsNothing(NumerosAleatoriosOrigen) Then
                Dim Aleatorio As New Random
                NumerosAleatoriosOrigen = New Double((PictureBox1.Width - 1) / 2) {}
                NumerosAleatoriosFiltro = New Double((PictureBox1.Width - 1) / 2) {}
                For indice As Integer = 0 To (PictureBox1.Width - 1) / 4
                    Dim NumeroAleatorio As Integer = Aleatorio.Next(-PictureBox1.Height / 10, PictureBox1.Height / 10) ' Aleatorio.Next(-1 * PictureBox1.Height / 4, PictureBox1.Height / 4)
                    NumerosAleatoriosOrigen(indice) = NumeroAleatorio
                    NumerosAleatoriosFiltro(indice) = FiltroInstancias.ObtenerValor2(NumeroAleatorio)
                Next
                ' Grafica.ImrimeTimeDomaiSimple(PictureBox1, NumerosAleatoriosOrigen, Color.LimeGreen, NumerosAleatoriosFiltro, Color.Fuchsia)
            Else
                ' NumerosAleatoriosOrigen = New Double((PictureBox1.Width - 1) / 4) {}

                For indice As Integer = 0 To (NumerosAleatoriosOrigen.Length - 1) / 2

                    NumerosAleatoriosFiltro(indice) = FiltroInstancias.ObtenerValor2(NumerosAleatoriosOrigen(indice)) '* -1

                Next


            End If
            Grafica.ImrimeTimeDomaiSimple(PictureBox1, NumerosAleatoriosOrigen, Color.LimeGreen, NumerosAleatoriosFiltro, Color.Fuchsia)

        Catch ex As Exception

        End Try
    End Sub


    Private Sub Button_FIR3_Click(sender As Object, e As EventArgs) Handles Button_FIR3.Click
        Try
            Dim FiltroInstancias As Class_FIR
            FiltroInstancias = New Class_FIR
            FiltroInstancias.FiltroFactor = Replace(TextBox_FactorFIR.Text, ".", ",")

            Dim NumerosAleatoriosFiltro((PictureBox1.Width - 1) / 2) As Double



            If IsNothing(NumerosAleatoriosOrigen) Then
                Dim Aleatorio As New Random
                NumerosAleatoriosOrigen = New Double((PictureBox1.Width - 1) / 2) {}
                NumerosAleatoriosFiltro = New Double((PictureBox1.Width - 1) / 2) {}
                For indice As Integer = 0 To (PictureBox1.Width - 1) / 4
                    Dim NumeroAleatorio As Integer = Aleatorio.Next(-PictureBox1.Height / 10, PictureBox1.Height / 10) ' Aleatorio.Next(-1 * PictureBox1.Height / 4, PictureBox1.Height / 4)
                    NumerosAleatoriosOrigen(indice) = NumeroAleatorio
                    NumerosAleatoriosFiltro(indice) = FiltroInstancias.ObtenerValor3(NumeroAleatorio)
                Next
                ' Grafica.ImrimeTimeDomaiSimple(PictureBox1, NumerosAleatoriosOrigen, Color.LimeGreen, NumerosAleatoriosFiltro, Color.Fuchsia)
            Else
                ' NumerosAleatoriosOrigen = New Double((PictureBox1.Width - 1) / 4) {}

                For indice As Integer = 0 To (NumerosAleatoriosOrigen.Length - 1) / 2

                    NumerosAleatoriosFiltro(indice) = FiltroInstancias.ObtenerValor3(NumerosAleatoriosOrigen(indice)) '* -1

                Next


            End If
            Grafica.ImrimeTimeDomaiSimple(PictureBox1, NumerosAleatoriosOrigen, Color.LimeGreen, NumerosAleatoriosFiltro, Color.Fuchsia)

        Catch ex As Exception

        End Try
    End Sub


    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try

            Dim FiltroInstancias As Class_FIR
            FiltroInstancias = New Class_FIR
            FiltroInstancias.FiltroFactor = Replace(TextBox_FactorFIR.Text, ".", ",")

            Dim KalmanInstancias As Class_Kalman
            KalmanInstancias = New Class_Kalman
            KalmanInstancias.setQdistance(Replace(TextBox_Qdistance.Text, ".", ","))
            KalmanInstancias.setRmeasure(Replace(TextBox_KalmanGanancia.Text, ".", ","))

            Dim NumerosAleatoriosFiltro((PictureBox1.Width - 1) / 2) As Double

            If IsNothing(NumerosAleatoriosOrigen) Then
                Dim Aleatorio As New Random
                NumerosAleatoriosOrigen = New Double((PictureBox1.Width - 1) / 2) {}
                NumerosAleatoriosFiltro = New Double((PictureBox1.Width - 1) / 2) {}
                For indice As Integer = 0 To (PictureBox1.Width - 1) / 4
                    Dim NumeroAleatorio As Integer = Aleatorio.Next(-PictureBox1.Height / 10, PictureBox1.Height / 10) ' Aleatorio.Next(-1 * PictureBox1.Height / 4, PictureBox1.Height / 4)
                    NumerosAleatoriosOrigen(indice) = NumeroAleatorio
                    NumerosAleatoriosFiltro(indice) = KalmanInstancias.getDistance(FiltroInstancias.ObtenerValor(NumerosAleatoriosOrigen(indice)), 2)
                Next
                ' Grafica.ImrimeTimeDomaiSimple(PictureBox1, NumerosAleatoriosOrigen, Color.LimeGreen, NumerosAleatoriosFiltro, Color.Fuchsia)
            Else
                ' NumerosAleatoriosOrigen = New Double((PictureBox1.Width - 1) / 4) {}

                For indice As Integer = 0 To (NumerosAleatoriosOrigen.Length - 1) / 2

                    NumerosAleatoriosFiltro(indice) = KalmanInstancias.getDistance(FiltroInstancias.ObtenerValor(NumerosAleatoriosOrigen(indice)), 2)

                Next


            End If
            Grafica.ImrimeTimeDomaiSimple(PictureBox1, NumerosAleatoriosOrigen, Color.LimeGreen, NumerosAleatoriosFiltro, Color.Fuchsia)

        Catch ex As Exception

        End Try
    End Sub
    Private Sub Button_FIR2Kalman_Click(sender As Object, e As EventArgs) Handles Button_FIR2Kalman.Click
        Try

            Dim FiltroInstancias As Class_FIR
            FiltroInstancias = New Class_FIR
            FiltroInstancias.FiltroFactor = Replace(TextBox_FactorFIR.Text, ".", ",")

            Dim KalmanInstancias As Class_Kalman
            KalmanInstancias = New Class_Kalman
            KalmanInstancias.setQdistance(Replace(TextBox_Qdistance.Text, ".", ","))
            KalmanInstancias.setRmeasure(Replace(TextBox_KalmanGanancia.Text, ".", ","))

            Dim NumerosAleatoriosFiltro((PictureBox1.Width - 1) / 2) As Double

            If IsNothing(NumerosAleatoriosOrigen) Then
                Dim Aleatorio As New Random
                NumerosAleatoriosOrigen = New Double((PictureBox1.Width - 1) / 2) {}
                NumerosAleatoriosFiltro = New Double((PictureBox1.Width - 1) / 2) {}
                For indice As Integer = 0 To (PictureBox1.Width - 1) / 4
                    Dim NumeroAleatorio As Integer = Aleatorio.Next(-PictureBox1.Height / 10, PictureBox1.Height / 10) ' Aleatorio.Next(-1 * PictureBox1.Height / 4, PictureBox1.Height / 4)
                    NumerosAleatoriosOrigen(indice) = NumeroAleatorio
                    NumerosAleatoriosFiltro(indice) = KalmanInstancias.getDistance(FiltroInstancias.ObtenerValor2(NumerosAleatoriosOrigen(indice)), 2)
                Next
                ' Grafica.ImrimeTimeDomaiSimple(PictureBox1, NumerosAleatoriosOrigen, Color.LimeGreen, NumerosAleatoriosFiltro, Color.Fuchsia)
            Else
                ' NumerosAleatoriosOrigen = New Double((PictureBox1.Width - 1) / 4) {}

                For indice As Integer = 0 To (NumerosAleatoriosOrigen.Length - 1) / 2

                    NumerosAleatoriosFiltro(indice) = KalmanInstancias.getDistance(FiltroInstancias.ObtenerValor2(NumerosAleatoriosOrigen(indice)), 2)

                Next


            End If
            Grafica.ImrimeTimeDomaiSimple(PictureBox1, NumerosAleatoriosOrigen, Color.LimeGreen, NumerosAleatoriosFiltro, Color.Fuchsia)

        Catch ex As Exception

        End Try
    End Sub
    Private Sub Button_GenerarLiraAleatoria_Click(sender As Object, e As EventArgs) Handles Button_GenerarLiraAleatoria.Click
        Try


            Dim Aleatorio As New Random
            NumerosAleatoriosOrigen = New Double(TextBox_GenerarLiraAleatoria.Text - 1) {}

            For indice As Integer = 0 To TextBox_GenerarLiraAleatoria.Text - 1
                Dim NumeroAleatorio As Integer = Aleatorio.Next(-PictureBox1.Height / 10, PictureBox1.Height / 10) ' Aleatorio.Next(-1 * PictureBox1.Height / 4, PictureBox1.Height / 4)
                NumerosAleatoriosOrigen(indice) = NumeroAleatorio
            Next


        Catch ex As Exception

        End Try
    End Sub





    'Private ReadOnly windowSize As Integer
    'Private ReadOnly window As Queue(Of Double)
    Private window As Queue(Of Double)
    Private windowSize As Integer
    Private sum As Double

    Public Function Filter(value As Double) As Double
        If window.Count = windowSize Then
            sum -= window.Dequeue()
        End If

        window.Enqueue(value)
        sum += value

        Return sum / window.Count
    End Function

    Private Sub Button_MediaMobil_Click(sender As Object, e As EventArgs) Handles Button_MediaMobil.Click
        Try

            Me.windowSize = 10
            window = New Queue(Of Double)(windowSize)
            sum = 0



            If window.Count = windowSize Then
                sum -= window.Dequeue()
            End If


            'Dim filter As New MovingAverageFilter(windowSize:=5)

            'Dim input As Double() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}
            'Dim output As New List(Of Double)

            'For Each value In input
            'Dim filteredValue = filter.Filter(value)
            'output.Add(filteredValue)
            'Next



            Dim NumerosAleatoriosFiltro((PictureBox1.Width - 1) / 2) As Double

            If IsNothing(NumerosAleatoriosOrigen) Then
                Dim Aleatorio As New Random
                NumerosAleatoriosOrigen = New Double((PictureBox1.Width - 1) / 2) {}
                NumerosAleatoriosFiltro = New Double((PictureBox1.Width - 1) / 2) {}
                For indice As Integer = 0 To (PictureBox1.Width - 1) / 4
                    Dim NumeroAleatorio As Integer = Aleatorio.Next(-PictureBox1.Height / 10, PictureBox1.Height / 10) ' Aleatorio.Next(-1 * PictureBox1.Height / 4, PictureBox1.Height / 4)
                    NumerosAleatoriosOrigen(indice) = NumeroAleatorio
                    NumerosAleatoriosFiltro(indice) = Filter(NumerosAleatoriosOrigen(indice))
                Next
                ' Grafica.ImrimeTimeDomaiSimple(PictureBox1, NumerosAleatoriosOrigen, Color.LimeGreen, NumerosAleatoriosFiltro, Color.Fuchsia)
            Else
                ' NumerosAleatoriosOrigen = New Double((PictureBox1.Width - 1) / 4) {}

                For indice As Integer = 0 To (NumerosAleatoriosOrigen.Length - 1) / 2

                    NumerosAleatoriosFiltro(indice) = Filter(NumerosAleatoriosOrigen(indice))

                Next


            End If
            Grafica.ImrimeTimeDomaiSimple(PictureBox1, NumerosAleatoriosOrigen, Color.LimeGreen, NumerosAleatoriosFiltro, Color.Fuchsia)

        Catch ex As Exception

        End Try
    End Sub


    Private ReadOnly a0 As Double
    Private ReadOnly a1 As Double
    Private ReadOnly a2 As Double

    ''' <summary>
    ''' valores de la ventana de Blackman.
    ''' </summary>
    ''' <param name="n"></param>
    ''' <param name="N_"></param>
    ''' <returns></returns>
    Private Function BlackmanWindow(ByVal n As Integer, ByVal N_ As Integer) As Double
        Dim a0 As Double = 0.42
        Dim a1 As Double = 0.5
        Dim a2 As Double = 0.08
        Dim pi As Double = Math.PI
        Dim w As Double = a0 - a1 * Math.Cos(2 * pi * n / (N_ - 1)) + a2 * Math.Cos(4 * pi * n / (N_ - 1))
        Return w
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="signal"></param>
    ''' <returns></returns>
    'Private Function BlackmanAnalysis(ByVal signal As Double()) As System.num.Complex()
    '    Dim N As Integer = signal.Length
    '    Dim windowedSignal(N - 1) As Double
    '    Dim fftSignal(N - 1) As System.Numerics.Complex

    '    For i As Integer = 0 To N - 1
    '        windowedSignal(i) = signal(i) * BlackmanWindow(i, N)
    '    Next

    '    fftSignal = FourierTransform.FFT(windowedSignal)

    '    Return fftSignal
    'End Function

    Private Sub Button_Blackman_Click(sender As Object, e As EventArgs) Handles Button_Blackman.Click
        Try
            Dim alpha As Double = 0.16
            Dim a0 As Double = (1 - alpha) / 2
            Dim a1 As Double = 0.5
            Dim a2 As Double = alpha / 2

            a0 = 0.42
            a1 = 0.5
            a2 = 0.08



            Dim NumerosAleatoriosFiltro((PictureBox1.Width - 1) / 2) As Double

            If IsNothing(NumerosAleatoriosOrigen) Then
                Dim Aleatorio As New Random
                NumerosAleatoriosOrigen = New Double((PictureBox1.Width - 1) / 2) {}
                NumerosAleatoriosFiltro = New Double((PictureBox1.Width - 1) / 2) {}
                For indice As Integer = 0 To (PictureBox1.Width - 1) / 4
                    Dim NumeroAleatorio As Integer = Aleatorio.Next(-PictureBox1.Height / 10, PictureBox1.Height / 10) ' Aleatorio.Next(-1 * PictureBox1.Height / 4, PictureBox1.Height / 4)
                    NumerosAleatoriosOrigen(indice) = NumeroAleatorio
                    Dim w As Double = a0 - a1 * Math.Cos(2 * Math.PI * indice / (NumerosAleatoriosFiltro.Length - 1)) + a2 * Math.Cos(4 * Math.PI * indice / (NumerosAleatoriosFiltro.Length - 1))
                    NumerosAleatoriosFiltro(indice) = a0 * NumerosAleatoriosOrigen(indice) + a1 * NumerosAleatoriosOrigen(indice) + a2 * NumerosAleatoriosOrigen(indice)
                Next
                ' Grafica.ImrimeTimeDomaiSimple(PictureBox1, NumerosAleatoriosOrigen, Color.LimeGreen, NumerosAleatoriosFiltro, Color.Fuchsia)
            Else
                ' NumerosAleatoriosOrigen = New Double((PictureBox1.Width - 1) / 4) {}

                For indice As Integer = 0 To (NumerosAleatoriosOrigen.Length - 1) / 2
                    Dim w As Double = a0 - a1 * Math.Cos(2 * Math.PI * indice / (NumerosAleatoriosFiltro.Length - 1)) + a2 * Math.Cos(4 * Math.PI * indice / (NumerosAleatoriosFiltro.Length - 1))
                    NumerosAleatoriosFiltro(indice) = w '1 + (a0 * NumerosAleatoriosOrigen(indice) + a1 * NumerosAleatoriosOrigen(indice) + a2 * NumerosAleatoriosOrigen(indice)) 'a0 * NumerosAleatoriosOrigen(indice) + a1 * NumerosAleatoriosOrigen(indice) + a2 * NumerosAleatoriosOrigen(indice)

                Next


            End If
            Grafica.ImrimeTimeDomaiSimple(PictureBox1, NumerosAleatoriosOrigen, Color.LimeGreen, NumerosAleatoriosFiltro, Color.Fuchsia)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button_Softmax_Click(sender As Object, e As EventArgs) Handles Button_Softmax.Click
        Try
            Dim alpha As Double = 0.16
            Dim a0 As Double = (1 - alpha) / 2
            Dim a1 As Double = 0.5
            Dim a2 As Double = alpha / 2



            Dim NumerosAleatoriosFiltro((PictureBox1.Width - 1) / 2) As Double

            If IsNothing(NumerosAleatoriosOrigen) Then
                Dim Aleatorio As New Random
                NumerosAleatoriosOrigen = New Double((PictureBox1.Width - 1) / 2) {}
                NumerosAleatoriosFiltro = New Double((PictureBox1.Width - 1) / 2) {}
                For indice As Integer = 0 To (PictureBox1.Width - 1) / 4
                    Dim NumeroAleatorio As Integer = Aleatorio.Next(-PictureBox1.Height / 10, PictureBox1.Height / 10) ' Aleatorio.Next(-1 * PictureBox1.Height / 4, PictureBox1.Height / 4)
                    NumerosAleatoriosOrigen(indice) = NumeroAleatorio
                    'NumerosAleatoriosFiltro(indice) = a0 * NumerosAleatoriosOrigen(indice) + a1 * NumerosAleatoriosOrigen(indice) + a2 * NumerosAleatoriosOrigen(indice)
                Next
                NumerosAleatoriosFiltro = Softmax(NumerosAleatoriosOrigen)
                ' Grafica.ImrimeTimeDomaiSimple(PictureBox1, NumerosAleatoriosOrigen, Color.LimeGreen, NumerosAleatoriosFiltro, Color.Fuchsia)
            Else
                ' NumerosAleatoriosOrigen = New Double((PictureBox1.Width - 1) / 4) {}

                'For indice As Integer = 0 To (NumerosAleatoriosOrigen.Length - 1) / 2

                '    NumerosAleatoriosFiltro(indice) = Softmax(NumerosAleatoriosOrigen) 'a0 * NumerosAleatoriosOrigen(indice) + a1 * NumerosAleatoriosOrigen(indice) + a2 * NumerosAleatoriosOrigen(indice)

                'Next

                NumerosAleatoriosFiltro = Softmax(NumerosAleatoriosOrigen, 200)
            End If
            Grafica.ImrimeTimeDomaiSimple(PictureBox1, NumerosAleatoriosOrigen, Color.LimeGreen, NumerosAleatoriosFiltro, Color.Fuchsia)

        Catch ex As Exception

        End Try

    End Sub
    Private Function Softmax(ByVal values() As Double, Optional Escala As Double = 1) As Double()
        'Dim maxVal As Double = values.Max()
        Dim maxVal As Double = values.Max()
        Dim exp = values.Select(Function(v) Math.Exp(v - maxVal))
        Dim sumExp As Double = exp.Sum()
        Return exp.Select(Function(v) CDbl(v / sumExp) * Escala).ToArray()
    End Function

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Try

            Dim FiltroInstancias As Class_FIR
            FiltroInstancias = New Class_FIR
            FiltroInstancias.FiltroFactor = Replace(TextBox_FactorFIR.Text, ".", ",")

            Dim KalmanInstancias As Class_Kalman
            KalmanInstancias = New Class_Kalman
            KalmanInstancias.setQdistance(Replace(TextBox_Qdistance.Text, ".", ","))
            KalmanInstancias.setRmeasure(Replace(TextBox_KalmanGanancia.Text, ".", ","))





            Dim NumerosAleatoriosFiltro((PictureBox1.Width - 1) / 2) As Double

            If IsNothing(NumerosAleatoriosOrigen) Then
                Dim Aleatorio As New Random
                NumerosAleatoriosOrigen = New Double((PictureBox1.Width - 1) / 2) {}
                NumerosAleatoriosFiltro = New Double((PictureBox1.Width - 1) / 2) {}
                For indice As Integer = 0 To (PictureBox1.Width - 1) / 4
                    Dim NumeroAleatorio As Integer = Aleatorio.Next(-PictureBox1.Height / 10, PictureBox1.Height / 10) ' Aleatorio.Next(-1 * PictureBox1.Height / 4, PictureBox1.Height / 4)
                    NumerosAleatoriosOrigen(indice) = NumeroAleatorio
                    NumerosAleatoriosFiltro(indice) = KalmanInstancias.getDistance(FiltroInstancias.ObtenerValor2(NumerosAleatoriosOrigen(indice)), 2)
                Next
                ' Grafica.ImrimeTimeDomaiSimple(PictureBox1, NumerosAleatoriosOrigen, Color.LimeGreen, NumerosAleatoriosFiltro, Color.Fuchsia)
            Else
                ' NumerosAleatoriosOrigen = New Double((PictureBox1.Width - 1) / 4) {}

                For indice As Integer = 0 To (NumerosAleatoriosOrigen.Length - 1) / 2

                    NumerosAleatoriosFiltro(indice) = KalmanInstancias.getDistance(FiltroInstancias.ObtenerValor2(NumerosAleatoriosOrigen(indice)), 2)

                Next


            End If
            Grafica.ImrimeTimeDomaiSimple(PictureBox1, NumerosAleatoriosOrigen, Color.LimeGreen, NumerosAleatoriosFiltro, Color.Fuchsia)

        Catch ex As Exception

        End Try
    End Sub

    Dim Pasos As Integer = 100000
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Pasos = 0
        System.Threading.Thread.Sleep(350)
        Dim trama As Threading.Thread = New System.Threading.Thread(AddressOf DrawGravitySimulation)
        trama.Start()
    End Sub
    Friend Sub DrawGravitySimulation()
        Dim timePerParse As Stopwatch
        timePerParse = Stopwatch.StartNew()

        Dim G As Double = 0.000000000066743
        Dim m1 As Double = 10000000000000.0
        Dim m2 As Double = 10000000000000.0
        Dim r1x As Double = 100
        Dim r1y As Double = 100
        Dim r2x As Double = 350
        Dim r2y As Double = 300
        Dim v1x As Double = 1
        Dim v1y As Double = 0.1
        Dim v2x As Double = -0.1
        Dim v2y As Double = -0.01

        Dim dt As Double = 1 '0.1
        Dim t As Double = 0
        Dim canvas As New Bitmap(800, 600)
        Dim g_1 As Graphics = Graphics.FromImage(canvas)
        Dim Pasos As Integer = 100000
        While Pasos
            timePerParse = Stopwatch.StartNew()
            Dim r12x As Double = r2x - r1x
            Dim r12y As Double = r2y - r1y
            Dim r12 As Double = Math.Sqrt(r12x ^ 2 + r12y ^ 2)
            Dim F12 As Double = G * m1 * m2 / r12 ^ 2
            Dim F21 As Double = -F12
            v1x += F21 / m1 * dt
            v1y += F21 / m1 * dt
            v2x += F12 / m2 * dt
            v2y += F12 / m2 * dt
            r1x += v1x * dt
            r1y += v1y * dt
            r2x += v2x * dt
            r2y += v2y * dt
            g_1.Clear(Color.White)
            g_1.FillEllipse(Brushes.Red, CInt(r1x - 10), CInt(r1y - 10), 20, 20)
            g_1.FillEllipse(Brushes.Blue, CInt(r2x - 10), CInt(r2y - 10), 20, 20)
            PictureBox1.Image = canvas
            t += dt
            Pasos -= 1
            timePerParse.Stop()
            Dim ticksThisTime As Integer = timePerParse.ElapsedTicks
            If 20 < ticksThisTime Then
                ticksThisTime = 19
            End If
            System.Threading.Thread.Sleep(20 - ticksThisTime)
        End While
        g_1.Dispose()
    End Sub

    Private Sub Button_MandelbrotSet_Click(sender As Object, e As EventArgs) Handles Button_MandelbrotSet.Click
        'Dim maxIterations As Integer = 1000
        'Dim xMin As Double = -2.5
        'Dim xMax As Double = 1.5
        'Dim yMin As Double = -2
        'Dim yMax As Double = 2
        'Dim width As Integer = 800
        'Dim height As Integer = 600

        'Dim canvas As New Bitmap(width, height)
        'Dim g As Graphics = Graphics.FromImage(canvas)

        'For x As Integer = 0 To width - 1
        '    For y As Integer = 0 To height - 1
        '        Dim a As Double = xMin + (xMax - xMin) * x / width
        '        Dim b As Double = yMin + (yMax - yMin) * y / height
        '        Dim z As Complex = New Complex(0, 0)
        '        Dim c As Complex = New Complex(a, b)
        '        Dim i As Integer = 0

        '        While i < maxIterations AndAlso z.Magnitude < 2
        '            z = z * z + c
        '            i += 1
        '        End While

        '        Dim color As Color = If(i = maxIterations, Color.Black, Color.White)
        '        canvas.SetPixel(x, y, color)
        '    Next
        'Next

        'PictureBox1.Image = canvas
        Try
            Dim maxIterations As Integer = 1000
            Dim xMin As Double = -2.5
            Dim xMax As Double = 1.5
            Dim yMin As Double = -2
            Dim yMax As Double = 2
            Dim width As Integer = PictureBox1.Width
            Dim height As Integer = PictureBox1.Height
            Dim canvas As New Bitmap(width, height)
            Dim g As Graphics = Graphics.FromImage(canvas)
            For x As Integer = 0 To width - 1
                For y As Integer = 0 To height - 1
                    Dim a As Double = xMin + (xMax - xMin) * x / width
                    Dim b As Double = yMin + (yMax - yMin) * y / height
                    Dim zr As Double = 0
                    Dim zi As Double = 0
                    Dim cr As Double = a
                    Dim ci As Double = b
                    Dim i As Integer = 0
                    While i < maxIterations AndAlso zr * zr + zi * zi < 4
                        Dim zrNew As Double = zr * zr - zi * zi + cr
                        Dim ziNew As Double = 2 * zr * zi + ci
                        zr = zrNew
                        zi = ziNew
                        i += 1
                    End While
                    Dim color As Color = If(i = maxIterations, Color.Black, Color.White)
                    canvas.SetPixel(x, y, color)
                Next
            Next
            PictureBox1.Image = canvas
        Catch ex As Exception

        End Try

    End Sub



#End Region

End Class
