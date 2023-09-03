using Microsoft.AspNetCore.Mvc;
using NAudio.Wave;
using System.Diagnostics;
using WaveFormPlotter.Models;

namespace WebApplication1.Controllers
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
            string filePath = @"C:\Users\basil\Documents\10khz.wav";
            string recordPath = @"C:\Users\basil\Documents\Recording.wav";

            // read the audio file
            ViewData["SourceSamples"] = ChangeFileToSamples(filePath);
            ViewData["RecordSamples"] = ChangeFileToSamples(recordPath);

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