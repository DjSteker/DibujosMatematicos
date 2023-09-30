Public Class Class_GeneradorSeñales

    Friend _waveLeft As Single()
    Friend _waveRight As Single()

    Private _signalGenerator As SoundViewer.SignalGenerator


    Friend WaveformType As String = ("Sine")
    Friend SamplingRate As UInt32 = (44100)
    Friend Samples As UInt32 = (16384)
    Friend Frequency As UInt32 = (5000)
    Friend Amplitude As UInt32 = (32768)


    Public Function Process() As Double()

        '_waveLeft = New Single((wave.Length / 4) - 1) {}
        '_waveRight = New Single((wave.Length / 4) - 1) {}


        _signalGenerator = New SoundViewer.SignalGenerator()
        _signalGenerator.SetWaveform(WaveformType)
        _signalGenerator.SetSamplingRate(SamplingRate)
        _signalGenerator.SetSamples(Samples)
        _signalGenerator.SetFrequency(Frequency)
        _signalGenerator.SetAmplitude(Amplitude)
        '_waveLeft = _signalGenerator.GenerateSignal()
        '_waveRight = _signalGenerator.GenerateSignal()

        Return _signalGenerator.GenerateSignalDuble()
    End Function


End Class
