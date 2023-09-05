using Microsoft.AspNetCore.Mvc;
using NAudio.Wave;
using System.Diagnostics;
using WaveFormPlotter.Models;

namespace WaveFormPlotter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // path to the audio file
            string recordedPath = @"C:\Users\basila\Desktop\Pixel_recording_SNR_10kzh_1.5s_48_16bit_mono.wav";
            string trimmedPath = @"C:\Users\basila\Desktop\output.wav";
            string referencePath = @"C:\Users\basila\Desktop\SNR_10kzh_1.5s_48_16bit_mono.wav";

            // read the audio file
            ViewData["Recorded"] = ChangeFileToSamples(recordedPath);
            ViewData["Trimmed"] = ChangeFileToSamples(trimmedPath);
            ViewData["Reference"] = ChangeFileToSamples(referencePath);

            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private float[] ChangeFileToSamples(string path)
        {
            // read the audio file
            using (var reader = new AudioFileReader(path))
            {
                // get the time limit from the total duration of the audio file
                double timeLimit = reader.TotalTime.TotalSeconds;

                // calculate the number of samples to read
                int numSamples = (int)(timeLimit * reader.WaveFormat.SampleRate);

                // get the sample data
                float[] samples = new float[numSamples];
                reader.Read(samples, 0, numSamples);

                // pass the sample data to the view
                return samples;
            }
        }
    }
}