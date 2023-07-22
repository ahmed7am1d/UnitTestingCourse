using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Tests;
public class WorldsDumbestFunctionTests
{
    //Naming convention - ClassName_MethodName_ExpectedResult
    public static void WorldsDumbestFunction_ReturnsPikachuIfZero_ReturnString()
    {
        try
        {
            //Arrange 
            int num = 0;
            WorldsDumbestFunction worldsDumbest = new WorldsDumbestFunction();

            //Act 
            string result = worldsDumbest.ReturnsPickachuIfZero(num);
            
            //Assert - Whatever is returned is it what we want [The part where we make sure]
            if (result.Equals("PIKACU!"))
            {
                Console.WriteLine("PASSED: WorldsDumbestFunction_ReturnsPikachuIfZero_ReturnString");
            } else
            {
                Console.WriteLine("FAILED: WorldsDumbestFunction_ReturnsPikachuIfZero_ReturnString");
            }
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex);
        }
    }
}
