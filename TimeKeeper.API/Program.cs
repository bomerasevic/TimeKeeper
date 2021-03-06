﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace TimeKeeper.API
{
    public class Program
    {        
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                          .UseKestrel()
                          .UseContentRoot(Directory.GetCurrentDirectory())
                          .UseIISIntegration()
                          .UseStartup<Startup>()
                          //.ConfigureLogging(log =>
                          //{
                          //    log.ClearProviders();
                          //    log.SetMinimumLevel(LogLevel.Information);
                          //}).UseNLog()
                          .Build();
            host.Run();


            //var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            //try
            //{
            //    logger.Info("init main");
            //}
            //catch(Exception ex)
            //{
            //    logger.Error(ex, "Stopped program");
            //    throw;
            //}
            //finally
            //{
            //    NLog.LogManager.Shutdown();
            //}
        }

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args).UseStartup<Startup>()
        // .ConfigureLogging(log => 
        //    {
        //    log.ClearProviders();
        //    log.SetMinimumLevel(LogLevel.Information);
        //}).UseNLog();

    }
}
