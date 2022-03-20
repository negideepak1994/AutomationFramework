using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.FrameworkComponents
{
    public class ErrorHandler : GlobalVariable
    {

        public static void Errorhandler(Action funcToCheck, string actualResult, string expectedResult, bool doThrowException = true)
        {
            bool isTestCasePassed = true;
            try
            {
                string action = Utilities.GetMethodNameFromStack(2);
                funcToCheck();
            }
            catch (Exception ex)
            {
                isTestCasePassed = false;
                actualResult = ex.Message;
                if (doThrowException)
                {
                    throw ex;
                }
            }
        }
    }
}
