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
            var directory = @"..\\..\\..\\Imagens";

            var memoryStream = ImageDetection.ImageHelper.LoadImageToMemoryStream($"{directory}//familiacomendo.jpg");
            var redactFaces = await img.RedactFaces(memoryStream);


            var memoryStream1 = ImageDetection.ImageHelper.LoadImageToMemoryStream($"{directory}//arma-de-fogo.jpg");
            var list = await img.ListLabels(memoryStream1);
            var isSafe = await img.IsImageSafe(memoryStream1);
            Console.WriteLine($"É Seguro: {isSafe}");
            Console.WriteLine("---------------------------------");

            var memoryStream2 = ImageDetection.ImageHelper.LoadImageToMemoryStream($"{directory}//familiacomendo.jpg");
            list = await img.ListLabels(memoryStream2);
            var isSafe2 = await img.IsImageSafe(memoryStream2);
            Console.WriteLine($"É Seguro: {isSafe2}");
            Console.WriteLine("---------------------------------");

            var memoryStrea3m = ImageDetection.ImageHelper.LoadImageToMemoryStream($"{directory}//faca-na-mão.jpg");
            list = await img.ListLabels(memoryStrea3m);
            var isSafe3 = await img.IsImageSafe(memoryStrea3m);
            Console.WriteLine($"É Seguro: {isSafe3}");
            Console.WriteLine("---------------------------------");



        }

    }
}
