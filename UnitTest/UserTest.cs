using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using IR = IRepositories;
using IS = IServices;
using Services;
using ViewModels;
using FakeItEasy;

namespace ShaHua.Tests
{
    [TestClass]
    public class UserTest
    {
        IUnityContainer container;

        [TestInitialize]
        public void TestInit()
        {
            container = new UnityContainer();
            container.RegisterType<IS.IUserService, UserService>();
        }

        [TestMethod]
        public void GetUser()
        {
           
        }
    }
}
