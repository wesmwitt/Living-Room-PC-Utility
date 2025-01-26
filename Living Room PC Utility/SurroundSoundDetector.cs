using NAudio.CoreAudioApi;
using NAudio.Wave;

public class SurroundSoundDetector
{
    private float[] channelEnergy;
    private bool[] channelDetectedSound;
    private string[] speakerNames;
    private bool isRunning;

    public SurroundSoundDetector()
    {
        // Default speaker names for 7.1 surround sound
        speakerNames = new[]
        {
            "Front Left", "Front Right", "Center", "Subwoofer",
            "Rear Left", "Rear Right", "Side Left", "Side Right"
        };
    }

    public Dictionary<string, bool> DetectActiveChannels()
    {
        var results = new Dictionary<string, bool>();

        var enumerator = new MMDeviceEnumerator();
        var defaultDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);

        using (var capture = new WasapiLoopbackCapture(defaultDevice))
        {
            int channelCount = capture.WaveFormat.Channels;
            channelEnergy = new float[channelCount];
            channelDetectedSound = new bool[channelCount];

            capture.DataAvailable += (s, e) =>
            {
                ProcessAudioData(e.Buffer, e.BytesRecorded, capture.WaveFormat);
            };

            isRunning = true;
            capture.StartRecording();
            //Console.WriteLine("Detecting channels... Press any key to stop.");
            while (isRunning){
                Thread.Sleep(1000);
            }
            //Console.ReadKey();
            capture.StopRecording();
        }

        for (int i = 0; i < channelDetectedSound.Length; i++)
        {
            string speakerName = i < speakerNames.Length ? speakerNames[i] : $"Channel {i + 1}";
            results[speakerName] = channelDetectedSound[i];
        }

        return results;
    }

    public void StopDetection()
    {
        isRunning = false;
    }

    private void ProcessAudioData(byte[] buffer, int bytesRecorded, WaveFormat format)
    {
        if (format.Encoding != WaveFormatEncoding.IeeeFloat || format.BitsPerSample != 32)
        {
            Console.WriteLine("Unsupported format. Ensure audio is 32-bit float PCM.");
            return;
        }

        int channelCount = format.Channels;
        int sampleCount = bytesRecorded / (channelCount * sizeof(float));

        Array.Clear(channelEnergy, 0, channelEnergy.Length);

        for (int i = 0; i < sampleCount; i++)
        {
            for (int ch = 0; ch < channelCount; ch++)
            {
                int sampleIndex = i * channelCount + ch;
                float sampleValue = BitConverter.ToSingle(buffer, sampleIndex * sizeof(float));
                channelEnergy[ch] += sampleValue * sampleValue;
            }
        }

        float threshold = 0.01f;

        for (int ch = 0; ch < channelCount; ch++)
        {
            channelEnergy[ch] = (float)Math.Sqrt(channelEnergy[ch] / sampleCount);
            if (channelEnergy[ch] > threshold)
            {
                channelDetectedSound[ch] = true;
            }
        }
    }
}