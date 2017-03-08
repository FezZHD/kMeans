using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace kMeans.Model
{
    internal class PointsModel
    {

        private readonly Random randomClass = new Random();

        internal PointsModel(int pointsCount, int classCount)
        {
            this.pointsCount = pointsCount;
            this.classCount = classCount;
        }


        private readonly int pointsCount;
        private readonly int classCount;


        private List<ClassModel> RandomStartPoint()
        {
            var startPoints = new List<Points>(pointsCount);
            for (int i = 0; i < startPoints.Capacity; i++)
            {
                startPoints.Add(new Points
                {
                    XPoint = randomClass.Next(0, 570),
                    YPoint = randomClass.Next(0, 600)
                });
            }
            return GenerateClasses(startPoints);
        }


        internal void Execute()
        {
            var list = RandomStartPoint();
        }


        private List<ClassModel> GenerateClasses(List<Points> points)
        {
            int[] centralPointsindex = new int[classCount];
            for (int i = 0; i < centralPointsindex.Length; i++)
            {
                centralPointsindex[i] = randomClass.Next(0, points.Capacity);
            }
            var classesList = new List<ClassModel>(classCount);
            for (int i = 0; i < classesList.Capacity; i++)
            {
                classesList.Add(new ClassModel
                {
                    CentralPoints = points[centralPointsindex[i]],
                    ClassColor =
                        Color.FromRgb(GetRandomColorNumber(), GetRandomColorNumber(), GetRandomColorNumber()),
                    ClassPoints = new List<Points>()
                });
            }
            PutPointsInToClasses(classesList, points);
            return classesList;
        }


        private byte GetRandomColorNumber()
        {
            return (byte) randomClass.Next(0, 255);
        }


        private void PutPointsInToClasses(List<ClassModel> classes, List<Points> points)
        {
            foreach (var point in points)
            {
                double minimalLenght = 0;
                int index = 0;
                for (int i = 0; i < classes.Count; i++)
                {
                    if (i == 0)
                    {
                        minimalLenght = CalculateLenght(point, classes[i].CentralPoints);
                    }
                    else
                    {
                        var newLenght = CalculateLenght(point, classes[i].CentralPoints);
                        if ( newLenght < minimalLenght)
                        {
                            minimalLenght = newLenght;
                            index = i;
                        }
                    }
                }
                classes[index].ClassPoints.Add(point);
            }
        }


        private double CalculateLenght(Points point, Points centerClassPoint)
        {
            var yLenght = Math.Abs(point.YPoint - centerClassPoint.YPoint);
            var xLenght = Math.Abs(point.XPoint - centerClassPoint.XPoint);
            return Math.Sqrt(Pow(xLenght) + Pow(yLenght));
        }


        private int Pow(int value)
        {
            return value*value;
        }
    }
}