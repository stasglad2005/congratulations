using BithdayAPP;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BirthdayAPP
{
    class Program
    {
        static void Main(string[] args)
        {
            Congratulations congratulations = new Congratulations();
            congratulations.Run();
        }
    }
}