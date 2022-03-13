using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace UP01._01
{
    public partial class Material
    {
        public string MinCounts
        {
            get
            {
                BaseClass.Base.Material.Where(x => x.ID == ID );
                return (MinCount +" "+Unit);
            }
        }

        public string Remainder
        {
            get
            {
                BaseClass.Base.Material.Where(x => x.ID == ID);
                return(CountInStock+" "+Unit);          
            }
        }

        public SolidColorBrush StockColor
        {
            get
            {
                BaseClass.Base.Material.Where(x => x.ID == ID);
                if(CountInStock<MinCount)
                {
                    return Brushes.LightPink;
                }   
                else if (CountInStock==(MinCount*3))
                {
                    return Brushes.Orange;
                }
                else
                {
                    return Brushes.White;
                }     
            }
        }

    }
}
