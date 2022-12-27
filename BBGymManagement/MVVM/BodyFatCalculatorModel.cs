using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BBGymManagement.MVVM
{
    public class BodyFatCalculatorModel
    {
        public int Age { get; set; }
        public SexType Sex { get; set; }
        public int Height { get; set; }
        public int NeckCircumference { get; set; }
        public int WaistCircumference { get; set; }
        public int HipCircumference { get; set; }

        public enum SexType
        {
            Male = 10,
            Female = 20
        }
    }
}