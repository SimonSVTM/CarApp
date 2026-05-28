// CarApp.Console/Program.cs

using CarApp.Core.Models;
using CarApp.Core.Repositories;
using System;
using System.Linq;


// Skift mellem InMemory og File med én linje

// ICarRepository repo = new InMemoryCarRepository();

ICarRepository repo = new FileCarRepository("cars.txt");


Console.WriteLine($"System starter: {repo.GetAll().Count()} biler indlæst.");


if (!repo.GetAll().Any())

{

    repo.Add(new FuelCar("Toyota", "Corolla", 2022, "AB12345", 50, 18, 45000));

    repo.Add(new ElectricCar("Tesla", "Model 3", 2023, "CD67890", 75, 6.5,

    380000));

    repo.Add(new FuelCar("BMW", "320d", 2021, "XY99999", 60, 15, 320000));

    Console.WriteLine("Foerste kørsel: 3 biler tilfoejat.");

}


foreach (Car car in repo.GetAll())
    Console.WriteLine($" {car.Brand} {car.Model} ({car.Year}) — {car.LicensePlate}");


Console.WriteLine("Program slutter.");