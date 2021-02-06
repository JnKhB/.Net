using System;
using System.Collections.Generic;
using System.Text;
using Acceleration_WPF_MVVM.Persistance;
namespace Acceleration_WPF_MVVM.ViewModel
{
    public class ViewScreenObject : ViewModelBase
    {
        private bool m_isPetrol;
        private double m_dimension;
        private int m_x;
        private int m_y;
        private int m_id; 
        public double Dimension
        {
            get { return m_dimension; }
            set
            {
                if (m_dimension != value)
                {
                    m_dimension = value;
                    OnPropertyChanged(nameof(Dimension));
                }
            }
        }
        public int ID
        {
            get { return m_id; }
            set
            {
                if (m_id != value)
                {
                    m_id = value;
                    OnPropertyChanged(nameof(ID));
                }
            }
        }
        public bool IsPetrol
        {
            get { return m_isPetrol; }
            set
            {
                if (m_isPetrol != value)
                {
                    m_isPetrol = value;
                    OnPropertyChanged(nameof(IsPetrol));
                }
            }
        }
        public int X
        {
            get { return m_x;  }
            set
            {
                if (m_x != value)
                {
                    m_x = value;
                    OnPropertyChanged(nameof(X));
                }
            }
        }
        public int Y
        {
            get { return m_y; }
            set
            {
                if (m_y != value)
                {
                    m_y = value;
                    OnPropertyChanged(nameof(Y));
                }
            }
        }
        public ViewScreenObject(int x, int y, double dimension, bool isPetrol, int id)
        {
            X = x;
            Y = y;
            Dimension = dimension;
            IsPetrol = isPetrol;
            ID = id;
        }
    }
}
