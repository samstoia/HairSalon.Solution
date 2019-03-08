using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class SpecialtyTest: IDisposable
  {
    public SpecialtyTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=sam_stoia_test;";
    }

    public void Dispose()
    {
      Specialty.ClearAll();
    }

    [TestMethod]
    public void SpecialtyConstructor_CreatesInstanceOfSpecialty_Specialty()
    {
      Specialty newSpecialty = new Specialty("test");
      Assert.AreEqual(typeof(Specialty), newSpecialty.GetType());
    }

    [TestMethod]
    public void GetSpecialtyName_GetsSpecialtyName_String()
    {
      string name = "The Rock";
      Specialty newSpecialty = new Specialty(name);

      string result = newSpecialty.GetSpecialtyName();

      Assert.AreEqual(name, result);
    }

    [TestMethod]
    public void GetAll_ReturnsSpecialtys_SpecialtyList()
    {
      //Arrange
      string name01 = "Michael Jackson";
      string name02 = "John Snow";
      Specialty newSpecialty1 = new Specialty(name01);
      newSpecialty1.Save();
      Specialty newSpecialty2 = new Specialty(name02);
      newSpecialty2.Save();
      List<Specialty> newList = new List<Specialty> { newSpecialty1, newSpecialty2 };

      //Act
      List<Specialty> result = Specialty.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }
  }
  
}