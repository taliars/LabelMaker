using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using LabelMaker.Core;
using LabelMaker.ViewModel;

namespace LabelMaker.Helpers
{
    internal static class DocumentHelper
    {
        public static void OpenWithDefaultApp(MemoryStream memoryStream, string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            using (var file = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                memoryStream.WriteTo(file);
            }

            var p = new Process
            {
                StartInfo = new ProcessStartInfo(path)
                {
                    UseShellExecute = true
                }
            };

            p.Start();
        }
    }

    internal static class ConvertHelper
    {
        public static void UpdateWithNewPointCount(
            this ICollection<PointViewModel> pointCollection,
            MainInfo mainInfo)
        {
            if (mainInfo.PointsCount == 0)
            {
                pointCollection.Clear();
            }

            var newPointsNumbers = Enumerable.Range(1, mainInfo.PointsCount);

            foreach (var newPoint in newPointsNumbers)
            {
                var removePoints = pointCollection.Where(x => !newPointsNumbers.Contains(x.Number)).ToArray();

                for (int i = 0; i < removePoints.Length; i++)
                {
                    pointCollection.Remove(removePoints[i]);
                }
            }

            var pointsToAdd = newPointsNumbers.Where(x => !pointCollection.Any(y => y.Number == x)).ToArray();

            foreach (var pointToAdd in pointsToAdd)
            {
                pointCollection.Add(new PointViewModel
                {
                    Number = pointToAdd,
                    Horizons = 0,
                });
            }
        }
    }
}
