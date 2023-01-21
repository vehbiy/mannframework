/*
	This file was generated automatically by Garcia Framework. 
	Do not edit manually. 
	Add a new partial class with the same name if you want to add extra functionality.
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MannFramework;
using System.Web.Http.Routing;


namespace MannFramework.Application
{
    [Flags]
    public enum ProjectType
    {
        //BL = 1,
        //MVC = 2,
        //WebApi = 4,
        //SocketIO = 8,
        //UnitTest = 16,
        //Desktop = 32
        BL = 1,
        MVC = 1 << 2,
        WebApi = 1 << 3,
        SocketIO = 1 << 4,
        UnitTest = 1 << 5,
        Desktop = 1 << 6,
        MSSQL = 1 << 7,
        MySql = 1 << 8,
        Angular2 = 1 << 9
    }
}
