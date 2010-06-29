using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Todesesser.ObjectPooling;
using System.Collections;

namespace Todesesser.Core
{
    public class Ray2D
    {
        private Vector2 startPos;
        private Vector2 endPos;

        public Ray2D(Vector2 startPos, Vector2 endPos)
        {
            this.startPos = startPos;
            this.endPos = endPos;
        }

        /// <summary>
        /// Determine the first object to intersect with the Ray2D.
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        public ObjectBase IndexIntersect(Hashtable objects, int index)
        {
            ObjectBase closestObject = null;
            float closestObjectVector = -1f;

            Dictionary<float, ObjectBase> intersectingObjects = new Dictionary<float, ObjectBase>();   

            Point p0 = new Point((int)startPos.X, (int)startPos.Y);
            Point p1 = new Point((int)endPos.X, (int)endPos.Y);

            foreach (Point testPoint in BresenhamLine(p0, p1))
            {
                foreach (string objkey in objects.Keys)
                {
                    ObjectBase obj = (ObjectBase)objects[objkey];
                    if (obj.BoundingRectangle.Contains(testPoint))
                    {
                        Vector2 intersectPos = new Vector2((float)testPoint.X, (float)testPoint.Y);
                        if ((Vector2.Distance(new Vector2(p0.X, p0.Y), intersectPos) < closestObjectVector) || closestObjectVector == -1f)
                        {
                            //closestObject = obj;
                            //closestObjectVector = Vector2.Distance(new Vector2(p0.X, p0.Y), intersectPos);
                            if (intersectingObjects.ContainsKey(Vector2.Distance(new Vector2(p0.X, p0.Y), intersectPos)) == false)
                            {
                                intersectingObjects.Add(Vector2.Distance(new Vector2(p0.X, p0.Y), intersectPos), obj);
                            }
                        }
                    }
                }
            }
            //Remove Duplicates
            for (int i = 0; i < intersectingObjects.Count; i++)
            {
                ObjectBase obj = intersectingObjects.ElementAt(i).Value;

                for (int j = 0; j < intersectingObjects.Count; j++)
                {
                    if (i < intersectingObjects.Count && j < intersectingObjects.Count)
                    {
                        ObjectBase obj2 = intersectingObjects.ElementAt(i).Value;

                        if (obj.Key == obj2.Key)
                        {
                            intersectingObjects.Remove(intersectingObjects.ElementAt(i).Key);
                        }
                    }
                }
            }
            //Sort Distances
            var items = from k in intersectingObjects.Keys
                        orderby k ascending
                        select intersectingObjects[k];
            if (index < intersectingObjects.Count)
            {
                closestObject = intersectingObjects.ElementAt(index).Value;
            }
            if (closestObject != null)
            {
                return closestObject;
            }
            return null;
        }

        /// <summary>
        /// Determine the first object to intersect with the Ray2D.
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        public ObjectBase FirstIntersect(Hashtable objects)
        {
            ObjectBase closestObject = null;
            float closestObjectVector = -1f;

            Point p0 = new Point((int)startPos.X, (int)startPos.Y);
            Point p1 = new Point((int)endPos.X, (int)endPos.Y);

            foreach (Point testPoint in BresenhamLine(p0, p1))
            {
                foreach (string objkey in objects.Keys)
                {
                    ObjectBase obj = (ObjectBase)objects[objkey];
                    if (obj.BoundingRectangle.Contains(testPoint))
                    {
                        Vector2 intersectPos = new Vector2((float)testPoint.X, (float)testPoint.Y);
                        if ((Vector2.Distance(new Vector2(p0.X, p0.Y), intersectPos) < closestObjectVector) || closestObjectVector == -1f)
                        {
                            closestObject = obj;
                            closestObjectVector = Vector2.Distance(new Vector2(p0.X, p0.Y), intersectPos);
                        }
                    }
                }
            }
            if (closestObject != null)
            {
                return closestObject;
            }
            return null;
        }

        /// <summary> 
        /// Determine if the ray intersects the rectangle 
        /// </summary> 
        /// <param name="rectangle">Rectangle to check</param> 
        /// <returns></returns> 
        public Vector2 Intersects(Rectangle rectangle)
        {
            Point p0 = new Point((int)startPos.X, (int)startPos.Y);
            Point p1 = new Point((int)endPos.X, (int)endPos.Y);

            foreach (Point testPoint in BresenhamLine(p0, p1))
            {
                if (rectangle.Contains(testPoint))
                {
                    return new Vector2((float)testPoint.X, (float)testPoint.Y);
                }
            }

            return Vector2.Zero;
        }

        // Swap the values of A and B 

        private void Swap<T>(ref T a, ref T b)
        {
            T c = a;
            a = b;
            b = c;
        }

        // Returns the list of points from p0 to p1  

        private List<Point> BresenhamLine(Point p0, Point p1)
        {
            return BresenhamLine(p0.X, p0.Y, p1.X, p1.Y);
        }

        // Returns the list of points from (x0, y0) to (x1, y1) 

        private List<Point> BresenhamLine(int x0, int y0, int x1, int y1)
        {
            // Optimization: it would be preferable to calculate in 
            // advance the size of "result" and to use a fixed-size array 
            // instead of a list. 

            List<Point> result = new List<Point>();

            bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
            if (steep)
            {
                Swap(ref x0, ref y0);
                Swap(ref x1, ref y1);
            }
            if (x0 > x1)
            {
                Swap(ref x0, ref x1);
                Swap(ref y0, ref y1);
            }

            int deltax = x1 - x0;
            int deltay = Math.Abs(y1 - y0);
            int error = 0;
            int ystep;
            int y = y0;
            if (y0 < y1) ystep = 1; else ystep = -1;
            for (int x = x0; x <= x1; x++)
            {
                if (steep) result.Add(new Point(y, x));
                else result.Add(new Point(x, y));
                error += deltay;
                if (2 * error >= deltax)
                {
                    y += ystep;
                    error -= deltax;
                }
            }

            return result;
        }
    }
}
