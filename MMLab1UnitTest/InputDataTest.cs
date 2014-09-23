using System.Collections.Generic;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using MMLab1;
using MMLab1.Model;

namespace MMLab1UnitTest
{
    [TestClass]
    public class InputDataTest
    {
        /// <summary>
        /// InitializeStyles
        /// 
        /// Initializing function that allows the tests to be passed and not to fail with XAML parse exception,
        /// cause UnitTest project doesn't have this styles
        /// </summary>
        [TestInitialize]
        public void InitializeStyles()
        {
            if (Application.Current == null)                                                   // if we haven't initialized App.xaml or another App
            {
                App application = new App();                                                   // Create one

                #region Add Static Resources from the App.xaml

                Style windowStyle = new Style(typeof(Window));                                 // Create style for Window

                application.Resources.Add("VS2012WindowStyle", windowStyle);                   // Add our style
                application.Resources.Add("Assembly", "Zpg");                                  // Add assembly

                #endregion
            }  
        }

        /// <summary>
        /// ValidateRangeNTest
        /// 
        /// Test validation of N input parameter
        /// This parameter has a range of values it could save. This case we give it out of range value.
        /// Other parameters in inputData are kept normal to make this test independent
        /// </summary>
        [TestMethod]
        [ExpectedExceptionAttribute(typeof(ValidationException))]
        public void ValidateRangeNTest()
        {
            var inputData = new InputData
            {
                N = 0,
                C_an = 3500,
                G = 0.2,
                k = 2,
                V=0.08,
                h=1
            };
        }
        /// <summary>
        /// ValidateRangeC_anTest
        /// 
        /// Test validation of ConcentrationItems input parameter
        /// This parameter has a range of values it could save. This case we give it out of range value.
        /// Other parameters in inputData are kept normal to make this test independent
        /// </summary>
        [TestMethod]
        [ExpectedExceptionAttribute(typeof(ValidationException))]
        public void ValidateRangeC_anTest()
        {
            var inputData = new InputData
            {
                N = 1,
                C_an = 20000,
                G = 0.2,
                k = 2,
                V = 0.08,
                h = 1
            };
        }
        /// <summary>
        /// ValidateRangeVTest
        /// 
        /// Test validation of V input parameter
        /// This parameter has a range of values it could save. This case we give it out of range value.
        /// Other parameters in inputData are kept normal to make this test independent
        /// </summary>
        [TestMethod]
        [ExpectedExceptionAttribute(typeof(ValidationException))]
        public void ValidateRangeVTest()
        {

            var inputData = new InputData
            {
                N = 1,
                C_an = 3500,
                G = 0.2,
                k = 2,
                V = 3,
                h = 1
            };
        }
        /// <summary>
        /// ValidateRangeGTest
        /// 
        /// Test validation of G input parameter
        /// This parameter has a range of values it could save. This case we give it out of range value.
        /// Other parameters in inputData are kept normal to make this test independent
        /// </summary>
        [TestMethod]
        [ExpectedExceptionAttribute(typeof(ValidationException))]
        public void ValidateRangeGTest()
        {

            var inputData = new InputData
            {
                N = 1,
                C_an = 3500,
                G = 3,
                k = 2,
                V = 0.08,
                h = 1
            };
        }

        [TestMethod]
        public void ProcessDataTest()
        {
            //TODO Make a test for data processing. 
            //TODO Maybe some comparisons of calculated by hands data and the results
        }

    }
}
