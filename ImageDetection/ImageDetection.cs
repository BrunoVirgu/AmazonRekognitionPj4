using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageDetection
{
    public class ImageDetection
    {
        private AmazonRekognitionClient client = new AmazonRekognitionClient();

        public async Task<Boolean> IsImageSafe(string imageId, string bucketId)
        {
            var response = await client.DetectModerationLabelsAsync(new DetectModerationLabelsRequest()
            {
                Image = new Amazon.Rekognition.Model.Image()
                {
                    S3Object = new S3Object()
                    {
                        Bucket = bucketId,
                        Name = imageId
                    }
                }

            });

            return response.ModerationLabels.Count == 0;

        }

        public async Task<MemoryStream> RedactFaces(MemoryStream input)
        {
            var response = await client.DetectFacesAsync(new DetectFacesRequest()
            {
                Image = new Amazon.Rekognition.Model.Image()
                {
                    Bytes = input
                },
            });

            if (response.FaceDetails.Count == 0)
            {
                input.Position = 0;
                return input;
            }
            else
            {
                var image = System.Drawing.Image.FromStream(input);
                var graphics = System.Drawing.Graphics.FromImage(image);
                var output = new MemoryStream();

                foreach (var item in response.FaceDetails)
                {
                    var brush = new SolidBrush(System.Drawing.Color.White);

                    var rectan = new Rectangle(
                        (int)(image.Width * item.BoundingBox.Left),
                        (int)(image.Height * item.BoundingBox.Top),
                        (int)(image.Width * item.BoundingBox.Width),
                        (int)(image.Width * item.BoundingBox.Height)
                        );
                    graphics.FillRectangle(brush, rectan);
                }

                graphics.Save();
                image.Save(output, image.RawFormat);

                File.WriteAllBytes(@"C:\temp\imagem.jpg", output.ToArray());
                return output;
            }
        }

        public async Task<Boolean> ListLabels(string imageId, string bukcetId)
        {
            var response = await client.DetectLabelsAsync(new DetectLabelsRequest()
            {
                Image = new Amazon.Rekognition.Model.Image()
                {
                    S3Object = new S3Object()
                    {
                        Bucket = bukcetId,
                        Name = imageId
                    }
                },
            });

            foreach (var item in response.Labels)
            {
                Console.WriteLine(item.Name);
            }

            return true;
        }


    }
}
