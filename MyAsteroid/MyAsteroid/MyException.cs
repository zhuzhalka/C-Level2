using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MyAsteroid
{
    class MyException : Exception
    {
        //для формирования своих собственных исключений

        public MyException(string msg) : base(msg)
        {
            
        }
    }
}
