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


        private Tuple<List<ClassModel>,List<Points>> RandomStartPoint()
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
            return Tuple.Create(GenerateClasses(startPoints), startPoints);
        }


        internal Tuple<List<ClassModel>, List<Points>> ExecuteFirstDrawing()
        {
            return RandomStartPoint();
        }


        internal Tuple<List<ClassModel>, bool> RedrawPoints(List<Points> points, List<ClassModel> currentClasses)
        {
            var oldCenters = CopyOldCenterPoints(currentClasses);
            foreach (var currentClass in currentClasses)
            {
                currentClass.CentralPoints = RecalculateCenter(currentClass.ClassPoints);
            }
            bool isNeedToRedraw = CompareCenters(oldCenters, currentClasses);
            if (isNeedToRedraw)
            {
                return Tuple.Create(currentClasses, false);
            }
            ClearClassList(currentClasses);
            PutPointsInToClasses(currentClasses, points);
            return Tuple.Create(currentClasses, true);
        }

        private void ClearClassList(List<ClassModel> classes)
        {
            foreach (var currentClass in classes)
            {
                currentClass.ClassPoints.Clear();
            }
        }

        private List<Points> CopyOldCenterPoints(List<ClassModel> classes)
        {
            var newList = new List<Points>(classCount);
            foreach (var currentClass in classes)
            {
                newList.Add(currentClass.CentralPoints);
            }
            return newList;
        }


        private bool CompareCenters(List<Points> oldCenters, List<ClassModel> classesWithNewCenter)
        {
            bool result = true;
            for (int i = 0; i < classCount; i++)
            {
                if (oldCenters[i].XPoint != classesWithNewCenter[i].CentralPoints.YPoint &&
                    oldCenters[i].YPoint != classesWithNewCenter[i].CentralPoints.YPoint)
                {
                    result = false;
                    break;
                }
            }
            return result;
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
            var centralPoints = CopyOldCenterPoints(classes);
            foreach (var point in points)
            {
                var index = ReturnPointIndex(centralPoints, point);
                classes[index].ClassPoints.Add(point);
            }
        }


        private int ReturnPointIndex(List<Points> endPoints, Points start)
        {
            double minimalLenght = 0;
            int index = 0;
            for (int i = 0; i < endPoints.Count; i++)
            {
                if (i == 0)
                {
                    minimalLenght = CalculateLenght(endPoints[i], start);
                }
                else
                {
                    var newLenght = CalculateLenght(endPoints[i], start);
                    if (newLenght < minimalLenght)
                    {
                        minimalLenght = newLenght;
                        index = i;
                    }
                }
            }
            return index;
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


        private double Pow(double value)
        {
            return value*value;
        }


        private double Pow(double a, double b)
        {
            return Pow(a) - (2*a*b) + Pow(b);
        }

        private Points RecalculateCenter(ClassModel currentClass)
        {
            var newCenter = ReturnSigma(currentClass.ClassPoints, AveragePoint(currentClass.ClassPoints));
            int newX = Convert.ToInt32(newCenter.Item1);
            int newY = Convert.ToInt32(newCenter.Item2);
            var newCenterPoint = new Points
            {
                XPoint = newX,
                YPoint = newY
            };
            var index = ReturnPointIndex(currentClass.ClassPoints, newCenterPoint);
            return currentClass.ClassPoints[index];
        }


        private Points RecalculateCenter(List<Points> classPoints)
        {
            var pointsCountCordinate = CountPointsNumber(classPoints);

            var newCenter = new Points
            {
                XPoint = (int) Math.Round((double) pointsCountCordinate.Item1 / classPoints.Count, MidpointRounding.AwayFromZero),
                YPoint = (int) Math.Round((double) pointsCountCordinate.Item2 / classPoints.Count, MidpointRounding.AwayFromZero)
            };
            var index = ReturnPointIndex(classPoints, newCenter);
            return classPoints[index];
        }


        private Tuple<int, int> CountPointsNumber(List<Points> points)
        {
            int xCount = 0;
            int yCount = 0;
            foreach (var point in points)
            {
                xCount += point.XPoint;
                yCount += point.YPoint;
            }
            return Tuple.Create(xCount, yCount);
        }

        private Tuple<double, double> AveragePoint(List<Points> currentClassPoints)
        {
            int xPoint = 0;
            int yPoint = 0;
            foreach (Points point in currentClassPoints)
            {
                xPoint += point.XPoint;
                yPoint += point.YPoint;
            }
            var xRound = Math.Round((double) xPoint/currentClassPoints.Count, MidpointRounding.AwayFromZero);
            var yRound = Math.Round((double) yPoint/currentClassPoints.Count, MidpointRounding.AwayFromZero);
            return Tuple.Create(xRound, yRound);
        }


        private Tuple<double, double> ReturnSigma(List<Points> currentClassPoints, Tuple<double, double> averagePoint)
        {
            double xSigma = 0;
            double ySigma = 0;
            foreach (var points in currentClassPoints)
            {
                xSigma += Pow(points.XPoint, averagePoint.Item1);
                ySigma += Pow(points.YPoint, averagePoint.Item2);
            }
            double xResultSigma = Math.Round(Math.Sqrt(xSigma/currentClassPoints.Count), MidpointRounding.AwayFromZero);
            double yResultSigma = Math.Round(Math.Sqrt(ySigma/currentClassPoints.Count), MidpointRounding.AwayFromZero);
            return Tuple.Create(xResultSigma, yResultSigma);
        }
    }
}