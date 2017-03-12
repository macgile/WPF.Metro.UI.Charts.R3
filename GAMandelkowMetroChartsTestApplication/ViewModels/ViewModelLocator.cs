﻿/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:WDAMMG"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using GAMandelkowMetroChartsTestApplication.Models;


namespace GAMandelkowMetroChartsTestApplication.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view services and models
                // SimpleIoc.Default.Register<IDataService, DesignDataService>();
              //  SimpleIoc.Default.Register<IGADataService, DataService>();
            }
            else
            {
                // Create run time view services and models
               // SimpleIoc.Default.Register<IGADataService, DataService>();
            }

            //views
            SimpleIoc.Default.Register<MainViewModel>();



        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();


        public static void Cleanup()
        {
           
        }
    }
}