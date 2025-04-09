using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;

namespace Living_Room_PC_Utility
{

    class VolumeObject
    {

        public int volume_level;
        public string entity_id;

        public VolumeObject(int volume_level, string entity_id)
        {
            this.volume_level = volume_level;
            this.entity_id = entity_id;
        }
    }

    internal class VolumeSetter
    {

        private static HttpClient client = new HttpClient();
        private static String url = "http://homeassistant.local:8123";
        private static String token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJmZjdlY2YzZTM0ZTI0ZGJmYjFjYTZmYjFhNTAyNzhlZCIsImlhdCI6MTczOTEzNDI4MywiZXhwIjoyMDU0NDk0MjgzfQ.VIzbuXq6LPfFvaOADtyaVerSaB6LTnJbKRsN8Ll7zDc";

        public static async void SetReceiverVolume(int volume)
        {

            Debug.WriteLine("SetReceiverVolume(): " + volume);

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Convert the int volume (0-100) to float (0.0 - 1.0)
            float volumeLevel = Math.Clamp(volume / 100f, 0f, 1f);

            string json = $@"{{
              ""entity_id"": ""media_player.denon_avr_x3800h"",
              ""volume_level"": {volumeLevel.ToString(System.Globalization.CultureInfo.InvariantCulture)}
            }}";

            var request = new HttpRequestMessage(HttpMethod.Post, "http://homeassistant.local:8123/api/services/media_player/volume_set")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Debug.WriteLine(await response.Content.ReadAsStringAsync());
        }

    }

}
