using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using ImageDetection;


namespace AmazonRekognitionPj4
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var img = new ImageDetection.ImageDetection();

            var memoryStream = ImageDetection.ImageHelper.LoadImageToMemoryStream(@"C:\temp\familiacomendo.jpg");

            var redactFaces = await img.RedactFaces(memoryStream);

            var list = await img.ListLabels("pj4-bsi-rekognition", "teste.jpg");
            var isSafe = await img.IsImageSafe("pj4-bsi-rekognition", "arma-de-fogo.jpg");
            var isSafe2 = await img.IsImageSafe("pj4-bsi-rekognition", "faca-na-mao.jpg");
            var isSafe3 = await img.IsImageSafe("pj4-bsi-rekognition", "familiacomendo.jpg");

            

        }

    }
}
