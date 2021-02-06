using System;
using System.Collections.Generic;
using System.Text;

namespace Acceleration_WPF_MVVM.Persistance
{
    public class Pair
    {
        public int m_x;
        public int m_y;
        public Pair(int x, int y)
        {
            m_x = x;
            m_y = y;
        }
    };
    public class ScreenObject
    {
        public Pair m_coordinates;
        public Pair m_dimensions;
        public int m_id;

        public ScreenObject()
        {

        }
        public ScreenObject(Pair coordinates, Pair dimensions)
        {
            m_coordinates = coordinates;
            m_dimensions = dimensions;
        }
        public bool isOverlapped(ScreenObject other)
        {
            bool v = (m_coordinates.m_x <= other.m_coordinates.m_x && other.m_coordinates.m_x <= m_coordinates.m_x + m_dimensions.m_x) ||
                (other.m_coordinates.m_x <= m_coordinates.m_x && m_coordinates.m_x <= other.m_coordinates.m_x + other.m_dimensions.m_x);
            bool h = (m_coordinates.m_y <= other.m_coordinates.m_y && other.m_coordinates.m_y <= m_coordinates.m_y + m_dimensions.m_y) ||
                (other.m_coordinates.m_y <= m_coordinates.m_y && m_coordinates.m_y <= other.m_coordinates.m_y + other.m_dimensions.m_y); 
            return v & h; 
        }
        public void MoveDown(int step, int boundary)
        {
            if (m_coordinates.m_x + m_dimensions.m_x  + step > boundary)
            {
                m_coordinates.m_x = boundary - m_dimensions.m_x;
            }
            else
            {
                m_coordinates.m_x += step;
            }
        }
        
        public void MoveLeft(int step, int boundary)
        {
            if(m_coordinates.m_y - step < boundary)
            {
                m_coordinates.m_y = boundary;
            }
            else
            {
                m_coordinates.m_y -= step; 
            }
        }
        public void MoveRight(int step, int boundary)
        {
            if (m_coordinates.m_y + m_dimensions.m_y + step > boundary)
            {
                m_coordinates.m_y = boundary - m_dimensions.m_y;
            }
            else
            {
                m_coordinates.m_y += step;
            }
        }
        public void setValue(List<ScreenObject> objects, List<Pair> coordinates)
        {

        }
    }
}
