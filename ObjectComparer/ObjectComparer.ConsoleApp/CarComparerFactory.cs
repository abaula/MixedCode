using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObjectComparer.Drafts;

namespace ObjectComparer.ConsoleApp
{
    public class CarComparerFactory : ObjectComparerFactory<CarDto>
    {
        public CarComparerFactory()
        {
            //AddComparer();
        }
    }
}
