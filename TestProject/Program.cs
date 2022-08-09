﻿// See https://aka.ms/new-console-template for more information
using static TestProject.Algorithm;
class Program
{
    static void Main(string[] args)
    {
        //Example to run
        string demoQuery = "Connections| where DestinationEntityType =~'AadIdentity'| extend a = a + b |let x= where a > 10.0";
        var cleanQuery = PassQueryFindCustomerData(demoQuery);
        Console.WriteLine("Source query: "+demoQuery);
        Console.WriteLine("New query: " + cleanQuery);
    }
}
